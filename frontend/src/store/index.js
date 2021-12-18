import Vue from 'vue';
import Vuex from 'vuex';
import shared from './modules/shared';
import users from './modules/users';
import reports from './modules/reports';

Vue.use(Vuex);

export default new Vuex.Store({
    modules: {
        shared,
        users,
        reports,
    },
});
