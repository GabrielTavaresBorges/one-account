<!-- src/components/inputs/EmailField.vue -->

<script setup lang="ts">
  import { computed, useAttrs } from 'vue'
  import { mdiEmail } from '@mdi/js'

  type Rule = (value: any) => true | string

  const props = withDefaults(
    defineProps<{
      modelValue: string
      rules?: Rule[]
      label?: string
      placeholder?: string
      clearable?: boolean
      trimOnBlur?: boolean
    }>(),
    {
      modelValue: '',
      label: 'Email',
      placeholder: 'ex: nome@dominio.com',
      clearable: true,
      trimOnBlur: true,
    }
  )

  const emit = defineEmits<{
    (e: 'update:modelValue', value: string): void
  }>()

  const attrs = useAttrs()

  const model = computed<string>({
    get: () => props.modelValue ?? '',
    set: (val: string) => emit('update:modelValue', val),
  })

  function onBlur() {
    if (!props.trimOnBlur) return
    emit('update:modelValue', (props.modelValue ?? '').trim())
  }
</script>

<template>
  <v-text-field v-bind="attrs"
                v-model="model"
                :label="label"
                :placeholder="placeholder"
                :rules="rules"
                :clearable="clearable"
                :prepend-inner-icon="mdiEmail"
                type="email"
                inputmode="email"
                autocomplete="email"
                spellcheck="false"
                autocapitalize="none"
                @blur="onBlur" />
</template>
