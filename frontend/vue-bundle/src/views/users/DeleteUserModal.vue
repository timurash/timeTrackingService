<template>
  <section class="delete-user-modal">
    <el-button
        @click="openModal"
        type="danger"
        icon="el-icon-delete"
        width="20px"
        slot="activator"
    ></el-button>
    <el-dialog
        title="Удаление"
        ref=":form" label-width="12px"
        id="eModal"
        width="50%"
        :modal="true"
        :show-close="false"
        :close-on-click-modal="false"
        :visible.sync="dialogVisible"
        v-loading="loading">
      <div class="warning">
        <p>Вы действительно хотите удалить данные пользователя "{{form.firstname + ' ' + form.surname}}"?</p>
      </div>
        <span slot="footer" class="dialog-footer">
        <el-button
            type="text"
            @click="dialogVisible = false, clearFields()
        ">Отмена</el-button>
        <el-button
            type="danger"
            @click="deleteUser()"
            :disabled="loading"
            :loading="loading"
        >Удалить</el-button>
     </span>
    </el-dialog>
  </section>
</template>

<script>
export default {
  props: [
      'user'
  ],
  data () {
    return {
      dialogVisible: false,
      form: {
        id: this.user.id,
        email: this.user.email,
        surname: this.user.surname,
        firstname: this.user.firstname,
        patronymic: this.user.patronymic
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
    clearFields() {
      this.formLabelAlign = {
        email: '',
        surname: '',
        firstname: '',
        patronymic: ''
      }
    },
    async deleteUser() {
      await  this.$store.dispatch('deleteUser', this.form).then(() => {
        this.dialogVisible = false;
      })
          .catch(() => {
          });
      await this.$store.dispatch('fetchUsers');
    },
  }
}
</script>

<style scoped>
.warning {
  display: flex;
  padding: 18px 16px;
  margin: 20px 0;
}
</style>
