<!-- src/components/inputs/CpfField.vue -->

<script setup lang="ts">
import { computed, useAttrs } from 'vue'

type Rule = (value: any) => true | string

const props = withDefaults(
  defineProps<{
    modelValue: string
    rules?: Rule[]
    label?: string
    placeholder?: string
    clearable?: boolean
  }>(),
  {
    modelValue: '',
    label: 'CPF',
    placeholder: '000.000.000-00',
    clearable: true,
  }
)

const emit = defineEmits<{
  (e: 'update:modelValue', value: string): void
}>()

const attrs = useAttrs()

function digitsOnly(v: string) {
  return (v ?? '').replace(/\D/g, '')
}

function formatCpf(value: string) {
  const d = digitsOnly(value).slice(0, 11)

  const p1 = d.slice(0, 3)
  const p2 = d.slice(3, 6)
  const p3 = d.slice(6, 9)
  const p4 = d.slice(9, 11)

  let out = p1
  if (p2) out += `.${p2}`
  if (p3) out += `.${p3}`
  if (p4) out += `-${p4}`

  return out
}

/**
 * v-text-field trabalha com string exibida (mascarada),
 * mas o v-model do componente emite somente dígitos.
 */
const displayValue = computed<string>({
  get() {
    return formatCpf(props.modelValue)
  },
  set(val: string) {
    emit('update:modelValue', digitsOnly(val).slice(0, 11))
  },
})
</script>

<template>
  <v-text-field v-bind="attrs"
                v-model="displayValue"
                :label="label"
                :placeholder="placeholder"
                :rules="rules"
                :clearable="clearable"
                maxlength="14"
                inputmode="numeric"
                autocomplete="off" />
</template>
