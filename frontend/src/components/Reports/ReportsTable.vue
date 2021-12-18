<template>
    <el-container>
        <el-main v-loading="loading">
            <el-row
                type="flex"
                class="row-bg"
                justify="center"
                :md="4"
                :xs="8">
                <el-card :span="6" class="box-card" shadow="hover">
                    <div class="clearfix">
                        <span style="font-size: 18px; font-weight: 500">Отчеты</span>
                        <reportModal :is-edit="false" />
                    </div>
                    <el-table
                        ref="table"
                        :header-cell-class-name="headerCellStyle"
                        :data="reportsAll.data">
                        <slot name="columns">
                            <el-table-column
                                prop="note"
                                label="Примечание" />
                            <el-table-column
                                prop="hours"
                                label="Кол-во часов"
                                :formatter="formatHours"
                                width="200px" />
                            <el-table-column
                                prop="date"
                                label="Дата"
                                :formatter="formatDate"
                                width="200px" />
                            <el-table-column
                                label="Действия"
                                width="140">
                                <div slot-scope="{row}" class="action-buttons">
                                    <reportModal :report="row" :is-edit="true" />
                                    <span style="padding: 0 15px 0 0" />
                                    <delete-report-modal :report="row" />
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
import DeleteReportModal from './DeleteReportModal.vue';
import ReportModal from './ReportModal.vue';
import 'dayjs/locale/ru';

export default {
    components: {
        reportModal: ReportModal,
        deleteReportModal: DeleteReportModal,
    },
    computed: {
        reportsAll() {
            return this.$store.getters.reports;
        },
        loading() {
            return this.$store.getters.loading;
        },
    },
    created() {
        this.$store.dispatch('initReportsState');
    },
    methods: {
        formatHours(row) {
            const thresholds = [
                { l: 'h', r: 1 },
                { l: 'hh', r: 24, d: 'hour' },
            ];
            const rounding = Math.floor;

            const config = {
                thresholds,
                rounding,
            };

            const dayjs = require('dayjs');
            const duration = require('dayjs/plugin/duration');
            const relativeTime = require('dayjs/plugin/relativeTime');

            dayjs.extend(duration);
            dayjs.extend(relativeTime, config);

            return dayjs.duration(row.hours, 'hours').locale('ru').humanize();
        },
        formatDate(row) {
            const dayjs = require('dayjs');
            return dayjs(row.date).format('DD.MM.YYYY');
        },
        headerCellStyle({ columnIndex }) {
            if (columnIndex === 3) {
                return 'actions-column';
            }
            return '';
        },
    },
};
</script>

<style>
.actions-column {
  text-align: right!important;
}
</style>

<style scoped>
.box-card {
    width: 880px;
    padding: 0 30px;
    margin-top: 55px;
    margin-bottom: 30px;
}
.clearfix {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 10px 0 30px 10px;
}
.action-buttons {
    white-space: nowrap;
    display: flex;
    text-align: left;
}
</style>
