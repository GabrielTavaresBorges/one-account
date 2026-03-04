// src/constants/phoneType.ts

export type PhoneType =
  | 'Unknown'
  | 'Mobile'
  | 'Landline'

export const phoneTypeItems: Array<{ title: string; value: PhoneType }> = [
  { title: 'Celular', value: 'Mobile' },
  { title: 'Fixo', value: 'Landline' },  
]
