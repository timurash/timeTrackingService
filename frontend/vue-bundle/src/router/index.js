import Vue from 'vue'
import Router from "vue-router";

import Users from '../views/users/Users.vue'
import Reports from '../views/reports/Reports'
import Home from '../views/home/'
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

/*import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/!* webpackChunkName: "about" *!/ '../views/About.vue')
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router*/
