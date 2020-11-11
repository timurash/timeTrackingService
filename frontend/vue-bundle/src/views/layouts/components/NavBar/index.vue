<template>
  <div class="navWrapper">
    <div id="menu" :class="{ active: isActive }">
      <el-menu :default-active="activeIndex" mode="horizontal" @select="handleSelect" :router="true">
        <el-menu-item id="logo" index="/" :route="{ name: 'Root'}">TrackYourTime</el-menu-item>
        <div class="secondaryHeaderItems">
          <el-menu-item index="/users" :route="{ name: 'Users'}">Пользователи</el-menu-item>
          <el-menu-item index="/reports" :route="{ name: 'Reports'}">Отчёты</el-menu-item>
        </div>
      </el-menu>
    </div>
  </div>
</template>

<script>
import './navbar.scss';


export default {
  data() {
    return {
      activeIndex: '/',
      isActive: true
    }
  },
  methods: {
    handleSelect(key, keyPath) {
      console.log(key, keyPath);
    },
    select: function() {
      this.isActive = !this.isActive;
    }
  }
}
</script>