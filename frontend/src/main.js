import Vue from 'vue';
import ElementUI from 'element-ui';
import VueRouter from 'vue-router';
import App from '@/components/App.vue';
import router from './router';
import store from './store';

import './styles/index.scss';
import 'element-ui/lib/theme-chalk/index.css';

const isProduction = process.env.NODE_ENV === 'production';

Vue.use(VueRouter);
Vue.use(ElementUI, { locale: 'ru' });
Vue.config.productionTip = isProduction;
Vue.config.devtools = !isProduction;

/* eslint-disable no-new */
new Vue({
    el: '#app',
    store,
    components: { App },
    render: (h) => h(App),
    router,
    template: '<App/>',
});
