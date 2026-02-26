// src/validators/primitives.ts

export type Rule = (v: unknown) => true | string

export const required = (msg: string): Rule => (v) =>
  (!!String(v ?? '').trim() || msg)

export const maxLen = (n: number, msg: string): Rule => (v) =>
  (String(v ?? '').length <= n || msg)

export const minLen = (n: number, msg: string): Rule => (v) =>
  (String(v ?? '').length >= n || msg)

export const regex = (re: RegExp, msg: string): Rule => (v) => {
  const s = String(v ?? '').trim()
  if (!s) return true // deixa a "required" cuidar
  return re.test(s) || msg
}
