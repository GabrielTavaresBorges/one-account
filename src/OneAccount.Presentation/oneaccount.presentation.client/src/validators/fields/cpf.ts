// src/validators/fields/cpf.ts

export type Rule = (value: unknown) => true | string

function digitsOnly(value: unknown): string {
  return String(value ?? '').replace(/\D/g, '')
}

function isAllSameDigits(s: string): boolean {
  return s.length > 0 && s.split('').every((c) => c === s[0])
}

function digitAt(s: string, i: number): number {
  // '0' = 48
  return s.charCodeAt(i) - 48
}

export function isValidCpf(value: unknown): boolean {
  const cpf = digitsOnly(value)

  if (cpf.length !== 11) return false
  if (isAllSameDigits(cpf)) return false

  // 1º dígito
  let sum1 = 0
  for (let i = 0; i < 9; i++) sum1 += digitAt(cpf, i) * (10 - i)
  let dv1 = (sum1 * 10) % 11
  if (dv1 === 10) dv1 = 0
  if (dv1 !== digitAt(cpf, 9)) return false

  // 2º dígito
  let sum2 = 0
  for (let i = 0; i < 10; i++) sum2 += digitAt(cpf, i) * (11 - i)
  let dv2 = (sum2 * 10) % 11
  if (dv2 === 10) dv2 = 0
  if (dv2 !== digitAt(cpf, 10)) return false

  return true
}

/**
 * Espelham o back:
 * - vazio -> erro
 * - extrai números e valida tamanho
 * - checksum real
 */
export const cpfRules: Rule[] = [
  (v) => {
    const cpf = digitsOnly(v)
    return cpf.length > 0 || 'CPF é obrigatório.'
  },
  (v) => {
    const cpf = digitsOnly(v)
    return cpf.length === 11 || 'CPF deve conter 11 dígitos.'
  },
  (v) => {
    return isValidCpf(v) || 'CPF inválido.'
  },
]
