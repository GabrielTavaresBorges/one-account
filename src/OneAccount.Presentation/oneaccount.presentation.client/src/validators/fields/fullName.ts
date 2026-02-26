// src/validators/fields/fullName.ts

import type { Rule } from '../primitives'
import { required, minLen, maxLen } from '../primitives'

export const fullNameRules: Rule[] = [
  required('Nome é obrigatório.'),
  minLen(2, 'Nome deve ter no mínimo 2 caracteres.'),
  maxLen(100, 'Nome deve ter no máximo 100 caracteres.'),
]
