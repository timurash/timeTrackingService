import axios from "../utils/axios";
import { showSuccessNotify } from '@/utils/notify.js';
import { SET_STATE } from '@/store/helpers.js';

export default {
    state: {
        users: [],
        uniqueEmail: true
    },
    mutations: {
        setUsers(state, payload) {
            state.users = payload;
        },
        SET_UNIQUE_EMAIL(state, payload) {
            state.uniqueEmail = payload;
        },
        SET_STATE
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
        }
    },
    actions: {
        async initState({ dispatch }) {
            await dispatch('fetchUsers');
        },
        async fetchUsers ({commit}) {
            commit('clearError');
            commit('setLoading', true);
            const users = await axios.get('api/user/get');
            commit('setUsers', users);
            commit('setLoading', false)
        },
        async createUser ({commit}, payload) {
            commit('clearError');
            commit('setLoading', true);
            try {
                const  response  =  (await axios.post('api/user/create', payload)).data;
                showSuccessNotify(response);
            }
            catch (error)  {
                commit('setError', error);
                throw error;
            } finally {
                commit('setLoading', false);
            }
        },
        async updateUser ({commit}, payload) {
            commit('clearError');
            commit('setLoading', true);
            try {
                const response = (await axios.put('api/user/update', payload)).data;
                showSuccessNotify(response);
            } catch (error) {
                commit('setError', error);
                throw error;
            } finally {
                commit('setLoading', false);
            }
        },
        async deleteUser ({commit}, payload) {
            commit('clearError');
            commit('setLoading', true);
            try {
                const response = (await axios.delete('api/user/delete/' + payload.id, payload)).data;
                showSuccessNotify(response);
            } catch (error) {
                const response = error.response;
                commit('setError', error);
                console.log(response.data.errors);
                throw error;
            } finally {
                commit('setLoading', false);
            }
        },
        async checkForUniqueEmail ({commit}, payload) {
            try {
                let result = await axios.get('api/user/checkEmail/?email=' + payload.email, payload);
                console.log('Запрашиваемый адрес: ' + 'api/user/checkEmail/?email=' + payload.email);
                console.log('Result с бэка: ' + result.data );
                commit('SET_UNIQUE_EMAIL', result.data);
                console.log('В геттере: ' + this.uniqueEmail);
            } catch (error) {
                commit('setError', error);
                throw error;
            }
        }
    }
}