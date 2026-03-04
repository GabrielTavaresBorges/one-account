// src/constants/country.ts

export type CountryCode = 'BR' | 'US' | 'CA'

export const countryItems: Array<{ title: string; value: CountryCode; flag: string }> = [
  { title: 'BR', value: 'BR', flag: '🇧🇷' },
  { title: 'US', value: 'US', flag: '🇺🇸' },
  { title: 'CA', value: 'CA', flag: '🇨🇦' },
]
