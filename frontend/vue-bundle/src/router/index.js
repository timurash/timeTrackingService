import Vue from 'vue'
import Router from "vue-router";

import Users from '../views/users/Users'
import Reports from '../views/reports/Reports'
import Home from '../views/home/Home'
import AppLayout from '../views/layouts/AppLayout'

Vue.use(Router);

export const routerMap = [
  {
    path: "/",
    component: AppLayout,
    children: [
      {
        path: '/',
        name: 'Root',
        component: Home

      },
      {
        path: '/users',
        name: 'Users',
        component: Users

      },
      {
        path: '/reports',
        name: 'Reports',
        component: Reports
      }
    ]
  }
];

export default new Router({
  routes: routerMap,
  mode: "history"
});