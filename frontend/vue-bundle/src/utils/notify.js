import Vue from 'vue';

export function showSuccessNotify(text, options = {}) {
    Vue.prototype.$notify({
        message: text,

        ...options
    });
}

export function showErrorNotify(text, options = {}) {
    Vue.prototype.$notify({
        title: 'Ошибка',
        message: text,
        type: 'error',
        customClass: 'notify-error',
        ...options
    });
}

export function showInfoNotify(text, options = {}) {
    Vue.prototype.$notify({
        message: text,
        type: 'info',
        customClass: 'notify-info',
        ...options
    });
}

export function showWarningNotify(text, options = {}) {
    Vue.prototype.$notify({
        message: text,
        type: 'warning',
        customClass: 'notify-warning',
        ...options
    });
}

export function showMessage(message = '', options = {}) {
    Vue.prototype.$message({
        message,
        type: 'warning',
        offset: 50,
        ...options
    });
}
