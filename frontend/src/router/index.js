import Vue from 'vue';
import Router from 'vue-router';

import UsersTable from '@/components/Users/UsersTable.vue';
import ReportsTable from '@/components/Reports/ReportsTable.vue';
import HomePage from '@/components/HomePage/HomePage.vue';
import AppLayout from '@/components/AppLayout.vue';

Vue.use(Router);

export const routerMap = [
    {
        path: '/',
        component: AppLayout,
        children: [
            {
                path: '/',
                name: 'Root',
                component: HomePage,

            },
            {
                path: '/users',
                name: 'Users',
                component: UsersTable,

            },
            {
                path: '/reports',
                name: 'Reports',
                component: ReportsTable,
            },
        ],
    },
];

export default new Router({
    routes: routerMap,
    mode: 'history',
});
