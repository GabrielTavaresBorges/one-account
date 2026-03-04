<!-- src/components/inputs/UserPhoonesField.vue -->
<!--<script setup lang="ts">
  import { phoneTypeItems, type PhoneType } from '@/constants/phoneType'

   const model = defineModel<PhoneType | null>({ default: null })


  defineProps<{
    label?: string
    clearable?: boolean
    disabled?: boolean
    rules?: Array<(v: unknown) => true | string>
  }>()
</script>

<template>

  <v-select v-model="model"
            :label="label ?? 'Área'"
            :items="phoneTypeItems"
            item-title="title"
            item-value="value"
            variant="outlined"
            rounded="lg"
            density="comfortable"
            :clearable="clearable ?? true"
            :disabled="disabled ?? false"
            :rules="rules" />

  <v-select v-model="model"
            :label="label ?? 'País'"
            :items="phoneTypeItems"
            item-title="title"
            item-value="value"
            variant="outlined"
            rounded="lg"
            density="comfortable"
            :clearable="clearable ?? true"
            :disabled="disabled ?? false"
            :rules="rules" />

  <v-select v-model="model"
            :label="label ?? 'Tipo'"
            :items="phoneTypeItems"
            item-title="title"
            item-value="value"
            variant="outlined"
            rounded="lg"
            density="comfortable"
            :clearable="clearable ?? true"
            :disabled="disabled ?? false"
            :rules="rules" />

  <v-select v-model="model"
            :label="label ?? 'Região'"
            :items="phoneTypeItems"
            item-title="title"
            item-value="value"
            variant="outlined"
            rounded="lg"
            density="comfortable"
            :clearable="clearable ?? true"
            :disabled="disabled ?? false"
            :rules="rules" />

  <v-text-field>
    label="label ?? 'Número'

  </v-text-field>
</template>-->

<script setup lang="ts">
  import { computed } from 'vue'
  import { phoneTypeItems, type PhoneType } from '@/constants/phoneType'
  import { callingCodeItems, type CallingCode } from '@/constants/callingCode'
  import { countryItems, type CountryCode } from '@/constants/country'
  import { brazilAreaCodes } from '@/constants/areaCode'

  type PhoneModel = {
    callingCode: CallingCode
    country: CountryCode
    phoneType: PhoneType
    areaCode: string
    number: string
  }

  const model = defineModel<PhoneModel | null>({ default: null })

  defineProps<{
    rules?: Array<(v: unknown) => true | string>
  }>()

  /* ===== Helpers ===== */

  const isBrazil = computed(() => model.value?.country === 'BR')

  function digitsOnly(v: string) {
    return v.replace(/\D/g, '')
  }

  function onNumberInput(v: string) {
    if (!model.value) return
    const digits = digitsOnly(v)

    if (model.value.phoneType === 'Landline') {
      model.value.number = formatLandline(digits)
    } else {
      model.value.number = formatMobile(digits)
    }
  }

  function formatMobile(d: string) {
    d = d.slice(0, 9)
    if (d.length <= 5) return d
    return `${d.slice(0, 5)}-${d.slice(5)}`
  }

  function formatLandline(d: string) {
    d = d.slice(0, 8)
    if (d.length <= 4) return d
    return `${d.slice(0, 4)}-${d.slice(4)}`
  }
</script>

<template>
  <v-row>

    <!-- DDI -->
    <v-col cols="12" md="2">
      <v-select v-model="model.callingCode"
                label="DDI"
                :items="callingCodeItems"
                item-title="title"
                item-value="value"
                variant="outlined"
                rounded="lg"
                density="comfortable" />
    </v-col>

    <!-- País -->
    <v-col cols="12" md="2">
      <v-select v-model="model.country"
                label="País"
                :items="countryItems"
                item-title="title"
                item-value="value"
                variant="outlined"
                rounded="lg"
                density="comfortable">
        <template #selection="{ item }">
          {{ item.raw.flag }}
        </template>
        <template #item="{ props, item }">
          <v-list-item v-bind="props">
            {{ item.raw.flag }} {{ item.raw.title }}
          </v-list-item>
        </template>
      </v-select>
    </v-col>

    <!-- Tipo -->
    <v-col cols="12" md="2">
      <v-select v-model="model.phoneType"
                label="Tipo"
                :items="phoneTypeItems"
                item-title="title"
                item-value="value"
                variant="outlined"
                rounded="lg"
                density="comfortable" />
    </v-col>

    <!-- DDD -->
    <v-col cols="12" md="2">
      <v-select v-if="isBrazil"
                v-model="model.areaCode"
                label="DDD"
                :items="brazilAreaCodes"
                variant="outlined"
                rounded="lg"
                density="comfortable" />
      <v-text-field v-else
                    v-model="model.areaCode"
                    label="Área"
                    variant="outlined"
                    rounded="lg"
                    density="comfortable" />
    </v-col>

    <!-- Número -->
    <v-col cols="12" md="4">
      <v-text-field v-model="model.number"
                    label="Número"
                    variant="outlined"
                    rounded="lg"
                    density="comfortable"
                    @update:model-value="onNumberInput" />
    </v-col>

  </v-row>
</template>
