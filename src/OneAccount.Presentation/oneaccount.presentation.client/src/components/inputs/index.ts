// src/components/inputs/index.ts

import EmailField from './EmailField.vue'
import PasswordField from './PasswordField.vue'
import GenderSelect from './GenderSelect.vue'
import CpfField from './CpfField.vue'
import UserPhonesField from './UserPhonesField.vue'


export {
  EmailField,
  PasswordField,
  GenderSelect,
  CpfField,
  UserPhonesField
}

export const inputs = {
  EmailField,
  PasswordField,
  GenderSelect,
  CpfField,
  UserPhonesField,
} as const
