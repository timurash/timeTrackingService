<template>
  <el-row class="row-bg" justify="center" type="flex">
    <el-col :span="12">
      <el-card class="box-card" shadow="never">
        <div class="main-body">
          <el-header>
            <h2>TrackYourTime.ru — сервис учёта отработанного времени</h2>
          </el-header>
          <el-main>
            <h3 class="h3-text">Описание</h3>
            <p class="text">Данный сайт является, так называемым, Pet-project'ом, созданным для развития навыков программирования</p>
            <p class="text">GitHub страница проекта: <a href="https://github.com/timurash/timeTrackingService" target="_blank">github.com/timurash/timeTrackingService</a></p>
            <h3 class="h3-text">Технологический стек</h3>
            <ul class="ul-list">
              <li class="enum">BackEnd — ASP .Net Core 3, EntityFramework, PostgreSQL</li>
              <li class="enum">FrontEnd — Vue.js, Element UI, axios, Day.js</li>
              <li class="enum">Deploy — Linux, Nginx</li>
            </ul>
            <h3 class="h3-text">Используемые инструменты</h3>
            <p class="text">На данный момент в рамках проекта усвоены следующие вспомогательные инструменты:</p>
            <ol class="ol-list">
              <li class="enum">Swagger для автоматической генерации документации, доступный по адресу: <a href="https://api.trackyourtime.ru/swagger" target="_blank">api.trackyourtime.ru/swagger</a></li>
              <li class="enum">Docker для удобного и быстрого деплоя приложения</li>
              <li class="enum">Unit-тесты</li>
              <li class="enum">Логгирование backend-событий при помощи библиотеки SeriLog</li>
              <li class="enum">Система миграций, использующая систему управления версиями БД Liquibase</li>
            </ol>
            <h3 class="h3-text">Планы</h3>
            <p class="text">В будущих планах развития проекта:</p>
            <ul class="ul-list">
              <li class="enum">Добавить в проект возможность регистрации/авторизации пользователей при помощи Bearer токена</li>
              <li class="enum">Сделать автоматический деплой при помощи TeamCity или подобного ПО</li>
            </ul>
          </el-main>
        </div>
      </el-card>
    </el-col>
  </el-row>
</template>

<script>
export default {

}
</script>

<style scoped lang="scss">
a, a:visited, a:hover, a:active {
  color: #3f8ae0;
}

.text {
  margin-bottom: 12px;
  margin-top: 12px;
}

.h3-text {
  margin-bottom: 12px;
}

.enum {
margin-left: 15px;
  margin-bottom:10px;
}

.el-header {
  margin-top: 10px;
  margin-bottom: 2px;
  font-size: 15px;
}

.el-main {
  margin-bottom: 10px;
  font-size: 16px;
  padding: 5px 20px 20px 20px;
}

.el-card {
  margin-top: 40px;
}

.el-row {
  margin-bottom: 60px;

  &:last-child {
    margin-bottom: 0;
  }
}

box-card {
  width: 480px;
}

.main-body {
  font-family: "Helvetica Neue", sans-serif;
  margin: 30px;
  font-size: 18px;
}

.title {
  font-size: 20px;
  font-weight: bold;
}
</style>