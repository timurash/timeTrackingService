<template>
  <el-container>
    <el-main v-loading="loading">
      <el-row  type="flex" class="row-bg" justify="center" :md="4" :xs="8">
        <el-card :span="6" class="box-card" shadow="hover">
          <div class="clearfix">
            <span style="font-size: 18px; font-weight: 500" >Пользователи</span>
            <userModal :isEdit="false"></userModal>
          </div>
          <el-table
              :header-cell-class-name="headerCellStyle"
              :data=usersAll.data
              ref="table">
            <slot name="columns">
              <el-table-column
                  prop="surname"
                  label="Фамилия">
              </el-table-column>
              <el-table-column
                  prop="firstname"
                  label="Имя">
              </el-table-column>
              <el-table-column
                  prop="patronymic"
                  label="Отчество"
                  width="160px">
              </el-table-column>
              <el-table-column
                  prop="email"
                  label="E-mail"
                  width="200">
              </el-table-column>
              <el-table-column
                  label="Действия"
                  width="140">
                <div class="action-buttons" slot-scope="{row}">
                  <userModal :user="row" :isEdit="true"></userModal>
                  <span style="padding: 0 15px 0 0" ></span>
                  <delete-user-modal :user="row"></delete-user-modal>
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
import UserModal from './UserModal.vue'
import DeleteUserModal from "./DeleteUserModal";

export default {
  data() {
    return {
    }
  },
  created() {
    this.$store.dispatch('initState');
  },
  computed: {
    usersAll() {
      return this.$store.getters.users;
    },
    loading() {
      return this.$store.getters.loading
    }
  },
  components: {
    userModal: UserModal,
    deleteUserModal: DeleteUserModal
  },
  methods: {
    headerCellStyle({columnIndex}) {
      if (columnIndex == 4)
      {
        return "actions-column";
      }
    }
  },
}
</script>

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

