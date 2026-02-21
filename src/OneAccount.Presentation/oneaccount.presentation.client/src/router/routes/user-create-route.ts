// src/router/routes/user-create-route.ts
import type { RouteRecordRaw } from 'vue-router'

export const userCreateRoutes: RouteRecordRaw[] = [
  {
    path: '/users/create',
    name: 'user-create',
    component: () => import('@/pages/UserCreatePage.vue'),
  },
]
