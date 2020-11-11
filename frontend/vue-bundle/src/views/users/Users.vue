<template>
  <el-container>
    <el-main v-loading="loading">
      <el-row  type="flex" class="row-bg" justify="center" :md="4" :xs="8">
        <el-card :span="6" class="box-card">
          <div class="clearfix">
            <span style="padding: 10px 0 padding-left: 15px; padding-right: 50px;" >Пользователи</span>
            <createCreateUserModal></createCreateUserModal>
          </div>
          <el-table
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
                  <edit-user-modal :user="row"></edit-user-modal>
                  <span style="padding: 10px 0 padding-left: 5px; padding-right: 10px;" ></span>
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
import CreateUserModal from './CreateUserModal.vue'
import EditUseRModal from "./EditUserModal";
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
    createCreateUserModal: CreateUserModal,
    editUserModal: EditUseRModal,
    deleteUserModal: DeleteUserModal
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

