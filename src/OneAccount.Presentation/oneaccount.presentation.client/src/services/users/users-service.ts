// src/services/users/users-service.ts

import type { CreateUserRequest, CreateUserResponse } from './users-types'
import { throwApiError } from '@/services/http/http-error'

export async function createUser(payload: CreateUserRequest): Promise<CreateUserResponse> {
  const res = await fetch('/api/users', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(payload),
  })

  if (res.ok) return (await res.json()) as CreateUserResponse
  return throwApiError(res)
}
