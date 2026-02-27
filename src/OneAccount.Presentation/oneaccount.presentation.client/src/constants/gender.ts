// src/constants/gender.ts

export type Gender =
  | 'Unknown'
  | 'Male'
  | 'Female'
  | 'No_Binary'
  | 'Others'
  | 'Prefer_Not_To_Say'

export const genderItems: Array<{ title: string; value: Gender }> = [
  { title: 'Feminino', value: 'Female' },
  { title: 'Masculino', value: 'Male' },
  { title: 'Não Binário', value: 'No_Binary' },
  { title: 'Outros', value: 'Others' },
  { title: 'Prefiro não dizer', value: 'Prefer_Not_To_Say' },
]
