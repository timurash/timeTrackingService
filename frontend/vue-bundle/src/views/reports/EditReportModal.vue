<template>
  <section class="edit-report-modal">
    <slot :openModal="openModal">
      <el-button
          @click="openModal"
          type="primary"
          icon="el-icon-edit"
          width="20px"
          slot="activator"
      ></el-button>
    </slot>
    <el-dialog
        title="Изменение данных"
        label-width="12px"
        id="eModal"
        width="450px"
        :modal="true"
        :show-close="true"
        :close-on-click-modal="false"
        :visible.sync="dialogVisible"
        v-loading="loading">
      <el-form
          class="edit-report-modal"
          :model="form"
          ref="reportEditForm"
          label-position="top"
          label-width="120px"
          :rules="rules"
          status-icon>
        <el-form-item
            label="Пользователь"
            required
            prop="userid">
          <el-select
              v-model="form.userid"
              placeholder="Выберите пользователя"
              value-key="userid"
              filterable
          >
            <el-option v-for="user in usersAll.data"
                       :key="user.id"
                       :label="user.surname + ' ' + user.firstname + ' ' + user.patronymic"
                       :value="user.id">
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item
            label="Примечание"
            prop="note"
            required>
          <el-input
              type="text"
              step-strictly
              placeholder="Введите примечание отчета"
              autocomplete="off"
              v-model="form.note"
          ></el-input>
        </el-form-item>
        <el-form-item
            label="Кол-во часов"
            prop="hours"
            required>
          <el-input
              type="text"
              v-model.number="form.hours"
              placeholder="Введите количество часов"></el-input>
        </el-form-item>
        <el-form-item class="dialog-footer">
          <el-button type="text" @click="closeModal()">Закрыть</el-button>
          <el-button
              type="success"
              @click="submitForm('reportEditForm')"
              :disabled="loading || !this.isValid"
              :loading="loading"
          >Сохранить</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>
  </section>
</template>


<script>
import 'dayjs/locale/es';

export default {
  props: [
    'report'
  ],
  data() {
    let validateForSpaces = (rule, value, callback) => {
      if (value.trim() === '' && value != '') {
        callback(new Error('Поле не должно состоять только из пробелов'));
      }
      else {
        callback();
      }
    };
    let validateForHours = (rule, value, callback) => {
      if (/\D/.test(value)) {
        callback(new Error('Поле должно состоять из цифр'));
      } else if (value > 24) {
        callback(new Error('Количество часов не должно превышать 24 часа'));
      } else {
        callback();
      }
    }
    return {
      dialogVisible: false,
      isValid: true,
      form: {
        id: this.report.id,
        userid: this.report.userid,
        note: this.report.note,
        hours: this.report.hours,
        date: this.report.date
      },
      rules: {
        note: [
          {required: true, message: 'Поле обязательно для заполнения' },
          {validator: validateForSpaces, trigger: 'blur'}
        ],
        hours: [
          {required: true, message: 'Поле обязательно для заполнения'},
          {validator: validateForHours}
        ]
      }
    }
  },
  created() {
    this.$store.dispatch('fetchUsers');
  },
  computed: {
    loading() {
      return this.$store.getters.loading
    },
    usersAll() {
      return this.$store.getters.users;
    }
  },
  methods: {
    openModal() {
      this.dialogVisible = true;
    },
    closeModal() {
      this.dialogVisible = false;
      this.clearFields();
    },
    clearFields() {
      this.$refs.reportEditForm.resetFields();
    },
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.updateReport();
        }
      });
    },
    async updateReport() {
      await  this.$store.dispatch('updateReport', this.form).then(() => {
        this.dialogVisible = false;
        this.clearFields();
      })
          .catch(() => {
          });
      await this.$store.dispatch('fetchReports');
    },
  }
}
</script>

<style scoped>
.edit-report-modal {
  font-family: "Helvetica Neue", sans-serif;
}

.el-select {
  display: block;
  padding-right: 2px;
}

</style>