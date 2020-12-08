<template>
  <section class="adduser-modal">
    <slot>
      <el-button
          type="success"
          style="float: right; padding: 10px 14px"
          size="medium"
          slot="activator"
          @click="openModal"
      >+ Добавить</el-button>
    </slot>
    <el-dialog
        title="Создать новый отчет"
        id="eModal"
        width="450px"
        :modal="true"
        :show-close="true"
        :close-on-click-modal="false"
        @close="this.clearFields"
        :visible.sync="dialogVisible"
        v-loading="loading">
      <el-form
          :model="form"
          ref="reportCreateForm"
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
                placeholder="Выберите пользователя">
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
              @click="submitForm('reportCreateForm')"
              :disabled="loading || !this.isValid"
              :loading="loading"
          >Создать</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>
  </section>
</template>

<script>
import 'dayjs/locale/es';

export default {
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
        userid: null,
        note: null,
        hours: null,
        date: null
      },
      rules: {
        userid: [
          {required: true, message: 'Поле обязательно для заполнения' }
        ],
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
    this.$store.dispatch('initState');
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
      this.$refs.reportCreateForm.resetFields();
    },
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.addReport()
        }
      });
    },
    async addReport() {
      let dayjs = require('dayjs');
      this.form.date = dayjs();
      await  this.$store.dispatch('addReport', this.form).then(() => {
        this.dialogVisible = false;
      })
          .catch(() => {
          });
      await this.$store.dispatch('fetchReports');
    },
  }
}
</script>

<style scoped>

.el-select {
  display: block;
  padding-right: 2px;
}


</style>