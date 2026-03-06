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
  import { computed, watch } from 'vue'

  import { phoneTypeItems } from '@/constants/phoneType'
  import { callingCodeItems, type CallingCode } from '@/constants/callingCode'
  import { countryItems, type CountryCode } from '@/constants/country'
  import { brazilAreaCodes } from '@/constants/areaCode'

  import type { PhoneModel } from '@/models/phone-model'
  import { validateUserPhone } from '@/validators/fields/userPhone'

  import {
    getCallingCodeByCountry,
    resolveCountryFromCallingCode,
  } from '@/services/phoneCountry/phone-country-service'

  const props = defineProps<{
    rules?: Array<(v: unknown) => true | string>
  }>()

  const model = defineModel<PhoneModel>({
    default: {
      callingCode: '+55' as CallingCode,
      country: 'BR' as CountryCode,
      phoneType: 'Mobile',
      areaCode: '',
      number: '',
    },
  })

  /* ===== Validation rules (campo inteiro) ===== */
  const internalRules = computed(() => {
    const base = props.rules ?? []

    const myRule = () => validateUserPhone(model.value)

    return [...base, myRule]
  })

  /* ===== Sync country <-> callingCode ===== */
  let syncing = false

  watch(
    () => model.value.country,
    (country) => {
      if (syncing) return
      syncing = true

      // País define o DDI
      model.value.callingCode = getCallingCodeByCountry(country)

      syncing = false
    }
  )

  watch(
    () => model.value.callingCode,
    (callingCode) => {
      if (syncing) return
      syncing = true

      // DDI define (ou resolve) país
      const resolved = resolveCountryFromCallingCode(callingCode, model.value.country)
      if (resolved) model.value.country = resolved

      syncing = false
    }
  )

  /* ===== Helpers ===== */

  const isBrazil = computed(() => model.value.country === 'BR')

  function digitsOnly(v: string) {
    return v.replace(/\D/g, '')
  }

  function onNumberInput(v: string) {
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
                :rules="internalRules"
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
                :rules="internalRules"
                variant="outlined"
                rounded="lg"
                density="comfortable">
        <template #selection="{ item }">
          <v-img :src="item.raw.flagSrc"
                 :alt="item.raw.alt"
                 width="24"
                 height="16"
                 cover
                 style="display:inline-block" />
        </template>

        <template #item="{ props, item }">
          <v-list-item v-bind="props">
            <template #prepend>
              <v-img :src="item.raw.flagSrc"
                     :alt="item.raw.alt"
                     width="24"
                     height="16"
                     cover />
            </template>
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
