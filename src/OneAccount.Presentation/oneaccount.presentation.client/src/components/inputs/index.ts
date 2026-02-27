// src/components/inputs/index.ts

import GenderSelect from './GenderSelect.vue'
import CpfField from './CpfField.vue'

export { GenderSelect, CpfField }

export const inputs = {
  GenderSelect,
  CpfField,
} as const
