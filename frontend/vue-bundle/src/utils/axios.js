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

export default axios;
