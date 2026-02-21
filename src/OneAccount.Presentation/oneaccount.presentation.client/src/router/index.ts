// src/router/index.ts
import { createRouter, createWebHistory } from 'vue-router'
import { loginRoutes } from './routes/login-route'
import { userCreateRoutes } from './routes/user-create-route'

const routes = [
  { path: '/', redirect: '/login' }, 
  ...loginRoutes,
  ...userCreateRoutes,
]

export const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
