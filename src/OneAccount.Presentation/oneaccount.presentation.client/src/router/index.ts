// src/router/index.ts
import { createRouter, createWebHistory } from 'vue-router'
import { loginRoutes } from './routes/login-route'

const routes = [
  { path: '/', redirect: '/login' }, 
  ...loginRoutes,
]

export const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
