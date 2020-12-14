<template>
  <el-row type="flex" class="row-bg" justify="center" :md="4" :xs="8">
    <el-card :span="8" class="box-card" shadow="hover">
      <el-main>
        <h1 class="h1-text">
          TrackYourTime.ru — сервис учёта отработанного времени
        </h1>
        <h3 class="h3-text">Описание</h3>
        <p class="text">Данный сайт является pet-project'ом, созданным для развития навыков
          программирования</p>
        <p class="text">GitHub страница проекта: <a href="https://github.com/timurash/timeTrackingService"
                                                    target="_blank">github.com/timurash/timeTrackingService</a></p>
        <h3 class="h3-text">Технологический стек</h3>
        <ul class="ul-list">
          <li class="enum">BackEnd — ASP .Net Core 3, Entity Framework, PostgreSQL</li>
          <li class="enum">FrontEnd — Vue.js, Element UI, axios, Day.js</li>
          <li class="enum">Deploy — Linux, Nginx</li>
        </ul>
        <h3 class="h3-text">Используемые инструменты</h3>
        <p class="text">На данный момент в рамках проекта усвоены следующие вспомогательные инструменты:</p>
        <ol class="ol-list">
          <li class="enum">Swagger для автоматической генерации документации, доступный по адресу: <a
              href="https://api.trackyourtime.ru/swagger" target="_blank">api.trackyourtime.ru/swagger</a></li>
          <li class="enum">Docker для удобного и быстрого деплоя приложения</li>
          <li class="enum">Unit-тесты</li>
          <li class="enum">Логгирование backend-событий при помощи библиотеки SeriLog</li>
          <li class="enum">Система миграций, использующая систему управления версиями БД Liquibase</li>
        </ol>
        <h3 class="h3-text">Планы</h3>
        <p class="text">В будущих планах развития проекта:</p>
        <ul class="ul-list">
          <li class="enum">Добавить в проект возможность регистрации/авторизации пользователей при помощи Bearer
            токена
          </li>
          <li class="enum">Сделать автоматический деплой при помощи TeamCity или подобного ПО</li>
        </ul>
      </el-main>
    </el-card>
  </el-row>
</template>

<style scoped>
.text {
  margin-bottom: 12px;
  margin-top: 12px;
}

.h1-text {
  font-size: 24px;
  margin-bottom: 20px;
  color: black;
}

.h3-text {
  margin-bottom: 12px;
}

.enum {
  margin-left: 15px;
  margin-bottom: 10px;
}

.el-card {
  margin-top: 40px;
  width:800px;
}

.box-card {
  padding: 18px 30px;
  margin: 60px 30px 40px 40px;
}

.main-body {
  margin: 30px;
  font-size: 18px;
}

</style>