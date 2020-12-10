<template>
  <el-container>
    <el-main v-loading="loading">
      <el-row type="flex" class="row-bg" justify="center" :md="4" :xs="8">
        <el-card :span="6" class="box-card" shadow="hover">
          <div class="clearfix">
            <span style="font-size: 18px; font-weight: 500">Отчеты</span>
            <reportModal :isEdit="false"></reportModal>
          </div>
          <el-table
              :header-cell-class-name="headerCellStyle"
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
                  <reportModal :report="row" :isEdit="true"></reportModal>
                  <span style="padding: 0 15px 0 0" ></span>
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
import DeleteReportModal from "./DeleteReportModal";
import ReportModal from "./ReportModal";
import 'dayjs/locale/ru'

export default {
  data() {
    return {}
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
    reportModal: ReportModal,
    deleteReportModal: DeleteReportModal
  },
  methods: {
    formatHours(row) {
      let thresholds = [
        { l: 'h', r: 1 },
        { l: 'hh', r: 24, d: 'hour' }
      ];
      let rounding = Math.floor

      const config = {
        thresholds: thresholds,
        rounding: rounding
      }

      const dayjs = require("dayjs");
      const duration = require("dayjs/plugin/duration");
      const relativeTime = require('dayjs/plugin/relativeTime');

      dayjs.extend(duration);
      dayjs.extend(relativeTime, config);

      return dayjs.duration(row.hours, "hours").locale("ru").humanize();
    },
    formatDate(row) {
      let dayjs = require('dayjs');
      return dayjs(row.date).format('DD.MM.YYYY');
    },
    headerCellStyle({columnIndex}) {
      if (columnIndex == 3)
      {
        return "actions-column";
      }
    }
  }
}
</script>

<style>
.actions-column {
  text-align: right!important;
}
</style>

<style scoped>
.box-card {
  width: 880px;
  padding: 0px 30px;
  margin-top: 55px;
  margin-bottom: 30px;
}
.clearfix {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 0px 30px 10px;
}
.action-buttons {
  white-space: nowrap;
  display: flex;
  text-align: left;
}
</style>
