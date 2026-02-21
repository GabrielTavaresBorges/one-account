<!-- src/pages/user-create.vue -->
<script setup lang="ts">
  import { computed, reactive, ref, watch } from 'vue'
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
  } from '@mdi/js'

  type VForm = { validate: () => Promise<{ valid: boolean }> }

  /* panels */
  const openedPanels = ref<number[]>([0])

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
    birthDate: '',
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

  const passwordRules = ref([
    { text: 'Mínimo de 8 caracteres', valid: false },
    { text: '1 letra maiúscula', valid: false },
    { text: '1 letra minúscula', valid: false },
    { text: '1 número', valid: false },
    { text: '1 caractere especial (ex: @, #, $).', valid: false },
  ])

  watch(
    () => form.password,
    (p) => {
      const pwd = p ?? ''
      passwordRules.value = [
        { text: 'Mínimo de 8 caracteres', valid: pwd.length >= 8 },
        { text: '1 letra maiúscula', valid: upperRegex.test(pwd) },
        { text: '1 letra minúscula', valid: lowerRegex.test(pwd) },
        { text: '1 número', valid: digitRegex.test(pwd) },
        { text: '1 caractere especial (ex: @, #, $).', valid: specialRegex.test(pwd) },
      ]
    },
    { immediate: true }
  )

  /* minimal submit (visual only) */
  async function createAccount() {
    const validation = await formRef.value?.validate()
    if (validation && !validation.valid) {
      notify('Revise os campos obrigatórios.')
      return
    }
    notify('Visual OK. Depois conectamos no seu UserController.')
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
                  <v-expansion-panel class="panel">
                    <v-expansion-panel-title class="section-title">
                      Dados de acesso
                    </v-expansion-panel-title>

                    <v-expansion-panel-text>
                      <v-text-field v-model="form.email"
                                    label="Email"
                                    type="email"
                                    :prepend-inner-icon="mdiEmail"
                                    class="mb-4"
                                    variant="outlined"
                                    rounded="lg"
                                    density="comfortable" />

                      <v-text-field v-model="form.password"
                                    label="Senha"
                                    :type="showPassword ? 'text' : 'password'"
                                    :prepend-inner-icon="mdiLock"
                                    :append-inner-icon="showPassword ? mdiEyeOff : mdiEye"
                                    @click:append-inner="showPassword = !showPassword"
                                    class="mb-2"
                                    variant="outlined"
                                    rounded="lg"
                                    density="comfortable">
                        <template #append>
                          <v-btn variant="text"
                                 class="help-icon-btn"
                                 :ripple="false"
                                 @click="passwordHelp = true"
                                 aria-label="Ver requisitos de senha">
                            <v-icon :icon="mdiHelpCircleOutline" size="20" />
                          </v-btn>
                        </template>
                      </v-text-field>

                      <v-text-field v-model="form.confirmPassword"
                                    label="Confirmar senha"
                                    :type="showConfirmPassword ? 'text' : 'password'"
                                    :prepend-inner-icon="mdiLock"
                                    :append-inner-icon="showConfirmPassword ? mdiEyeOff : mdiEye"
                                    @click:append-inner="showConfirmPassword = !showConfirmPassword"
                                    variant="outlined"
                                    rounded="lg"
                                    density="comfortable" />
                    </v-expansion-panel-text>
                  </v-expansion-panel>

                  <!-- DADOS PESSOAIS -->
                  <v-expansion-panel class="panel">
                    <v-expansion-panel-title class="section-title">
                      Dados pessoais
                    </v-expansion-panel-title>

                    <v-expansion-panel-text>
                      <v-text-field v-model="form.fullName"
                                    label="Nome completo"
                                    class="mb-4"
                                    variant="outlined"
                                    rounded="lg"
                                    density="comfortable" />

                      <v-text-field v-model="form.cpf"
                                    label="CPF"
                                    class="mb-4"
                                    hint="Somente números"
                                    persistent-hint
                                    variant="outlined"
                                    rounded="lg"
                                    density="comfortable" />

                      <v-text-field v-model="form.rg"
                                    label="RG"
                                    class="mb-4"
                                    variant="outlined"
                                    rounded="lg"
                                    density="comfortable" />

                      <v-text-field v-model="form.birthDate"
                                    label="Data de nascimento"
                                    type="date"
                                    variant="outlined"
                                    rounded="lg"
                                    density="comfortable" />
                    </v-expansion-panel-text>
                  </v-expansion-panel>

                  <!-- ENDEREÇO -->
                  <v-expansion-panel class="panel">
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
                  <v-expansion-panel class="panel">
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
                <v-dialog v-model="passwordHelp" max-width="520">
                  <v-card rounded="xl">
                    <v-card-title class="password-dialog-title">
                      <v-icon :icon="mdiShieldLockOutline" size="22" class="mr-2" />
                      Requisitos de segurança
                    </v-card-title>

                    <v-card-text>
                      <p class="password-description">
                        Para sua segurança, crie uma senha com pelo menos <strong>8 caracteres</strong>,
                        combinando letras maiúsculas, minúsculas, números e um caractere especial.
                      </p>

                      <ul class="password-rules">
                        <li v-for="rule in passwordRules" :key="rule.text">
                          <v-icon size="18"
                                  :icon="rule.valid ? mdiCheckCircle : mdiCircleOutline"
                                  class="mr-2"
                                  :class="rule.valid ? 'rule-ok' : 'rule-pending'" />
                          {{ rule.text }}
                        </li>
                      </ul>
                    </v-card-text>

                    <v-card-actions class="justify-end">
                      <v-btn variant="text" class="btn-ghost" @click="passwordHelp = false">
                        Entendi
                      </v-btn>
                    </v-card-actions>
                  </v-card>
                </v-dialog>
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
