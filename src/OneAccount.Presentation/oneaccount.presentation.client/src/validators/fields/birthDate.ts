// src/validators/fields/birthDate.ts

import type { Rule } from '../primitives'

const MAX_AGE_YEARS = 150
function toLocalMidnight(d: Date) {
  return new Date(d.getFullYear(), d.getMonth(), d.getDate())
}

function todayLocalMidnight() {
  const now = new Date()
  return new Date(now.getFullYear(), now.getMonth(), now.getDate())
}

function addYearsLocal(dateLocalMidnight: Date, years: number) {
  return new Date(
    dateLocalMidnight.getFullYear() + years,
    dateLocalMidnight.getMonth(),
    dateLocalMidnight.getDate()
  )
}

export const birthDateRules: Rule[] = [
  (v) => (v instanceof Date ? true : 'Data de nascimento é obrigatória.'),

  (v) => {
    if (!(v instanceof Date)) return true
    const selected = toLocalMidnight(v)
    const today = todayLocalMidnight()
    return selected <= today || 'Data de nascimento não pode estar no futuro.'
  },

  (v) => {
    if (!(v instanceof Date)) return true
    const selected = toLocalMidnight(v)
    const today = todayLocalMidnight()
    const minAllowed = addYearsLocal(today, -MAX_AGE_YEARS)
    return (
      selected >= minAllowed ||
      `Data de nascimento não pode ser maior que ${MAX_AGE_YEARS} anos.`
    )
  },
]
