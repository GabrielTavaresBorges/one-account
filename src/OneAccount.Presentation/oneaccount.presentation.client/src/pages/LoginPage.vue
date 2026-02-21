<!-- src/pages/LoginPage.vue -->
<script setup lang="ts">
  import { ref } from 'vue'
  import { mdiEmail, mdiLock, mdiEye, mdiEyeOff, mdiLogin, mdiHelpCircleOutline } from '@mdi/js'

  const showPassword = ref(false)
</script>

<template>
  <v-main class="page">
    <v-container class="fill-height py-10">
      <v-row justify="center" align="start">
        <v-col cols="12" md="10" lg="9" xl="8">
          <!-- BRAND -->
          <div class="text-center brand">
            <h1 class="brand-title">
              <span class="brand-one">One</span><span class="brand-account">Account</span>
            </h1>

            <p class="brand-lead mt-3">
              Uma conta. Um ecossistema. Uma experiência simples, rápida e segura.
            </p>
          </div>

          <!-- CARD -->
          <v-card class="shell" rounded="xl" elevation="14">
            <v-row no-gutters>
              <!-- LOGIN -->
              <v-col cols="12" md="6" class="left pa-8 pa-md-10">
                <h2 class="section-title mb-2">Acessar</h2>
                <p class="section-sub mb-8">
                  Use seu e-mail e senha para entrar nos serviços integrados do OneAccount.
                </p>

                <v-form>
                  <v-text-field label="Email"
                                type="email"
                                :prepend-inner-icon="mdiEmail"
                                variant="outlined"
                                rounded="lg"
                                density="comfortable"
                                color="white"
                                base-color="white"
                                class="mb-5" />

                  <v-text-field label="Senha"
                                :type="showPassword ? 'text' : 'password'"
                                :prepend-inner-icon="mdiLock"
                                :append-inner-icon="showPassword ? mdiEyeOff : mdiEye"
                                @click:append-inner="showPassword = !showPassword"
                                variant="outlined"
                                rounded="lg"
                                density="comfortable"
                                color="white"
                                base-color="white"
                                class="mb-6" />

                  <!-- BOTÃO ENTRAR (Material 3 "pill") -->
                  <v-btn block
                         size="large"
                         rounded="pill"
                         class="btn-login">
                    <v-icon :icon="mdiLogin" start />
                    Entrar
                  </v-btn>

                  <!-- LINK AJUDA -->
                  <div class="text-center mt-4">
                    <v-btn variant="text"
                           class="help-btn"
                           :ripple="false"
                           to="/forgot-password">
                      <v-icon :icon="mdiHelpCircleOutline" start size="18" />
                      Problemas para entrar? Clique aqui.
                    </v-btn>
                  </div>
                </v-form>
              </v-col>

              <!-- RIGHT / CTA -->
              <v-col cols="12"
                     md="6"
                     class="right pa-8 pa-md-10 d-flex flex-column justify-center">
                <h3 class="text-h5 font-weight-medium mb-3">
                  Quer explorar nossos serviços?
                </h3>

                <p class="text-body-1 mb-7 right-text">
                  Junte-se a nós e simplifique sua rotina.<br />
                  Crie seu acesso gratuito em poucos segundos.
                </p>

                <div class="d-flex flex-column ga-3">
                  <!-- BOTÃO CRIAR CONTA -->
                  <v-btn size="large"
                         rounded="0"
                         variant="outlined"
                         class="btn-register"
                         :v-btn :to="{ name: 'user-create' }">
                    Criar conta
                  </v-btn>
                </div>
              </v-col>
            </v-row>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </v-main>
</template>

<style scoped>
  /* ===== PALETA (terra + verde musgo) =====
     Fundo areia:        #F3E7D3
     Areia mais quente:  #EAD9BF
     Verde terra escuro: #214B3A
     Verde folha:        #2E5E45
     Verde destaque:     #3F7A57
     Marrom texto:       #3A2F24
     Café (link):        #5b4636
  */

  /* ===== PAGE ===== */
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
    max-width: 58ch;
    margin: 0 auto;
    color: #3a2f24;
    opacity: 0.92;
    font-size: 1.02rem;
  }

  /* ===== CARD SHELL ===== */
  .shell {
    overflow: hidden;
    border: 1px solid rgba(31, 27, 22, 0.08);
    background: rgba(255, 255, 255, 0.78);
    backdrop-filter: blur(10px);
  }

  /* ===== LEFT ===== */
  .left {
    background: linear-gradient(145deg, #214b3a 0%, #2e5e45 55%, #3f7a57 120%);
    color: #ffffff;
  }

  .section-title {
    font-size: 1.35rem;
    font-weight: 650;
    letter-spacing: 0.2px;
    margin: 0;
  }

  .section-sub {
    margin: 0;
    opacity: 0.92;
  }

  /* ENTRAR: pill + tipografia mais “Material-ish” */
  .btn-login {
    background-color: rgba(0, 0, 0, 0.16);
    color: #ffffff;
    border: 1px solid rgba(255, 255, 255, 0.38);
    letter-spacing: 0.35px;
    font-weight: 650;
    text-transform: none; /* evita UPPERCASE padrão em alguns temas */
  }

  /* Link ajuda como botão-texto (garante ícone e mantém visual clean) */
  .help-btn {
    display: inline-flex;
    align-items: center;
    gap: 8px;
    color: rgba(207, 229, 255, 0.95);
    font-weight: 400;
    font-size: 0.92rem;
    letter-spacing: 0.15px;
    font-family: "Segoe UI", "Segoe UI Variable", system-ui, -apple-system, Arial, sans-serif;
    text-transform: none;
    padding: 0;
    min-height: auto;
  }

  .help-btn:hover {
    text-decoration: underline;
    opaity: 1;
  }

  /* ===== RIGHT ===== */
  .right {
    background: rgba(255, 255, 255, 0.92);
    color: #1f1b16;
  }

  .right-text {
    color: rgba(31, 27, 22, 0.85);
  }

  /* CRIAR CONTA: quadrado, clean, borda fina */
  .btn-register {
    border-color: rgba(33, 75, 58, 0.55);
    color: #214b3a;
    font-weight: 550;
    letter-spacing: 0.2px;
    text-transform: none;
  }
</style>
