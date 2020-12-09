@import '../../styles/index.scss';
@import "~element-ui/packages/theme-chalk/src/tabs";
@import "~element-ui/packages/theme-chalk/src/tab-pane";

<template>
  <section class="adduser-modal">
    <slot :openModal="openModal">
      <el-button v-if="!isEdit"
          slot="activator"
          size="medium"
          style="float: right; padding: 10px 14px"
          type="success"
          @click="openModal"
      >+ Добавить
      </el-button>
      <el-button v-if="isEdit"
                 slot="activator"
                 icon="el-icon-edit"
                 type="primary"
                 width="20px"
                 @click="openModal"
      ></el-button>
    </slot>
    <el-dialog
        v-loading="loading"
        :close-on-click-modal="true"
        :modal="true"
        :show-close="true"
        :visible.sync="dialogVisible"
        width="500px"
        @close="this.clearFields"
    >
      <div slot="title">
        <h2 class="dialog-title">
          {{ isEdit ? 'Изменение данных' : 'Новый пользователь' }}
        </h2>
      </div>
      <el-form
          ref="ruleForm"
          :model="form"
          :rules="rules"
          class="dialog-body"
          label-position="top"
          label-width="10px"
          status-icon
          @submit.native.prevent="submitForm('form')"
      >
        <el-form-item
            label="Email"
            prop="email">
          <el-input
              v-model="form.email"
              autocomplete="off"
              placeholder="Введите Email"
              step-strictly
              type="text"
          ></el-input>
        </el-form-item>
        <el-form-item
            label="Фамилия"
            prop="surname"
            required>
          <el-input
              v-model="form.surname"
              placeholder="Введите фамилию"
          ></el-input>
        </el-form-item>
        <el-form-item
            label="Имя"
            prop="firstname"
            required>
          <el-input
              v-model="form.firstname"
              placeholder="Введите имя"
          ></el-input>
        </el-form-item>
        <el-form-item
            label="Отчество"
            prop="patronymic">
          <el-input
              v-model="form.patronymic"
              placeholder="Введите отчество"
          ></el-input>
        </el-form-item>
        <el-form-item class="dialog-footer">
          <el-button type="text" @click="closeModal()">Закрыть</el-button>
          <el-button v-if="!isEdit"
              :disabled="loading"
              :loading="loading"
              type="success"
              @click="submitForm('ruleForm'), createUser()"
          >Создать
          </el-button>
          <el-button v-if="isEdit"
              :disabled="loading"
              :loading="loading"
              type="success"
              @click="submitForm('ruleForm'), updateUser()"
          >Сохранить
          </el-button>
        </el-form-item>
      </el-form>
    </el-dialog>
  </section>
</template>

<script>
// eslint-disable-next-line no-unused-vars
import {showErrorNotify, showInfoNotify, showSuccessNotify, showWarningNotify} from '@/utils/notify.js';


export default {
  props: [
    'user', 'isEdit'
  ],
  data() {
    let validateForSpaces = (rule, value, callback) => {
      if (value.trim() === '' && value != '') {
        callback(new Error('Пробелы не допустимы'));
      } else {
        callback();
      }
    };
    return {
      dialogVisible: false,
      isValid: true,
      form: {
        id: 0,
        email: '',
        surname: '',
        firstname: '',
        patronymic: '',
      },
      rules: {
        email: [
          {required: true, message: 'Поле обязательно для заполнения'},
          {type: 'email', message: 'Пожалуйста, введите корректный e-mail адрес', trigger: 'blur'},
          {validator: this.validateForUniqueEmail, trigger: 'blur'}
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
      if (this.isEdit)
      {
        this.form.id = this.user.id;
        this.form.email = this.user.email;
        this.form.surname = this.user.surname;
        this.form.firstname = this.user.firstname;
        this.form.patronymic = this.user.patronymic;
      }
      this.dialogVisible = true;
    },
    closeModal() {
      this.dialogVisible = false;
      this.clearFields();
    },
    clearFields() {
      this.$refs.ruleForm.resetFields();
    },
    async validateForUniqueEmail(rule, value, callback) {
      await this.$store.dispatch('checkForUniqueEmail', this.form).then(() => {
        if (this.$store.getters.uniqueEmail || (this.isEdit && (this.form.email == this.user.email))) {
          callback();
        } else {
          callback(new Error('Этот e-mail адрес уже занят'));
        }
      });
    },
    async createUser() {
      await this.$store.dispatch('createUser', this.form).then(() => {
        this.dialogVisible = false;
      })
          .catch(() => {
          });
      await this.$store.dispatch('fetchUsers');
    },
    async updateUser() {
      await this.$store.dispatch('updateUser', this.form).then(() => {
        this.dialogVisible = false;
      })
          .catch(() => {
          });
      await this.$store.dispatch('fetchUsers');
    },
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.isValid = true;
        } else {
          this.isValid = false;
          return false;
        }
      });
    }
  }
}
</script>

<style scoped>
.dialog-title {
  font-size: 20px;
  font-weight: 500;
  padding: 20px 0px 0px 20px;
}

.dialog-body {
  font-size: 17px;
  margin: 0 20px 0 20px;
  padding: 0 0 0 0;
  word-break: normal;
  white-space: normal;
}

.dialog-footer {
  padding-top: 20px;
  text-align: right;
}
</style>