// import Vue from 'vue';
import axiosInstance from 'axios';

const axios = axiosInstance.create({
    responseType: 'json',
    headers: {
        'Content-Type': 'application/json; charset=utf-8',
    }
});

axios.interceptors.request.use(
    config => {
        return config;
    },
    error => {
        return error;
    }
);

// axios.interceptors.response.use(
//     response => {
//         if (response && response.data && typeof response.data && response.data.isSuccess == 'boolean' && response.data && !response.data.isSuccess) {
//             Vue.prototype.$message.error(response.data.errorMessage || 'Ошибка обработки данных');
//             return Promise.reject(response);
//         }
//         return response;
//     },
//     error => {
//         return Promise.reject(error);
//     }
// );

export default axios;
