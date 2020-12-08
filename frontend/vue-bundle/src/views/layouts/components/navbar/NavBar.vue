<template>
  <div class="navWrapper">
    <div id="menu" :class="{ active: isActive }">
      <el-menu active-text-color="#012e67" :default-active="activeIndex" mode="horizontal" :router="true">
        <el-menu-item class="home" id="logo" index="/" :route="{ name: 'Root'}">
          <div class="logo-header">
            <img src="@/assets/logo.png" style="width:28px; padding-right: 10px;" alt="Logo">
            <span>TrackYourTime</span>
          </div>
        </el-menu-item>
        <div class="secondaryHeaderItems">
          <el-menu-item index="/users" :route="{ name: 'Users'}">Пользователи</el-menu-item>
          <el-menu-item index="/reports" :route="{ name: 'Reports'}">Отчёты</el-menu-item>
        </div>
      </el-menu>
    </div>
  </div>
</template>

<script>

export default {
  data() {
    return {
      activeIndex: '/',
      isActive: true
    }
  },
  methods: {
    select: function() {
      this.isActive = !this.isActive;
    }
  }
}
</script>

<style scoped lang="scss">
.home {
}

.logo-header {
  font-weight: 500;
  font-size: 21px;
}

#logo {
  height: 40px;
  border-bottom: none;
  active-text-color: blue;
}

.el-menu {
  display: flex;
  justify-content: space-between;
  width: 100%;
  background-color: white;
  backface-visibility: visible;
}

.el-menu-item {
  font-size: 17px;
  font-weight: 500;
}

.el-menu-item.is-active {
  color: red;
  background: white;
}

.el-menu-item:not(.is-active) {
  color: black;
  background: white;
}

.secondaryHeaderItems {
  display: flex;
}

</style>