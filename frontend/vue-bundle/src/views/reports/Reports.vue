<template>
  <el-container>
    <el-main v-loading="loading">
      <el-row  type="flex" class="row-bg" justify="center" :md="4" :xs="8">
        <el-card :span="6" class="box-card">
          <div class="clearfix">
            <span style="padding: 10px 0 padding-left: 15px; padding-right: 50px;" >Отчеты</span>
            <addReportModal></addReportModal>
          </div>
          <el-table
              :data=reportsAll.data
              ref="table">
            <slot name="columns">
              <el-table-column
                  prop="note"
                  label="Примечание">
              </el-table-column>
              <el-table-column
                  prop="hours"
                  label="Кол-во часов"
                  :formatter="formatHours"
                  width="200px">
              </el-table-column>
              <el-table-column
                  prop="date"
                  label="Дата"
                  :formatter="formatDate"
                  width="200px">
              </el-table-column>
              <el-table-column
                  label="Действия"
                  width="140">
                <div class="action-buttons" slot-scope="{row}">
                  <edit-report-modal :report="row"></edit-report-modal>
                  <span style="padding: 10px 0 padding-left: 5px; padding-right: 10px;" ></span>
                  <delete-report-modal :report="row"></delete-report-modal>
                </div>
              </el-table-column>
            </slot>
          </el-table>
        </el-card>
      </el-row>
    </el-main>
  </el-container>
</template>

<script>
import EditReportModal from "./EditReportModal";
import DeleteReportModal from "./DeleteReportModal";
import AddReportModal from "./AddReportModal";
import 'dayjs/locale/ru'

export default {
  data() {
    return {
    }
  },
  created() {
    this.$store.dispatch('initReportsState');
  },
  computed: {
    reportsAll() {
      return this.$store.getters.reports;
    },
    loading() {
      return this.$store.getters.loading
    }
  },
  components: {
    addReportModal: AddReportModal,
    editReportModal: EditReportModal,
    deleteReportModal: DeleteReportModal
  },
  methods: {
    // eslint-disable-next-line no-unused-vars
    formatHours(row) {
      const dayjs = require("dayjs");
      const duration = require("dayjs/plugin/duration");
      const relativeTime = require('dayjs/plugin/relativeTime');

      dayjs.extend(duration);
      dayjs.extend(relativeTime);

      return dayjs.duration(row.hours, "hours").locale("ru").humanize();
    },
    formatDate(row) {
      let dayjs = require('dayjs');
      return dayjs(row.date).format('DD.MM.YYYY');
    }
  }
}
</script>

<style scoped>
.text {
  font-size: 14px;
}
.item {
  padding: 28px 0;
}
.box-card {
  width: 880px;
  padding: 18px 30px;
  margin-top: 90px;
  margin-bottom: 30px;
  font-family: "Helvetica Neue", sans-serif;
}
.el-main {
  padding: 0;
}

.clearfix {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 18px 20px;
}
.el-card__header {
  border-bottom: none;
}

.action-buttons {
  white-space: nowrap;
  display: flex;
}
</style>

