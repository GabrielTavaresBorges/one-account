// src/validators/fields/email.ts

import type { Rule } from '../primitives'
import { required, maxLen, regex } from '../primitives'

const EMAIL_REGEX = /^[^@\s]+@[^@\s]+\.[^@\s]+$/i

export const emailRules: Rule[] = [
  required('Email é obrigatório.'),
  maxLen(254, 'Email deve ter no máximo 254 caracteres.'),
  regex(EMAIL_REGEX, 'Formato de email inválido. Ex: exemplo@dominio.com'),
]
