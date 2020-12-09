<template>
  <section class="adduser-modal">
    <slot>
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
        @close="this.clearFields">
      <div slot="title">
        <h2 class="dialog-title">
          {{ isEdit ? 'Изменение данных' : 'Создать новый отчет' }}
        </h2>
      </div>
      <el-form
          ref="reportForm"
          :model="form"
          :rules="rules"
          class="dialog-body"
          label-position="top"
          label-width="120px"
          status-icon>
        <el-col :span="15">
        <el-form-item
            label="Пользователь"
            prop="userid"
            required>
          <el-select
              v-model="form.userid"
              placeholder="Выберите пользователя">
            <el-option
                v-for="user in usersAll.data"
                       :key="user.id"
                       :label="user.surname + ' ' + user.firstname + ' ' + user.patronymic"
                       :value="user.id">
            </el-option>
          </el-select>
        </el-form-item>
        </el-col>
        <el-form-item
            label="Примечание"
            prop="note"
            required>
          <el-input
              v-model="form.note"
              autocomplete="off"
              placeholder="Введите примечание отчета"
              step-strictly
              type="text"
          ></el-input>
        </el-form-item>
        <el-form-item
            label="Кол-во часов"
            prop="hours"
            required>
          <el-input
              v-model.number="form.hours"
              placeholder="Введите количество часов"
              type="text"></el-input>
        </el-form-item>
        <el-form-item class="dialog-footer">
          <el-button type="text" @click="closeModal()">Закрыть</el-button>
          <el-button v-if="!isEdit"
              :loading="loading"
              type="success"
              @click="submitForm('reportForm'), addReport()"
          >Создать
          </el-button>
          <el-button v-if="isEdit"
              type="success"
              @click="submitForm('reportForm'), updateReport()"
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
    'report', 'isEdit'
  ],
  data() {
    let validateForSpaces = (rule, value, callback) => {
      if (value.trim() === '' && value != '') {
        callback(new Error('Поле не должно состоять только из пробелов'));
      } else {
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
        id: null,
        userid: null,
        note: null,
        hours: null,
        date: null
      },
      rules: {
        userid: [
          {required: true, message: 'Поле обязательно для заполнения'}
        ],
        note: [
          {required: true, message: 'Поле обязательно для заполнения'},
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
      if (this.isEdit)
      {
        this.form.id = this.report.id;
        this.form.userid = this.report.userid;
        this.form.note = this.report.note;
        this.form.hours = this.report.hours;
        this.form.date = this.report.date;
      }
      this.dialogVisible = true;
    },
    closeModal() {
      this.dialogVisible = false;
      this.clearFields();
    },
    clearFields() {
      this.$refs.reportForm.resetFields();
      // this.form.id = null;
      // this.form.userid = null;
      // this.form.note = null;
      // this.form.hours = null;
      // this.form.date = null;
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
    },
    async addReport() {
      if (this.isValid)
      {
        let dayjs = require('dayjs');
        this.form.date = dayjs();
        await this.$store.dispatch('addReport', this.form).then(() => {
          this.dialogVisible = false;
        })
            .catch(() => {
            });
        await this.$store.dispatch('fetchReports');
        this.clearFields();
      }
    },
    async updateReport() {
      if (this.isValid)
      {
        await  this.$store.dispatch('updateReport', this.form).then(() => {
          this.dialogVisible = false;
          this.clearFields();
        })
            .catch(() => {
            });
        await this.$store.dispatch('fetchReports');
      }
    },
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