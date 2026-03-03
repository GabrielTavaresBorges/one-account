<!-- src/components/inputs/PasswordField.vue -->

<script setup lang="ts">
  import { computed } from 'vue'
  import {
    mdiShieldLockOutline,
    mdiCheckCircle,
    mdiCloseCircle,
  } from '@mdi/js'

  type RuleItem = {
    text: string
    valid?: boolean
  }

  const props = withDefaults(
    defineProps<{
      modelValue: boolean
      title?: string
      description?: string
      rules?: RuleItem[]
      maxWidth?: number | string
      confirmText?: string
    }>(),
    {
      title: 'Requisitos mínimos',
      description:
        'Para sua segurança, crie uma senha com pelo menos 8 caracteres, combinando letras maiúsculas, minúsculas, números e um caractere especial.',
      rules: () => [],
      maxWidth: 560,
      confirmText: 'Entendi',
    }
  )

  const emit = defineEmits<{
    (e: 'update:modelValue', v: boolean): void
  }>()

  const open = computed({
    get: () => props.modelValue,
    set: (v: boolean) => emit('update:modelValue', v),
  })

  function close() {
    open.value = false
  }
</script>

<template>
  <v-dialog v-model="open" :max-width="maxWidth">
    <v-card class="phd-card" rounded="xl">
      <!-- Header -->
      <div class="phd-header">
        <div class="phd-icon">
          <v-icon :icon="mdiShieldLockOutline" size="30" />
        </div>

        <div class="phd-title-wrap">
          <div class="phd-title">
            {{ title }}
          </div>
        </div>
      </div>

      <v-divider />

      <!-- Body -->
      <v-card-text class="phd-body">
        <!-- “v-info” -> em Vuetify o equivalente moderno é v-alert -->
        <v-alert variant="tonal"
                 type="info"
                 density="comfortable"
                 rounded="lg"
                 class="phd-alert">
          {{ description }}
        </v-alert>

        <ul v-if="rules?.length" class="phd-rules">
          <li v-for="r in rules" :key="r.text" class="phd-rule">
            <v-icon size="20"
                    :icon="r.valid ? mdiCheckCircle : mdiCloseCircle"
                    :class="r.valid ? 'phd-ok' : 'phd-bad'" />
            <span class="phd-rule-text">{{ r.text }}</span>
          </li>
        </ul>

        <slot />
      </v-card-text>

      <!-- Actions (botão centralizado) -->
      <v-card-actions class="phd-actions">
        <v-btn class="phd-btn"
               variant="flat"
               color="success"
               rounded="pill"
               @click="close">
          {{ confirmText }}
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<style scoped>
  .phd-card {
    overflow: hidden;
  }

  /* Header */
  .phd-header {
    display: flex;
    gap: 14px;
    padding: 18px 20px;
    align-items: center;
  }

  .phd-icon {
    width: 52px;
    height: 52px;
    border-radius: 16px;
    display: grid;
    place-items: center;
    /* Verde (success) no padrão do tema */
    background: color-mix(in srgb, rgb(var(--v-theme-success)) 16%, transparent);
    color: rgb(var(--v-theme-success));
  }

  .phd-title-wrap {
    flex: 1;
    min-width: 0;
  }

  .phd-title {
    font-size: 1.1rem;
    font-weight: 500;
    letter-spacing: 0.2px;
    line-height: 1.2;
    /* título em verde também */
    color: #214b3a;/*rgb(var(--v-theme-success));*/
  }

  .phd-body {
    padding: 16px 20px 0 20px;
  }

  .phd-alert {
    margin-bottom: 14px;
  }

  /* Lista */
  .phd-rules {
    list-style: none;
    padding: 0;
    margin: 0;
    display: grid;
    gap: 10px;
  }

  .phd-rule {
    display: flex;
    gap: 10px;
    align-items: flex-start;
  }

  .phd-rule-text {
    line-height: 1.35;
    color: rgba(var(--v-theme-on-surface), 0.9);
  }

  /* ícones ok/erro */
  .phd-ok {
    color: rgb(var(--v-theme-success));
    margin-top: 2px;
  }

  .phd-bad {
    color: rgb(var(--v-theme-error));
    margin-top: 2px;
  }

  /* Actions centralizadas */
  .phd-actions {
    padding: 16px 20px 20px 20px;
    display: flex;
    justify-content: center;
  }

  .phd-btn {
    text-transform: none;
    font-weight: 800;
    letter-spacing: 0.2px;
    padding-inline: 22px;
    min-height: 42px;
  }
</style>
