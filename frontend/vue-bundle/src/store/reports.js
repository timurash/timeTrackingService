import axios from "../utils/axios";
import { showSuccessNotify } from '@/utils/notify.js';
import { SET_STATE } from '@/store/helpers.js';

export default {
    state: {
        reports: []
    },
    mutations: {
        setReports(state, payload) {
            state.reports = payload;
        },
        SET_STATE
    },
    getters: {
        reports(state) {
            return state.reports;
        }
    },
    actions: {
        async initReportsState({ dispatch }) {
            await dispatch('fetchReports');
        },
        async fetchReports ({commit}) {
            commit('clearError');
            commit('setLoading', true);
            const reports = await axios.get(`${process.env.VUE_APP_API_URL}/api/report/get`);
            commit('setReports', reports);
            commit('setLoading', false)
        },
        async addReport ({commit}, payload) {
            commit('clearError');
            commit('setLoading', true);
            try {
                const  response  =  (await axios.post(`${process.env.VUE_APP_API_URL}/api/report/add`, payload)).data;
                showSuccessNotify(response);
            }
            catch (error)  {
                commit('setError', error);
                throw error;
            } finally {
                commit('setLoading', false);
            }
        },
        async updateReport ({commit}, payload) {
            commit('clearError');
            commit('setLoading', true);
            try {
                const response = (await axios.put(`${process.env.VUE_APP_API_URL}/api/report/update`, payload)).data;
                showSuccessNotify(response);
            } catch (error) {
                commit('setError', error);
                throw error;
            } finally {
                commit('setLoading', false);
            }
        },
        async deleteReport ({commit}, payload) {
            commit('clearError');
            commit('setLoading', true);
            try {
                const response = (await axios.delete(`${process.env.VUE_APP_API_URL}/api/report/delete/${payload.id}`, payload)).data;
                showSuccessNotify(response);
            } catch (error) {
                commit('setError', error);
                throw error;
            } finally {
                commit('setLoading', false);
            }
        }
    }
}