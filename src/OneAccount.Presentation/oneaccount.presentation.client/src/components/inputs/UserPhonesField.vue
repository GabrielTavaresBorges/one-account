<!-- src/components/inputs/UserPhonesField.vue -->

<script setup lang="ts">
  import { computed, watch } from 'vue'

  import { phoneTypeItems } from '@/constants/phoneType'
  import { callingCodeItems, type CallingCode } from '@/constants/callingCode'
  import { countryItems, type CountryCode } from '@/constants/country'
  import { brazilAreaCodes } from '@/constants/areaCode'

  import type { PhoneModel } from '@/models/phone-model'
  import { validateUserPhone } from '@/validators/fields/userPhone'

  import { getCallingCodeByCountry, resolveCountryFromCallingCode, } from '@/services/phoneCountry/phone-country-service'
  import { formatPhoneNumber, getPhoneNumberMaxLength, } from '@/services/phoneFormat/phone-format-service'

  const props = defineProps<{
    rules?: Array<(v: unknown) => true | string>
    multiple?: boolean
    required?: boolean
  }>()

  const model = defineModel<PhoneModel>({
    default: {
      callingCode: '+55' as CallingCode,
      country: 'BR' as CountryCode,
      phoneType: 'Mobile',
      areaCode: '11',
      number: '',
    },
  })

  /* ===== Validation rules (campo inteiro) ===== */
  const internalRules = computed(() => {
    const base = props.rules ?? []

    const myRule = () => validateUserPhone(model.value)

    return [...base, myRule]
  })

  const numberPlaceholder = computed(() => {
    if (model.value.callingCode === '+55' && model.value.country === 'BR') {
      return model.value.phoneType === 'Landline'
        ? '0000-00-00'
        : '00000-00-00'
    }

    return ''
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

  watch(
    () => model.value.phoneType,
    () => {
      model.value.number = ''
    }
  )

  /* ===== Helpers ===== */

  const isBrazil = computed(() => model.value.country === 'BR')    

  function onNumberInput(v: string) {
    model.value.number = formatPhoneNumber({
      callingCode: model.value.callingCode,
      country: model.value.country,
      phoneType: model.value.phoneType,
      value: v,
    })
  }

  const numberMaxLength = computed(() =>
    getPhoneNumberMaxLength(
      model.value.callingCode,
      model.value.country,
      model.value.phoneType,
    ),
  )
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
                density="comfortable"
                clearable/>
      <v-text-field v-else
                    v-model="model.areaCode"
                    label="Área"
                    variant="outlined"
                    rounded="lg"
                    density="comfortable"
                    clearable />
    </v-col>

    <!-- Número -->
    <v-col cols="12" md="4">
      <v-text-field v-model="model.number"
                    label="Número"
                    variant="outlined"
                    rounded="lg"
                    density="comfortable"
                    clearable
                    :placeholder="numberPlaceholder"
                    :maxlength="numberMaxLength"
                    @update:model-value="onNumberInput" />
    </v-col>

  </v-row>
</template>
