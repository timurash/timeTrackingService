<template>
    <div>
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
                <span>Вы действительно хотите удалить
                    <span style="font-weight: bold">«{{ form.note }}»</span>?</span>
            </div>
            <div class="buttons">
                <el-button
                    type="text"
                    @click="dialogVisible = false, clearFields()">
                    Отмена
                </el-button>
                <el-button
                    type="danger"
                    @click="deleteReport()">
                    Удалить
                </el-button>
            </div>
        </el-dialog>
    </div>
</template>

<script>
export default {
    props: {
        report: {
            type: Object,
            required: true,
        },
    },
    data() {
        return {
            dialogVisible: false,
            form: {
                id: this.report.id,
                userid: this.report.userid,
                note: this.report.note,
                hours: this.report.hours,
                date: this.report.date,
            },
        };
    },
    methods: {
        openModal() {
            this.form.id = this.report.id;
            this.form.userid = this.report.userid;
            this.form.note = this.report.note;
            this.form.hours = this.report.hours;
            this.form.date = this.report.date;
            this.dialogVisible = true;
        },
        clearFields() {
            this.form = {
                id: '',
                userid: '',
                note: '',
                hours: '',
                date: '',
            };
        },
        async deleteReport() {
            await this.$store.dispatch('deleteReport', this.form).then(() => {
                this.dialogVisible = false;
            })
                .catch(() => {
                });
            await this.$store.dispatch('fetchReports');
        },
    },
};
</script>

<style scoped>
.dialog-title {
    font-size: 20px;
    padding: 20px 30px 0 20px;
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
