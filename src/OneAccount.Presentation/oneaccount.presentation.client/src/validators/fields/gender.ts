// src/validators/fields/gender.ts

import type { Rule } from '../primitives'

export const genderRules: Rule[] = [
  (v) => (!!v || 'Selecione um gênero.'),
  // opcional (só se você algum dia permitir Unknown no front)
  (v) => (v !== 'Unknown' || 'Selecione um gênero válido.'),
]
