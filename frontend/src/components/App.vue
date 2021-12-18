<template>
    <div id="app">
        <el-main>
            <router-view />
        </el-main>
    </div>
</template>

<script>
import { showErrorNotify, showInfoNotify, showSuccessNotify } from '@/utils/notify';

export default {
    name: 'App',
    computed: {
        error() {
            return this.$store.getters.error;
        },
    },
    watch: {
        error(errorMsg) {
            if (errorMsg != null) {
                showErrorNotify(errorMsg);
            }
        },
        successMessage(successMessage) {
            if (successMessage != null) {
                showSuccessNotify(successMessage);
            }
        },
        infoMessage(infoMessage) {
            if (infoMessage != null) {
                showInfoNotify(infoMessage);
            }
        },
    },
};
</script>

<style scoped lang="scss">
.el-main {
    padding: 0;
}
</style>
