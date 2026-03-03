<!-- src/components/inputs/PasswordField.vue -->

<script setup lang="ts">
  import { computed, ref, useAttrs } from 'vue'
  import { mdiLock, mdiEye, mdiEyeOff } from '@mdi/js'

  type Rule = (value: any) => true | string

  const props = withDefaults(
    defineProps<{
      modelValue: string
      label?: string
      rules?: Rule[]
      clearable?: boolean

      match?: string
      matchMessage?: string
    }>(),
    {
      modelValue: '',
      label: 'Senha',
      clearable: true,
      matchMessage: 'As senhas não conferem.',
    }
  )

  const emit = defineEmits<{
    (e: 'update:modelValue', value: string): void
  }>()

  const attrs = useAttrs()
  const show = ref(false)

  const model = computed<string>({
    get: () => props.modelValue ?? '',
    set: (val: string) => emit('update:modelValue', val),
  })

  const mergedRules = computed<Rule[]>(() => {
    const rs: Rule[] = []

    if (props.match !== undefined) {
      rs.push((v) => (String(v ?? '') === String(props.match ?? '') ? true : props.matchMessage))
    }

    if (props.rules?.length) rs.push(...props.rules)

    return rs
  })
</script>

<template>
  <v-text-field v-bind="attrs"
                v-model="model"
                :label="label"
                :type="show ? 'text' : 'password'"
                :prepend-inner-icon="mdiLock"
                :append-inner-icon="show ? mdiEyeOff : mdiEye"
                @click:append-inner="show = !show"
                :rules="mergedRules"
                :clearable="clearable">
    <!-- Slot dentro do input (melhor visual) -->
    <template v-if="$slots['append-inner']" #append-inner>
      <slot name="append-inner" />
    </template>

    <!-- Mantém o append externo caso você queira usar -->
    <template v-if="$slots.append" #append>
      <slot name="append" />
    </template>
  </v-text-field>
</template>
