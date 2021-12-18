import axios from '@/utils/axios';
import { showSuccessNotify } from '@/utils/notify';

export default {
    state: {
        users: [],
        uniqueEmail: false,
    },
    mutations: {
        setUsers(state, payload) {
            state.users = payload;
        },
        SET_UNIQUE_EMAIL(state, payload) {
            state.uniqueEmail = payload;
        },
    },
    getters: {
        users(state) {
            return state.users;
        },
        uniqueEmail(state) {
            return state.uniqueEmail;
        },
        response(state) {
            return state.response;
        },
    },
    actions: {
        async initState({ dispatch }) {
            await dispatch('fetchUsers');
        },
        async fetchUsers({ commit }) {
            commit('clearError');
            commit('setLoading', true);
            const users = await axios.get(`${process.env.VUE_APP_API_URL}/api/user/get/`);
            commit('setUsers', users);
            commit('setLoading', false);
        },
        async createUser({ commit }, payload) {
            commit('clearError');
            commit('setLoading', true);
            try {
                const response = (await axios.post(`${process.env.VUE_APP_API_URL}/api/user/create/`, payload)).data;
                showSuccessNotify(response);
            } catch (error) {
                commit('setError', error);
                throw error;
            } finally {
                commit('setLoading', false);
            }
        },
        async updateUser({ commit }, payload) {
            commit('clearError');
            commit('setLoading', true);
            try {
                const response = (await axios.put(`${process.env.VUE_APP_API_URL}/api/user/update/`, payload)).data;
                showSuccessNotify(response);
            } catch (error) {
                commit('setError', error);
                throw error;
            } finally {
                commit('setLoading', false);
            }
        },
        async deleteUser({ commit }, payload) {
            commit('clearError');
            commit('setLoading', true);
            try {
                const response =
                    (await axios.delete(`${process.env.VUE_APP_API_URL}/api/user/delete/${payload.id}`, payload)).data;
                showSuccessNotify(response);
            } catch (error) {
                commit('setError', error);
                throw error;
            } finally {
                commit('setLoading', false);
            }
        },
        async checkForUniqueEmail({ commit }, payload) {
            try {
                const result =
                    await axios.get(`${process.env.VUE_APP_API_URL}/api/user/checkEmail/?email=${payload.email}`, payload);
                commit('SET_UNIQUE_EMAIL', result.data);
            } catch (error) {
                commit('setError', error);
                throw error;
            }
        },
    },
}