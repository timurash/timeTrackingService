import Vue from 'vue';
import Vuex from 'vuex';
import shared from './shared';
import users from './users.js'
import reports from './reports.js'

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
  },

  modules: {
    shared,
    users,
    reports
  },

  getters: {
  },

  mutations: {
  },

  actions: {
    }
  }
);