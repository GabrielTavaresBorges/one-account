// src/components/inputs/index.ts

import EmailField from './EmailField.vue'
import PasswordField from './PasswordField.vue'
import GenderSelect from './GenderSelect.vue'
import CpfField from './CpfField.vue'


export { EmailField, PasswordField, GenderSelect, CpfField }

export const inputs = {
  EmailField,
  PasswordField,
  GenderSelect,
  CpfField,  
} as const
