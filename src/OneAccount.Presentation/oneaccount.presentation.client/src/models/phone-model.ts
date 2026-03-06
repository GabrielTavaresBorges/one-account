// src/models/phone-model.ts

import type { CallingCode } from '@/constants/callingCode'
import type { CountryCode } from '@/constants/country'
import type { PhoneType } from '@/constants/phoneType'

export type PhoneModel = {
  callingCode: CallingCode
  country: CountryCode
  phoneType: PhoneType
  areaCode: string
  number: string
}
