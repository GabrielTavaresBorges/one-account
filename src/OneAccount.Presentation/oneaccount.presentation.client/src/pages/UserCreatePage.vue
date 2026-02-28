<!-- src/pages/UserCreatePage.vue -->

<script setup lang="ts">
  import { computed, reactive, ref, watch, nextTick } from 'vue'
  import {
    mdiAccountCircleOutline,
    mdiInformationOutline,
    mdiEmail,
    mdiLock,
    mdiEye,
    mdiEyeOff,
    mdiHelpCircleOutline,
    mdiShieldLockOutline,
    mdiCheckCircle,
    mdiCircleOutline,
    mdiCalendar
  } from '@mdi/js'

  import { createUser } from '@/services/users/users-service'
  import { rules } from '@/validators'
  import { EmailField, PasswordField, GenderSelect, CpfField } from '@/components/inputs'
  import { PasswordHelpDialog } from '@/components/dialogs'
  import type { Gender } from '@/constants/gender'

  type VForm = { validate: () => Promise<{ valid: boolean }> }

  const birthDateFieldRules = computed(() => [
    () => {
      for (const r of rules.birthDate) {
        const response = r(form.birthDate)
        if (response !== true) return response
      }
      return true
    },
  ])

  /* panels */
  const openedPanels = ref<string[]>(['accessData'])

  /* refs */
  const formRef = ref<VForm | null>(null)
  const loading = ref(false)

  /* password */
  const showPassword = ref(false)
  const showConfirmPassword = ref(false)
  const passwordHelp = ref(false)

  /* snackbar (visual) */
  const snackbar = reactive({ show: false, text: '' })
  function notify(text: string) {
    snackbar.text = text
    snackbar.show = true
  }

  /* form model (visual) */
  const form = reactive({
    email: '',
    password: '',
    confirmPassword: '',
    fullName: '',
    cpf: '',
    rg: '',
    birthDate: null as Date | null,
    gender: null as Gender | null,
    cep: '',
    address: '',
    city: '',
    state: '',
    phone: '',
  })

  /* password rules visuals */
  const upperRegex = /[A-Z]/
  const lowerRegex = /[a-z]/
  const digitRegex = /\d/
  const specialRegex = /[^A-Za-z0-9]/

  const passwordOk = computed(() => {
    const p = form.password ?? ''
    return (
      p.length >= 8 &&
      upperRegex.test(p) &&
      lowerRegex.test(p) &&
      digitRegex.test(p) &&
      specialRegex.test(p)
    )
  })

  /* birth date picker */
  const birthMenu = ref(false)

  const birthLabel = computed(() => {
    if (!form.birthDate) return 'Selecione uma data'
    return new Intl.DateTimeFormat('pt-BR').format(form.birthDate)
  })

  // string YYYY-MM-DD para enviar no backend
  const birthDateIso = computed(() => {
    if (!form.birthDate) return ''
    const y = form.birthDate.getFullYear()
    const m = String(form.birthDate.getMonth() + 1).padStart(2, '0')
    const d = String(form.birthDate.getDate()).padStart(2, '0')
    return `${y}-${m}-${d}`
  })

  const passwordChecklist = computed(() => {
    const pwd = form.password ?? ''
    return [
      { text: 'Mínimo de 8 caracteres', valid: pwd.trim().length >= 8 },
      { text: '1 letra maiúscula', valid: /[A-Z]/.test(pwd) },
      { text: '1 letra minúscula', valid: /[a-z]/.test(pwd) },
      { text: '1 número', valid: /\d/.test(pwd) },
      { text: '1 caractere especial (ex: @, #, $).', valid: /[^A-Za-z0-9]/.test(pwd) },
    ]
  })

  function runRules(ruleList: Array<(v: any) => true | string>, value: any) {
    for (const rule of ruleList) {
      const response = rule(value)
      if (response !== true) return response
    }
    return true
  }

  function getPanelsWithErrors(): string[] {
    const panels = new Set<string>()

    // DADOS DE ACESSO
    if (runRules(rules.email, form.email) !== true) panels.add('accessData')

    // DADOS PESSOAIS
    if (runRules(rules.fullName, form.fullName) !== true) panels.add('personalData')
    if (runRules(rules.gender, form.gender) !== true) panels.add('personalData')

    for (const fn of birthDateFieldRules.value) {
      if (fn() !== true) {
        panels.add('personalData')
        break
      }
    }

    // DOCUMENTOS
    if (rules.cpf && runRules(rules.cpf, form.cpf) !== true) panels.add('documents')

    return [...panels]
  }

  async function createAccount() {

    // 1) abre painéis que certamente têm erro (mesmo fechados)
    const panelsToOpen = getPanelsWithErrors()
    if (panelsToOpen.length) {
      openedPanels.value = Array.from(new Set([...openedPanels.value, ...panelsToOpen]))
      await nextTick() // espera o Vue renderizar os campos dos painéis abertos
    }

    // 2) valida o que estiver montado agora (inclui os painéis recém-abertos)
    const validation = await formRef.value?.validate()
    if (validation && !validation.valid) {
      notify('Revise os campos obrigatórios.')
      return
    }

    // 3) segue fluxo normal
    const payload = {
      email: form.email.trim(),
      password: form.password.trim(),
      userName: form.fullName.trim(),
      cpfNumber: form.cpf.replace(/\D/g, ''),
      birthDate: birthDateIso.value,
      gender: form.gender,
    }


    try {
      loading.value = true
      const result = await createUser(payload)
      notify(result.message || 'Usuário criado com sucesso!')
    } catch (e: any) {
      notify(e?.message || 'Erro ao criar usuário.')
    } finally {
      loading.value = false
    }
  }
</script>

<template>
  <v-main class="page">
    <v-container class="fill-height py-6 py-md-10">
      <v-row justify="center" align="start">
        <v-col cols="12" md="10" lg="9" xl="8">
          <!-- BRAND -->
          <div class="text-center brand">
            <h1 class="brand-title">
              <span class="brand-one">One</span><span class="brand-account">Account</span>
            </h1>
            <p class="brand-lead mt-3">
              Crie seu usuário para acessar o ecossistema com uma única conta.
            </p>
          </div>

          <!-- ALERT -->
          <v-alert class="mb-6 info-alert" variant="tonal" rounded="lg" border="start">
            <template #prepend>
              <v-icon :icon="mdiInformationOutline" />
            </template>
            Não compartilhamos suas informações com terceiros.
          </v-alert>

          <!-- CARD -->
          <v-card class="shell" rounded="xl" elevation="14">
            <!-- HEADER -->
            <div class="card-header">
              <v-icon :icon="mdiAccountCircleOutline" size="56" class="mb-2" />
              <h2 class="card-title">Criar usuário</h2>
              <p class="card-subtitle">Organize seus dados por seção e finalize ao final.</p>
            </div>

            <!-- FORM -->
            <div class="pa-6 pa-md-8">
              <v-form ref="formRef" @submit.prevent="createAccount">
                <v-expansion-panels v-model="openedPanels" multiple class="mb-6 panels">
                  <!-- DADOS DE ACESSO -->
                  <v-expansion-panel class="panel" value="accessData">
                    <v-expansion-panel-title class="section-title">
                      Dados de acesso
                    </v-expansion-panel-title>

                    <v-expansion-panel-text>
                      <EmailField v-model="form.email"
                                  :rules="rules.email"
                                  class="mb-4"
                                  variant="outlined"
                                  rounded="lg"
                                  density="comfortable"
                                  clearable />

                      <PasswordField v-model="form.password"
                                     :rules="rules.password"
                                     label="Senha"
                                     class="mb-2"
                                     variant="outlined"
                                     rounded="lg"
                                     density="comfortable"
                                     clearable>
                        <template #append>
                          <v-btn variant="text"
                                 class="help-icon-btn"
                                 :ripple="false"
                                 @click="passwordHelp = true"
                                 aria-label="Ver requisitos de senha">
                            <v-icon :icon="mdiHelpCircleOutline" size="20" />
                          </v-btn>
                        </template>
                      </PasswordField>

                      <PasswordField v-model="form.confirmPassword"
                                     label="Confirmar senha"
                                     :match="form.password"
                                     class="mb-0"
                                     variant="outlined"
                                     rounded="lg"
                                     density="comfortable"
                                     clearable />
                    </v-expansion-panel-text>
                  </v-expansion-panel>

                  <!-- DADOS PESSOAIS -->
                  <v-expansion-panel class="panel" value="personalData">
                    <v-expansion-panel-title class="section-title">
                      Dados pessoais
                    </v-expansion-panel-title>

                    <v-expansion-panel-text eager>
                      <v-text-field v-model="form.fullName"
                                    label="Nome completo"
                                    class="mb-4"
                                    variant="outlined"
                                    rounded="lg"
                                    density="comfortable"
                                    clearable
                                    :rules="rules.fullName" />
                      <v-row>
                        <v-col cols="12" sm="7">
                          <v-menu v-model="birthMenu"
                                  :close-on-content-click="false"
                                  location="bottom"
                                  transition="scale-transition"
                                  min-width="auto">
                            <template #activator="{ props }">
                              <v-text-field v-bind="props"
                                            :model-value="birthLabel"
                                            label="Data de nascimento"
                                            readonly
                                            variant="outlined"
                                            rounded="lg"
                                            density="comfortable"
                                            clearable
                                            :rules="birthDateFieldRules"
                                            :prepend-inner-icon="mdiCalendar" />
                            </template>

                            <v-card min-width="300" max-width="340" elevation="12" rounded="lg">
                              <v-date-picker :model-value="form.birthDate"
                                             locale="pt-BR"
                                             hide-header
                                             flat
                                             @update:model-value="(val) => { form.birthDate = val; birthMenu.value = false }" />
                            </v-card>
                          </v-menu>
                        </v-col>

                        <v-col cols="12" sm="5">
                          <GenderSelect v-model="form.gender" :rules="rules.gender" clearable />
                        </v-col>
                      </v-row>
                    </v-expansion-panel-text>
                  </v-expansion-panel>

                  <!-- DOCUMENTOS -->
                  <v-expansion-panel class="panel" value="documents">
                    <v-expansion-panel-title class="section-title">
                      Documentos
                    </v-expansion-panel-title>

                    <v-expansion-panel-text>
                      <CpfField v-model="form.cpf"
                                :rules="rules.cpf"
                                class="mb-4"
                                variant="outlined"
                                rounded="lg"
                                density="comfortable"
                                clearable />
                    </v-expansion-panel-text>
                  </v-expansion-panel>


                  <!-- ENDEREÇO -->
                  <v-expansion-panel class="panel" value="address">
                    <v-expansion-panel-title class="section-title">
                      Endereço
                    </v-expansion-panel-title>

                    <v-expansion-panel-text>
                      <v-row>
                        <v-col cols="12" md="4">
                          <v-text-field v-model="form.cep"
                                        label="CEP"
                                        class="mb-4"
                                        variant="outlined"
                                        rounded="lg"
                                        density="comfortable" />
                        </v-col>

                        <v-col cols="12" md="8">
                          <v-text-field v-model="form.address"
                                        label="Endereço"
                                        class="mb-4"
                                        variant="outlined"
                                        rounded="lg"
                                        density="comfortable" />
                        </v-col>

                        <v-col cols="12" md="8">
                          <v-text-field v-model="form.city"
                                        label="Cidade"
                                        class="mb-4"
                                        variant="outlined"
                                        rounded="lg"
                                        density="comfortable" />
                        </v-col>

                        <v-col cols="12" md="4">
                          <v-text-field v-model="form.state"
                                        label="Estado"
                                        variant="outlined"
                                        rounded="lg"
                                        density="comfortable" />
                        </v-col>
                      </v-row>
                    </v-expansion-panel-text>
                  </v-expansion-panel>

                  <!-- CONTATO -->
                  <v-expansion-panel class="panel" value="contact">
                    <v-expansion-panel-title class="section-title">
                      Contato
                    </v-expansion-panel-title>

                    <v-expansion-panel-text>
                      <v-text-field v-model="form.phone"
                                    label="Telefone"
                                    variant="outlined"
                                    rounded="lg"
                                    density="comfortable" />
                    </v-expansion-panel-text>
                  </v-expansion-panel>
                </v-expansion-panels>

                <!-- BOTÃO CRIAR CONTA -->
                <v-btn block
                       size="large"
                       rounded="pill"
                       class="btn-primary mt-2"
                       type="submit"
                       :loading="loading"
                       :disabled="loading">
                  Criar conta
                </v-btn>

                <!-- SNACKBAR -->
                <v-snackbar v-model="snackbar.show" :timeout="4500" location="top">
                  {{ snackbar.text }}
                  <template #actions>
                    <v-btn variant="text" @click="snackbar.show = false">Fechar</v-btn>
                  </template>
                </v-snackbar>

                <!-- DIALOG AJUDA SENHA -->
                <PasswordHelpDialog v-model="passwordHelp"
                                    :rules="passwordChecklist" />
              </v-form>
            </div>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </v-main>
</template>

<style scoped>
  /* ===== PAGE (terra + verde musgo) ===== */
  .page {
    background: radial-gradient(1200px 600px at 20% 10%, #ffffff80 0%, transparent 55%), linear-gradient(180deg, #f3e7d3 0%, #ead9bf 100%);
  }

  /* ===== BRAND ===== */
  .brand {
    margin-top: 6px;
    margin-bottom: 18px;
  }

  .brand-title {
    margin: 0;
    line-height: 1;
    letter-spacing: -0.5px;
    color: #1f1b16;
  }

  .brand-one {
    font-size: 3.2rem;
    font-weight: 300;
    font-family: "Segoe UI", "Segoe UI Variable", system-ui, -apple-system, Arial, sans-serif;
  }

  .brand-account {
    font-size: 3.2rem;
    font-weight: 700;
    letter-spacing: 1.2px;
    margin-left: 2px;
    font-family: ui-monospace, "Cascadia Mono", "SFMono-Regular", Menlo, Monaco, Consolas, "Liberation Mono", monospace;
  }

  .brand-lead {
    max-width: 62ch;
    margin: 0 auto;
    color: #3a2f24;
    opacity: 0.9;
    font-size: 1.02rem;
  }

  /* ===== ALERT ===== */
  .info-alert {
    border-color: rgba(33, 75, 58, 0.35);
    color: rgba(58, 47, 36, 0.9);
    background: rgba(255, 255, 255, 0.55);
  }

  /* ===== CARD ===== */
  .shell {
    overflow: hidden;
    border: 1px solid rgba(31, 27, 22, 0.08);
    background: rgba(255, 255, 255, 0.85);
    backdrop-filter: blur(10px);
  }

  .card-header {
    padding: 22px 24px;
    text-align: center;
    color: #ffffff;
    background: linear-gradient(145deg, #214b3a 0%, #2e5e45 60%, #3f7a57 120%);
  }

  .card-title {
    margin: 0;
    font-weight: 700;
    letter-spacing: 0.2px;
  }

  .card-subtitle {
    margin: 6px 0 0;
    opacity: 0.92;
  }

  /* ===== PANELS ===== */
  .panels {
    --v-theme-surface: transparent;
  }

  .panel {
    border: 1px solid rgba(31, 27, 22, 0.08);
    border-radius: 14px;
    overflow: hidden;
    margin-bottom: 12px;
    background: rgba(255, 255, 255, 0.72);
  }

  .section-title {
    color: #214b3a;
    font-weight: 650;
    letter-spacing: 0.15px;
  }

  /* help icon button inside input */
  .help-icon-btn {
    padding: 0;
    min-width: auto;
    color: rgba(33, 75, 58, 0.85);
  }

  /* ===== BUTTONS ===== */
  .btn-primary {
    background-color: #214b3a;
    color: #ffffff;
    font-weight: 650;
    letter-spacing: 0.2px;
    text-transform: none;
  }

  .btn-ghost {
    color: #214b3a;
    font-weight: 650;
    text-transform: none;
  }

  /* ===== BIRTH DATE ===== */
  .birth-activator {
    cursor: pointer;
    min-height: 56px;
  }

  /* ===== PASSWORD DIALOG ===== */
  .password-dialog-title {
    color: #214b3a;
    font-weight: 700;
    display: flex;
    align-items: center;
  }

  .password-description {
    color: rgba(58, 47, 36, 0.88);
    margin-bottom: 14px;
    font-size: 0.95rem;
  }

  .password-rules {
    list-style: none;
    padding: 0;
    margin: 0;
  }

    .password-rules li {
      display: flex;
      align-items: center;
      margin-bottom: 8px;
      font-size: 0.92rem;
      color: rgba(31, 27, 22, 0.86);
    }

  .rule-ok {
    color: #214b3a;
  }

  .rule-pending {
    color: rgba(31, 27, 22, 0.35);
  }
</style>
