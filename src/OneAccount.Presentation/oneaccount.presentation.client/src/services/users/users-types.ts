// src/services/users/users.types.ts

export type CreateUserRequest = {
  userName: string
  emailAddress: string
  cpfNumber: string
}

export type CreateUserResponse = {
  id: string
  userName: string
  message: string
}
