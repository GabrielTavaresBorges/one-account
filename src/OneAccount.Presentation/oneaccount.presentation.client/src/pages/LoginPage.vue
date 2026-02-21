<!-- src/pages/LoginPage.vue -->

<template>
  <v-main class="login-page">
    <v-container class="fill-height">
      <v-row justify="center" align="start">
        <v-col cols="12" md="9" lg="8">
          <!-- BRAND -->
          <div class="text-center brand-container">
            <h1 class="product-name">OneAccount</h1>

            <p class="headline mt-6">
              Prepare-se para o futuro dos serviços digitais.
            </p>

            <p class="sub-headline mt-3">
              Acesse todo o nosso ecossistema com uma única conta.<br />
              Simples, rápido e seguro.
            </p>
          </div>

          <!-- CARD -->
          <v-card class="main-card" rounded="xl" elevation="16">
            <v-row no-gutters>
              <!-- LOGIN -->
              <v-col cols="12" md="6" class="login-section pa-8">
                <p class="text-body-2 mb-8 opacity-text">
                  Utilize suas credenciais para acessar todos os nossos serviços
                  de forma integrada e com o mais alto nível de segurança.
                </p>

                <v-form>
                  <v-text-field label="Email"
                                type="email"
                                variant="outlined"
                                rounded="lg"
                                density="comfortable"
                                color="white"
                                base-color="white"
                                class="mb-5" />

                  <v-text-field label="Senha"
                                type="password"
                                variant="outlined"
                                rounded="lg"
                                density="comfortable"
                                color="white"
                                base-color="white"
                                class="mb-6" />

                  <v-btn block
                         size="large"
                         rounded="lg"
                         class="login-button">
                    Entrar
                  </v-btn>

                  <div class="text-center mt-4">
                    <router-link to="/forgot-password" class="help-link">
                      Problemas para entrar? Clique aqui.
                    </router-link>
                  </div>
                </v-form>
              </v-col>

              <!-- REGISTER / CTA -->
              <v-col cols="12"
                     md="6"
                     class="register-section pa-8 d-flex flex-column align-center justify-center text-center">
                <h3 class="text-h5 font-weight-medium mb-4">
                  Quer explorar nossos serviços?
                </h3>

                <p class="text-body-1 mb-6">
                  Junte-se a nós e simplifique sua rotina.<br />
                  Crie seu acesso gratuito em poucos segundos.
                </p>

                <router-link to="/register" class="register-link">
                  Ainda não tem conta? Criar agora →
                </router-link>
              </v-col>
            </v-row>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </v-main>
</template>

<style scoped>
  /* ===== PAGE ===== */
  .login-page {
    /* sem wallpaper: apenas um fundo neutro */
    background: #0f1115;
  }

  /* ===== BRAND ===== */
  .brand-container {
    margin-top: 20px;
    margin-bottom: 40px;
  }

  .product-name {
    font-size: 3.2rem;
    font-weight: 700;
    color: #ffffff;
    letter-spacing: 0.5px;
  }

  .headline {
    font-size: 1.5rem;
    font-weight: 500;
    color: rgba(255, 255, 255, 0.9);
  }

  .sub-headline {
    font-size: 1.05rem;
    color: rgba(255, 255, 255, 0.85);
  }

  /* ===== CARD ===== */
  .main-card {
    overflow: hidden;
  }

  /* ===== LEFT (LOGIN) ===== */
  /* Verde “bom”: #2E7D32 → #43A047 (equilibrado) */
  .login-section {
    background: linear-gradient(135deg, #2e7d32, #43a047);
    color: #ffffff;
  }

  .opacity-text {
    opacity: 0.92;
  }

  .login-button {
    background-color: rgba(0, 0, 0, 0.18);
    color: #ffffff;
    border: 1px solid rgba(255, 255, 255, 0.35);
  }

  /* ===== RIGHT (REGISTER) ===== */
  .register-section {
    background: #ffffff;
  }

  .register-link {
    color: #2e7d32;
    font-weight: 700;
    font-size: 0.95rem;
    text-decoration: none;
  }

    .register-link:hover {
      text-decoration: underline;
    }

  /* ===== LINKS ===== */
  .help-link {
    font-weight: 600;
    font-size: 0.85rem;
    color: rgba(255, 255, 255, 0.95);
    text-decoration: underline;
    opacity: 0.95;
  }
</style>
