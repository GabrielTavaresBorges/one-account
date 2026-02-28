// src/validators/index.ts

import { emailRules } from './fields/email'
import { passwordRules } from './fields/password'
import { fullNameRules } from './fields/fullName'
import { birthDateRules } from './fields/birthDate'
import { genderRules } from './fields/gender'
import { cpfRules }  from './fields/cpf'

export const rules = {
  email: emailRules,
  password: passwordRules,
  fullName: fullNameRules,
  birthDate: birthDateRules,
  gender: genderRules,
  cpf: cpfRules,
}
