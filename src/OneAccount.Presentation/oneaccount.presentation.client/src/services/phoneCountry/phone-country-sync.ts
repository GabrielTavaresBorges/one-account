// src/services/phoneCountry/phone-country-sync.ts

import type { PhoneModel } from '@/models/phone-model'
import { getCallingCodeByCountry, resolveCountryFromCallingCode } from './phone-country-service'

/**
 * Chamado quando o usuário muda o país.
 * Regra: país define 1 DDI principal.
 */
export function syncCallingCodeFromCountry(phone: PhoneModel): PhoneModel {
  return {
    ...phone,
    callingCode: getCallingCodeByCountry(phone.country),
  }
}

/**
 * Chamado quando o usuário muda o DDI.
 * Regra:
 * - se DDI tem 1 país => define
 * - se DDI tem vários => mantém o atual se compatível, senão escolhe o primeiro
 */
export function syncCountryFromCallingCode(phone: PhoneModel): PhoneModel {
  const resolved = resolveCountryFromCallingCode(phone.callingCode, phone.country)
  if (!resolved) return phone

  return {
    ...phone,
    country: resolved,
  }
}
