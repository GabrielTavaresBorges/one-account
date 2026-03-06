// src/constants/country.ts
import { flagsByIso3 } from '@/assets/flags'

export type CountryCode =
  | 'AR' // Argentina
  | 'AT' // Austria
  | 'BE' // Belgium
  | 'BR' // Brazil
  | 'CA' // Canada
  | 'CH' // Switzerland
  | 'CL' // Chile
  | 'CO' // Colombia
  | 'CU' // Cuba
  | 'DE' // Germany
  | 'DK' // Denmark
  | 'EG' // Egypt
  | 'ES' // Spain
  | 'FR' // France
  | 'GB' // United Kingdom
  | 'GR' // Greece
  | 'HU' // Hungary
  | 'IT' // Italy
  | 'KZ' // Kazakhstan
  | 'MX' // Mexico
  | 'MY' // Malaysia
  | 'NL' // Netherlands
  | 'NO' // Norway
  | 'PE' // Peru
  | 'PL' // Poland
  | 'RO' // Romania
  | 'RU' // Russia
  | 'SJ' // Svalbard and Jan Mayen
  | 'SE' // Sweden
  | 'US' // United States
  | 'VE' // Venezuela
  | 'ZA' // South Africa

const iso2ToIso3: Record<CountryCode, keyof typeof flagsByIso3> = {
  AR: 'ARG',
  AT: 'AUT',
  BE: 'BEL',
  BR: 'BRA',
  CA: 'CAN',
  CH: 'CHE',
  CL: 'CHI',
  CO: 'COL',
  CU: 'CUB',
  DE: 'DEU',
  DK: 'DNK',
  EG: 'EGY',
  ES: 'ESP',
  FR: 'FRA',
  GB: 'GBR',
  GR: 'GRC',
  HU: 'HUN',
  IT: 'ITA',
  KZ: 'KAZ',
  MX: 'MEX',
  MY: 'MYS',
  NL: 'NLD',
  NO: 'NOR',
  PE: 'PER',
  PL: 'POL',
  RO: 'ROU',
  RU: 'RUS',
  SJ: 'SJM',
  SE: 'SWE',
  US: 'USA',
  VE: 'VEN',
  ZA: 'ZAF',
}

export const countryItems: Array<{
  title: string
  value: CountryCode
  flagSrc: string
  alt: string
}> = [
    { title: 'África do Sul', value: 'ZA', flagSrc: flagsByIso3[iso2ToIso3.ZA], alt: 'Bandeira da África do Sul' },
    { title: 'Alemanha', value: 'DE', flagSrc: flagsByIso3[iso2ToIso3.DE], alt: 'Bandeira da Alemanha' },
    { title: 'Argentina', value: 'AR', flagSrc: flagsByIso3[iso2ToIso3.AR], alt: 'Bandeira da Argentina' },
    { title: 'Áustria', value: 'AT', flagSrc: flagsByIso3[iso2ToIso3.AT], alt: 'Bandeira da Áustria' },
    { title: 'Bélgica', value: 'BE', flagSrc: flagsByIso3[iso2ToIso3.BE], alt: 'Bandeira da Bélgica' },
    { title: 'Brasil', value: 'BR', flagSrc: flagsByIso3[iso2ToIso3.BR], alt: 'Bandeira do Brasil' },
    { title: 'Canadá', value: 'CA', flagSrc: flagsByIso3[iso2ToIso3.CA], alt: 'Bandeira do Canadá' },
    { title: 'Cazaquistão', value: 'KZ', flagSrc: flagsByIso3[iso2ToIso3.KZ], alt: 'Bandeira do Cazaquistão' },
    { title: 'Chile', value: 'CL', flagSrc: flagsByIso3[iso2ToIso3.CL], alt: 'Bandeira do Chile' },
    { title: 'Colômbia', value: 'CO', flagSrc: flagsByIso3[iso2ToIso3.CO], alt: 'Bandeira da Colômbia' },
    { title: 'Cuba', value: 'CU', flagSrc: flagsByIso3[iso2ToIso3.CU], alt: 'Bandeira de Cuba' },
    { title: 'Dinamarca', value: 'DK', flagSrc: flagsByIso3[iso2ToIso3.DK], alt: 'Bandeira da Dinamarca' },
    { title: 'Egito', value: 'EG', flagSrc: flagsByIso3[iso2ToIso3.EG], alt: 'Bandeira do Egito' },
    { title: 'Espanha', value: 'ES', flagSrc: flagsByIso3[iso2ToIso3.ES], alt: 'Bandeira da Espanha' },
    { title: 'Estados Unidos', value: 'US', flagSrc: flagsByIso3[iso2ToIso3.US], alt: 'Bandeira dos Estados Unidos' },
    { title: 'França', value: 'FR', flagSrc: flagsByIso3[iso2ToIso3.FR], alt: 'Bandeira da França' },
    { title: 'Grécia', value: 'GR', flagSrc: flagsByIso3[iso2ToIso3.GR], alt: 'Bandeira da Grécia' },
    { title: 'Holanda', value: 'NL', flagSrc: flagsByIso3[iso2ToIso3.NL], alt: 'Bandeira da Holanda' },
    { title: 'Hungria', value: 'HU', flagSrc: flagsByIso3[iso2ToIso3.HU], alt: 'Bandeira da Hungria' },
    { title: 'Itália', value: 'IT', flagSrc: flagsByIso3[iso2ToIso3.IT], alt: 'Bandeira da Itália' },
    { title: 'México', value: 'MX', flagSrc: flagsByIso3[iso2ToIso3.MX], alt: 'Bandeira do México' },
    { title: 'Malásia', value: 'MY', flagSrc: flagsByIso3[iso2ToIso3.MY], alt: 'Bandeira da Malásia' },
    { title: 'Noruega', value: 'NO', flagSrc: flagsByIso3[iso2ToIso3.NO], alt: 'Bandeira da Noruega' },
    { title: 'Peru', value: 'PE', flagSrc: flagsByIso3[iso2ToIso3.PE], alt: 'Bandeira do Peru' },
    { title: 'Polônia', value: 'PL', flagSrc: flagsByIso3[iso2ToIso3.PL], alt: 'Bandeira da Polônia' },
    { title: 'Reino Unido', value: 'GB', flagSrc: flagsByIso3[iso2ToIso3.GB], alt: 'Bandeira do Reino Unido' },
    { title: 'Romênia', value: 'RO', flagSrc: flagsByIso3[iso2ToIso3.RO], alt: 'Bandeira da Romênia' },
    { title: 'Rússia', value: 'RU', flagSrc: flagsByIso3[iso2ToIso3.RU], alt: 'Bandeira da Rússia' },
    { title: 'Suécia', value: 'SE', flagSrc: flagsByIso3[iso2ToIso3.SE], alt: 'Bandeira da Suécia' },
    { title: 'Suíça', value: 'CH', flagSrc: flagsByIso3[iso2ToIso3.CH], alt: 'Bandeira da Suíça' },
    { title: 'Svalbard e Jan Mayen', value: 'SJ', flagSrc: flagsByIso3[iso2ToIso3.SJ], alt: 'Bandeira de Svalbard e Jan Mayen' },
    { title: 'Venezuela', value: 'VE', flagSrc: flagsByIso3[iso2ToIso3.VE], alt: 'Bandeira da Venezuela' },
  ]
