<template>
  <section class="adduser-modal">
    <slot :openModal="openModal">
      <el-button
          type="success"
          style="float: right; padding: 10px 14px"
          size="medium"
          slot="activator"
          @click="openModal"
      >+ Добавить</el-button>
    </slot>
    <el-dialog
        title="Новый пользователь"
        label-width="12px"
        id="eModal"
        width="450px"
        :modal="true"
        :show-close="true"
        :close-on-click-modal="false"
        :visible.sync="dialogVisible"
        v-loading="loading"
        @close="this.clearFields"
    >
      <el-form
          :model="form"
          ref="ruleForm"
          label-position="top"
          label-width="80px"
          :rules="rules"
          status-icon
          @submit.native.prevent="submitForm('form')"
      >
        <el-form-item
            label="Email"
            prop="email"
            required>
          <el-input
              type="text"
              step-strictly
              placeholder="Введите Email"
              autocomplete="off"
              v-model="form.email"
          ></el-input>
        </el-form-item>
        <el-form-item
            label-width="20px"
            label="Фамилия"
            prop="surname"
            required>
          <el-input
              placeholder="Введите фамилию"
              v-model="form.surname"
          ></el-input>
        </el-form-item>
        <el-form-item
            label="Имя"
            prop="firstname"
            required>
          <el-input
              placeholder="Введите имя"
              v-model="form.firstname"
          ></el-input>
        </el-form-item>
        <el-form-item
            label="Отчество"
            prop="patronymic">
          <el-input
              placeholder="Введите отчество"
              v-model="form.patronymic"
          ></el-input>
        </el-form-item>
        <el-form-item class="dialog-footer">
            <el-button type="text" @click="closeModal()">Закрыть</el-button>
            <el-button
                type="success"
                @click="submitForm('ruleForm')"
                :disabled="loading || !this.isValid"
                :loading="loading"
              >Создать</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>
  </section>
</template>

<script>
// eslint-disable-next-line no-unused-vars
import { showErrorNotify, showInfoNotify, showSuccessNotify, showWarningNotify } from '@/utils/notify.js';

export default {
  data () {
    let validateForSpaces = (rule, value, callback) => {
      if (value.trim() === '' && value != '') {
        callback(new Error('Пробелы не допустимы'));
      } else {
        callback();
      }
    };
    let validateForUniqueEmail = (rule, value, callback) => {
      this.checkForUniqueEmail(callback);
    }
    return {
      dialogVisible: false,
      isValid: true,
      form: {
        email: '',
        surname: '',
        firstname: '',
        patronymic: ''
      },
      rules: {
        email: [
          {required: true, message: 'Поле обязательно для заполнения', trigger: 'change'},
          {type: 'email', message: 'Пожалуйста, введите корректный e-mail адрес', trigger: 'blur'},
          {validator: validateForUniqueEmail, trigger: 'blur'}
        ],
        firstname: [
          {required: true, message: 'Поле обязательно для заполнения'},
          {max: 40, message: 'Имя должно быть длиной не более 40 символов', trigger: 'blur'},
          {min: 2, message: 'Имя должно быть длиной не менее 2 символов', trigger: 'blur'},
          {validator: validateForSpaces, message: 'Имя не должно состоять из пробелов', trigger: 'blur'}
        ],
        surname: [
          {required: true, message: 'Поле обязательно для заполнения'},
          {max: 40, message: 'Фамилия должно быть длиной не более 40 символов', trigger: 'blur'},
          {min: 2, message: 'Отчество должно быть длиной не менее 2 символов', trigger: 'blur'},
          {validator: validateForSpaces, message: 'Фамилия не должно состоять из пробелов', trigger: 'blur'}
          ],
        patronymic: [
          {max: 40, message: 'Фамилия должно быть длиной не более 40 символов', trigger: 'blur'},
          {min: 2, message: 'Отчество должно быть длиной не менее 2 символов', trigger: 'blur'},
          {validator: validateForSpaces, message: 'Отчество не должно состоять из пробелов', trigger: 'blur'}
          ]
      }
    }
  },
  computed: {
    loading() {
      return this.$store.getters.loading
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
      this.$refs.ruleForm.resetFields();
    },
    updateIsFormValidated(field) {
      this.isValid = this.$refs.ruleForm.validateField(field);
    },
    async checkForUniqueEmail(callback) {
      await this.$store.dispatch('checkForUniqueEmail', this.form).then(() => {
        if (this.$store.getters.uniqueEmail){
          callback();
        }
        else {
          callback(new Error('Этот e-mail адрес уже занят'));
        }});
    },
    async createUser() {
      await  this.$store.dispatch('createUser', this.form).then(() => {
        this.dialogVisible = false;
      })
      .catch(() => {
      });
      await this.$store.dispatch('fetchUsers');
    },
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.createUser()
        }
      });
    }
  }
}
</script>