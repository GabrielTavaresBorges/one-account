// src/validators/index.ts

import { emailRules } from './fields/email'
import { fullNameRules } from './fields/fullName'
import { birthDateRules } from './fields/birthDate'
import { genderRules } from './fields/gender'

export const rules = {
  email: emailRules,
  fullName: fullNameRules,
  birthDate: birthDateRules,
  gender: genderRules,
}
