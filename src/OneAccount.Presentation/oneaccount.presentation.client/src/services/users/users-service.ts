// src/services/users/users-service.ts

import type { CreateUserRequest, CreateUserResponse, UpdateUserRequest, UpdateUserResponse, UserId } from './users-types'
import { throwApiError } from '@/services/http/http-error'

export async function createUser(payload: CreateUserRequest): Promise<CreateUserResponse> {
  const response = await fetch('/api/users', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(payload),
  })

  if (response.ok) return (await response.json()) as CreateUserResponse
  return throwApiError(response)
}


export async function updateUser(id: UserId, payload: UpdateUserRequest): Promise<UpdateUserResponse> {
  const res = await fetch(`/api/users/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(payload),
  })

  if (res.ok) return (await res.json()) as UpdateUserResponse
  return throwApiError(res)
}
