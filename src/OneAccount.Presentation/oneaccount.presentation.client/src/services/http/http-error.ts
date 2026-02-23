// src/services/http/http-error.ts

import type { ApiError, ApiErrorBody } from './api-error'

/**
 * Converte uma resposta HTTP com falha (não-2xx) em um erro padronizado no client.
 *
 * Por que isso existe?
 * - Para todo service HTTP do frontend lançar (throw) um erro no MESMO formato
 * - Para a UI (pages/components) tratar erros de maneira consistente (ex.: snackbar)
 *
 * Como funciona:
 * 1) Recebe um objeto `Response` do fetch (a resposta HTTP do servidor)
 * 2) Tenta ler o body como JSON no formato { identifier, message }
 * 3) Monta uma mensagem final (body.message ou fallback com status HTTP)
 * 4) Lança (throw) um objeto que satisfaz o tipo ApiError (inclui statusCode)
 *
 * Observação: a função retorna Promise<never> porque ela SEMPRE lança um erro.
 * Ou seja: ela nunca retorna um valor "normal".
 */
export async function throwApiError(response: Response): Promise<never> {
  /**
   * Corpo de erro no formato que esperamos da API:
   * { identifier?: string, message?: string }
   *
   * Pode ser null porque:
   * - algumas respostas de erro podem não ter body JSON (ex.: 500 com HTML)
   * - ou o body pode estar vazio
   */
  let body: ApiErrorBody | null = null

  try {
    // Tenta interpretar o body da resposta como JSON (padrão de APIs REST).
    body = (await response.json()) as ApiErrorBody
  } catch {
    // Se não for JSON válido, ignoramos e seguimos com uma mensagem padrão.
  }

  /**
   * Mensagem final que a UI pode mostrar:
   * - prioriza a message vinda do backend (mais amigável e contextual)
   * - caso não exista, usa fallback com o status HTTP
   */
  const message = body?.message ?? `Erro HTTP ${response.status}`

  /**
   * Lança um erro padronizado (ApiError):
   * - statusCode: HTTP status code (ex.: 422, 404, 500)
   * - identifier/message: dados retornados pela API (quando existir)
   *
   * "satisfies ApiError" garante, em tempo de compilação (TypeScript),
   * que o objeto tem o formato esperado de ApiError.
   */
  throw {
    statusCode: response.status,
    identifier: body?.identifier,
    message,
  } satisfies ApiError
}
