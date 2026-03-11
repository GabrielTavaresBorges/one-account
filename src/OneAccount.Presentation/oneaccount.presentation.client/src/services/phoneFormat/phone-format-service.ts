// src/services/phoneFormat/phone-format-service.ts

import type { CallingCode } from '@/constants/callingCode'
import type { CountryCode } from '@/constants/country'
import type { PhoneModel } from '@/models/phone-model'

type PhoneType = PhoneModel['phoneType']

type FormatPhoneNumberInput = {
  callingCode: CallingCode
  country: CountryCode
  phoneType: PhoneType
  value: string
}

type PhoneFormatRule = {
  minDigits: number
  maxDigits: number
  formatter: (digits: string) => string
}

function digitsOnly(value: string): string {
  return value.replace(/\D/g, '')
}

function formatBrazilMobile(digits: string): string {
  const d = digits.slice(0, 9)

  if (d.length <= 5) return d
  if (d.length <= 7) return `${d.slice(0, 5)}-${d.slice(5)}`
  return `${d.slice(0, 5)}-${d.slice(5, 7)}-${d.slice(7)}`
}

function formatBrazilLandline(digits: string): string {
  const d = digits.slice(0, 8)

  if (d.length <= 4) return d
  if (d.length <= 6) return `${d.slice(0, 4)}-${d.slice(4)}`
  return `${d.slice(0, 4)}-${d.slice(4, 6)}-${d.slice(6)}`
}

function formatDefaultPhone(digits: string, maxDigits = 15): string {
  return digits.slice(0, maxDigits)
}

function getPhoneFormatRule(
  callingCode: CallingCode,
  country: CountryCode,
  phoneType: PhoneType,
): PhoneFormatRule {
  if (callingCode === '+55' && country === 'BR') {
    if (phoneType === 'Landline') {
      return {
        minDigits: 8,
        maxDigits: 8,
        formatter: formatBrazilLandline,
      }
    }

    return {
      minDigits: 9,
      maxDigits: 9,
      formatter: formatBrazilMobile,
    }
  }

  return {
    minDigits: 1,
    maxDigits: 15,
    formatter: (digits) => formatDefaultPhone(digits, 15),
  }
}

export function formatPhoneNumber({
  callingCode,
  country,
  phoneType,
  value,
}: FormatPhoneNumberInput): string {
  const digits = digitsOnly(value)
  const rule = getPhoneFormatRule(callingCode, country, phoneType)

  return rule.formatter(digits)
}

export function getPhoneNumberMaxLength(
  callingCode: CallingCode,
  country: CountryCode,
  phoneType: PhoneType,
): number {
  if (callingCode === '+55' && country === 'BR') {
    return phoneType === 'Landline' ? 10 : 11
  }

  return 15
}

export function getPhoneDigitsRange(
  callingCode: CallingCode,
  country: CountryCode,
  phoneType: PhoneType,
): Pick<PhoneFormatRule, 'minDigits' | 'maxDigits'> {
  const rule = getPhoneFormatRule(callingCode, country, phoneType)

  return {
    minDigits: rule.minDigits,
    maxDigits: rule.maxDigits,
  }
}
