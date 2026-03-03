// src/validators/fields/password.ts

type Rule = (v: unknown) => true | string

const upperRegex = /[A-Z]/
const lowerRegex = /[a-z]/
const digitRegex = /\d/
const specialRegex = /[^A-Za-z0-9]/

export const passwordRules: Rule[] = [
  (v) => {
    const s = String(v ?? '').trim()
    return s.length > 0 || 'Senha é obrigatória.'
  },
  (v) => {
    const s = String(v ?? '').trim()
    return s.length >= 8 || 'A senha deve ter no mínimo 8 caracteres.'
  },
  (v) => upperRegex.test(String(v ?? '')) || 'A senha deve ter pelo menos 1 letra maiúscula.',
  (v) => lowerRegex.test(String(v ?? '')) || 'A senha deve ter pelo menos 1 letra minúscula.',
  (v) => digitRegex.test(String(v ?? '')) || 'A senha deve ter pelo menos 1 número.',
  (v) => specialRegex.test(String(v ?? '')) || 'A senha deve ter pelo menos 1 caractere especial.',
  (v) => {
    const s = String(v ?? '').trim()
    return s.length <= 256 || 'A senha excede o tamanho máximo permitido.'
  },
]
