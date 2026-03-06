// src/validators/fields/userPhone.ts

import type { PhoneModel } from '@/models/phone-model'
import { isCountryCompatibleWithCallingCode } from '@/services/phoneCountry/phone-country-service'

export function validateUserPhone(phone: PhoneModel): true | string {
  if (!isCountryCompatibleWithCallingCode(phone.country, phone.callingCode)) {
    return 'O país selecionado não corresponde ao DDI informado.'
  }

  return true
}
