<template>
    <el-container>
        <el-main v-loading="loading">
            <el-row
                type="flex"
                class="row-bg"
                justify="center"
                :md="4"
                :xs="8">
                <el-card :span="6" class="box-card" shadow="hover">
                    <div class="clearfix">
                        <span style="font-size: 18px; font-weight: 500">Пользователи</span>
                        <UserModal :is-edit="false" />
                    </div>
                    <el-table
                        ref="table"
                        :header-cell-class-name="headerCellStyle"
                        :data="usersAll.data">
                        <slot name="columns">
                            <el-table-column
                                prop="surname"
                                label="Фамилия" />
                            <el-table-column
                                prop="firstname"
                                label="Имя" />
                            <el-table-column
                                prop="patronymic"
                                label="Отчество"
                                width="160px" />
                            <el-table-column
                                prop="email"
                                label="E-mail"
                                width="200" />
                            <el-table-column
                                label="Действия"
                                width="140">
                                <div slot-scope="{row}" class="action-buttons">
                                    <UserModal :user="row" :is-edit="true" />
                                    <span style="padding: 0 15px 0 0" />
                                    <DeleteUserModal :user="row" />
                                </div>
                            </el-table-column>
                        </slot>
                    </el-table>
                </el-card>
            </el-row>
        </el-main>
    </el-container>
</template>

<script>
import UserModal from './UserModal.vue';
import DeleteUserModal from './DeleteUserModal.vue';

export default {
    components: {
        UserModal,
        DeleteUserModal,
    },
    data() {
        return {
        };
    },
    computed: {
        usersAll() {
            return this.$store.getters.users;
        },
        loading() {
            return this.$store.getters.loading;
        },
    },
    created() {
        this.$store.dispatch('initState');
    },
    methods: {
        headerCellStyle({ columnIndex }) {
            if (columnIndex === 4) {
                return 'actions-column';
            }

            return '';
        },
    },
};
</script>

<style scoped lang="scss">
.box-card {
    width: 880px;
    padding: 0px 30px;
    margin-top: 55px;
    margin-bottom: 30px;
}
.clearfix {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px 0px 30px 10px;
}
.action-buttons {
    white-space: nowrap;
    display: flex;
    text-align: left;
}
</style>
