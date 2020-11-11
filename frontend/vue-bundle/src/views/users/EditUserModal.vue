<template>
  <section class="adduser-modal">
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
          :model="form"
          ref="ruleForm"
          label-position="top"
          label-width="80px"
          :rules="rules"
          status-icon
          @submit.native.prevent="submitForm('form')"
          class="demo-ruleForm"
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
          <el-button type="text" @click="closeModal(), clearFields()">Закрыть</el-button>
          <el-button
              type="success"
              @click="updateUser(), submitForm('ruleForm')"
              :disabled="loading && !isValid"
              :loading="loading"
          >Сохранить</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>
  </section>
</template>

<script>
export default {
  props: [
      'user'
  ],
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
        id: this.user.id,
        email: this.user.email,
        surname: this.user.surname,
        firstname: this.user.firstname,
        patronymic: this.user.patronymic
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
      console.log(this.user);
    },
    closeModal() {
      this.dialogVisible = false;
      this.clearFields();
    },
    clearFields() {
      this.form = {
        email: this.form2.email,
        surname: '',
        firstname: '',
        patronymic: ''
      }
    },
    async checkForUniqueEmail(callback) {
      await this.$store.dispatch('checkForUniqueEmail', this.form).then(() => {
        console.log('В настоящем геттере: ' + this.$store.getters.uniqueEmail)
        if (this.$store.getters.uniqueEmail || (this.form.email == this.user.email)){
          callback();
        }
        else {
          callback(new Error('Этот e-mail адрес уже занят'));
        }});
    },
    async updateUser() {
      await  this.$store.dispatch('updateUser', this.form).then(() => {
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