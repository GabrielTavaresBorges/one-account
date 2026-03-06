// src/services/phoneCountry/phone-country-service.ts

import type { CountryCode } from '@/constants/country'
import type { CallingCode } from '@/constants/callingCode'
import {
  countryToCallingCodeMap,
  callingCodeToCountriesMap,
} from '@/constants/phoneCountryMap'

export function getCallingCodeByCountry(country: CountryCode): CallingCode {
  return countryToCallingCodeMap[country]
}

export function getCountriesByCallingCode(callingCode: CallingCode): CountryCode[] {
  return callingCodeToCountriesMap[callingCode] ?? []
}

export function isCountryCompatibleWithCallingCode(
  country: CountryCode,
  callingCode: CallingCode,
): boolean {
  const countries = getCountriesByCallingCode(callingCode)
  return countries.includes(country)
}

export function resolveCountryFromCallingCode(
  callingCode: CallingCode,
  currentCountry?: CountryCode | null,
): CountryCode | null {
  const countries = getCountriesByCallingCode(callingCode)

  if (currentCountry && countries.includes(currentCountry)) {
    return currentCountry
  }

  const [firstCountry] = countries
  return firstCountry ?? null
}
