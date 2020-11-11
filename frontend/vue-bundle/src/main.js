import Vue from 'vue'
import App from './App.vue'
import router from './router/index.js'
import store from './store/store';
import ElementUI from 'element-ui'
import VueRouter from 'vue-router'

import 'element-ui/lib/theme-chalk/index.css'
import "./styles/index.scss"; // global css

Vue.use(VueRouter);
Vue.use(ElementUI, { locale: 'ru' });
Vue.config.productionTip = false;
Vue.config.devtools = true;

new Vue({
  el: '#app',
  store,
  render: h => h(App),
  router,
  components: { App },
  template: "<App/>"
});

