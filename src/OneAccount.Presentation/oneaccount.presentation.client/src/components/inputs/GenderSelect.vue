<!-- src/components/inputs/GenderSelect.vue -->

<script setup lang="ts">
  import { genderItems, type Gender } from '@/constants/gender'

  const model = defineModel<Gender | null>({ default: null })

  defineProps<{
    label?: string
    clearable?: boolean
    disabled?: boolean
    rules?: Array<(v: unknown) => true | string>
  }>()
</script>

<template>
  <v-select v-model="model"
            :label="label ?? 'Gênero'"
            :items="genderItems"
            item-title="title"
            item-value="value"
            variant="outlined"
            rounded="lg"
            density="comfortable"
            :clearable="clearable ?? true"
            :disabled="disabled ?? false"
            :rules="rules" />
</template>
