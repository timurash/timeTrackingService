<template>
  <section class="delete-report-modal">
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
        <p>Вы действительно хотите удалить {{form.note}}?</p>
      </div>
      <span slot="footer" class="dialog-footer">
        <el-button
            type="text"
            @click="dialogVisible = false, clearFields()
        ">Отмена</el-button>
        <el-button
            type="danger"
            @click="deleteReport()"
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
    'report'
  ],
  data () {
    return {
      dialogVisible: false,
      form: {
        id: this.report.id,
        userid: this.report.userid,
        note: this.report.note,
        hours: this.report.hours,
        date: this.report.date
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
      this.form = {
        id: '',
        userid: '',
        note: '',
        hours: '',
        date: ''
      }
    },
    async deleteReport() {
      await  this.$store.dispatch('deleteReport', this.form).then(() => {
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
.warning {
  display: flex;
  padding: 18px 16px;
  margin: 20px 0;
}
</style>
