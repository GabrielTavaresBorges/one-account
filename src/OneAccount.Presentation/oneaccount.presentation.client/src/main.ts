import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import router from './router'

createApp(App)
  .use(createPinia())
  .use(vuetify)
  .use(router)
  .mount('#app')
