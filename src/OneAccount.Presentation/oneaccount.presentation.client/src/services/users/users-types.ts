// src/services/users/users.types.ts

/** Identificador de User (GUID em string no client) */
export type UserId = string

/** Campos base usados no CREATE */
export type UserCreateCore = {
  userName: string
  emailAddress: string
  cpfNumber: string
}

/** ===== CREATE ===== */
export type CreateUserRequest = UserCreateCore

export type CreateUserResponse = {
  id: UserId
  userName: string
  message: string
}

/** ===== UPDATE =====
 * Update parcial:
 * - Envie apenas o que quer alterar (campos opcionais)
 * - CPF NÃO é atualizado (não existe no payload)
 * - Id vai na URL: PUT /api/users/{id}
 */
export type UpdateUserRequest = {
  userName?: string 
  emailAddress?: string
}

/** Campos retornados pelo Update (conforme seu handler) */
export type UpdateUserResponse = {
  id: UserId
  updatedFields: string[]
  message: string
}
