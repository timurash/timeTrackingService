<template>
    <section class="delete-user-modal">
        <el-button
            slot="activator"
            icon="el-icon-delete"
            type="danger"
            width="20px"
            @click="openModal" />
        <el-dialog
            ref=":form"
            :close-on-click-modal="false"
            :modal="true"
            :show-close="false"
            :visible.sync="dialogVisible"
            top="200px"
            width="500px">
            <div slot="title">
                <h2 class="dialog-title">
                    Удаление
                </h2>
            </div>
            <div class="dialog-text">
                <span>Вы действительно хотите удалить данные пользователя
                    <span style="font-weight: bold">«{{ form.surname }} {{ form.firstname }}»</span>?</span>
            </div>
            <div class="buttons">
                <el-button
                    type="text"
                    @click="dialogVisible = false, clearFields()">
                    Отмена
                </el-button>
                <el-button
                    type="danger"
                    @click="deleteUser()">
                    Удалить
                </el-button>
            </div>
        </el-dialog>
    </section>
</template>

<script>
export default {
    props: {
        user: {
            type: Object,
            required: true,
        },
    },
    data() {
        return {
            dialogVisible: false,
            form: {
                id: 0,
                email: '',
                surname: '',
                firstname: '',
                patronymic: '',
            },
        };
    },
    methods: {
        openModal() {
            this.form.id = this.user.id;
            this.form.email = this.user.email;
            this.form.surname = this.user.surname;
            this.form.firstname = this.user.firstname;
            this.form.patronymic = this.user.patronymic;

            this.dialogVisible = true;
        },
        clearFields() {
            this.form = {
                id: 0,
                email: '',
                surname: '',
                firstname: '',
                patronymic: '',
            };
        },
        async deleteUser() {
            await this.$store.dispatch('deleteUser', this.form).then(() => {
                this.dialogVisible = false;
            })
                .catch(() => {
                });
            await this.$store.dispatch('fetchUsers');
        },
    },
};
</script>

<style scoped>
.dialog-title {
    font-size: 20px;
    padding: 20px 30px 0px 20px;
}
.dialog-text {
    font-size: 15px;
    margin: 0 20px 0 20px;
    word-break: normal;
    white-space: normal;
}
.buttons {
    padding: 20px 20px 0 40px;
    text-align: right;
}
</style>
