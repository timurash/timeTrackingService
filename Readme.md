

# Сервис для учета отработанного времени

Структура репозитория:

## devops
Каталог предназначен для хранения скриптов, необходимых для развертывания сервисов

## documentation
Каталог содержит документацию

## sources
Каталог предназначен для хранения исходных кодов сервиса и дополнительных библиотек

## migrations
В данном каталоге находятся скрипты миграции БД

## resources
В данном каталоге хранятся исходники ресурсов, которые используются в проекте

# Инструкции

## Unit-тесты
В проекте реализованы Unit-тесты для контроллеров и сервисов

### Просмотреть список всех доступных тестов
```
dotnet test --list-tests
```

### Запустить тесты
```
dotnet test
```
## Миграции
В репозитории имеются скрипты миграции Liquibase для создания таблиц и добавления в них тестовых данных.

Тестовые данные находятся в CSV файлах.

Для использования скриптов необходимо:

 1.  Скачать JDBC Postgres Driver по ссылке: [https://jdbc.postgresql.org/download.html](https://jdbc.postgresql.org/download.html)
 2.  Отредактировать файл "liquibase.properties", указав в поле "classpath" путь до скачанного JDBC драйвера.
 3.  В том же файле "liquibase.properties" добавить информацию для доступа к своей БД в поля "Username" и "Password". 

Для выполнения скриптов необходимо из папки scripts ввести в консоли команду:
```
liquibase update
```

## Swagger
В проект добавлен Swagger. Найти его можно по адресу https://localhost:26038/swagger/index.html

## Установка сервиса в IIS под Windows

1. Из папки sources/PL в командной строке запустить команду:
```
dotnet publish
```
2. Включить "Слжубы IIS" в компонентах Windows
3. Установить  [ASP.NET Core Runtime Hosting Bundle](https://dotnet.microsoft.com/download/dotnet-core/3.1)   
4. После установки Hosting Budle перезагрузить "Службы IIS" исполнив команду в командной строке:
```
iisreset
```
5. В "Диспетчере служб IIS" добавить новый веб-сайт. В физическом путе указать путь до сгенерированных в результате первого пункта файлов. Имя узла - "localhost"
6. В том же "Диспетчере служб IIS" перейти к пункту "Пулы приложений", выбрать наше приложение, справа выбрать пункт "Основные настройки" и в открывшемся окне для параметра "Версия среды CLR.NET" установить значение "Без управляемого кода"
7. Скачать и установить [PostgresSQL](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads)
8. В папке опубликованного приложения найти файл "appsettings.json" и для параметра "DefaultConnection" вписать строку:
```
Host = localhost; Port = 5432; Database = TimeTrackingServiceDB; Username = postgres; Password = 1234;
```
При этом заменив Username и Password на собственные. 

Если все шаги сделаны правильно, то сервис должен заработать по адресу "localhost".

## Установка сервиса на ОС Linux
### Исходные данные
В данном примере мы будем использовать удаленный сервер на ОС Ubuntu.
Для развертывания сервиса нам понадобится:
1. Установленный Nginx
2. 2 настроенных A домена, указывающих на публичный IP-адрес сервера
3. Установленный PostgreSQL

### Процесс установки

1. Установка .NET Core Runtime. По очереди введите в терминал перечисленные ниже команды:
```
cd ~
```
```
wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb
```
```
sudo dpkg -i packages-microsoft-prod.deb
```
```
sudo add-apt-repository universe
```
```
sudo apt install apt-transport-https
```
```
sudo apt update
```
```
sudo apt install dotnet-sdk-3.1
```
2. Установка Node.js. По очереди введите в терминал перечисленные ниже команды:
```
cd ~
curl -sL https://deb.nodesource.com/setup_8.x -o nodesource_setup.sh
sudo  bash nodesource_setup.sh
sudo  apt-get  install nodejs
```
3. Создайте папку c именем вашего домена (в нашем примере она будет называться "trackyourtime.ru"), которая будет являться корневой для нашего сервиса:
```
sudo mkdir -p /var/www/trackyourtime.ru
```
4. Поменяем владельца и группу папки для того, чтобы не root пользователи тоже имели полный доступ к ней (user поменять на имя вашего пользователя):
```
sudo chown user:user /var/www/trackyourtime.ru
```
5. Перейдем в папку сервиса  и скачаем репозиторий с GitHub:
```
cd /var/www/trackyourtime.ru
git clone https://github.com/timurash/timeTrackingService.git repository
```
6. Отредактируем файл "appsettings.json" для успешного доступа к БД.
Для этого откроем его в редакторе Nano:
```
cd repository/sources/PL
sudo nano appsettings.json
```
и для параметра "DefaultConnection" впишем строку:
```
Host = localhost; Port = 5432; Database = TimeTrackingServiceDB; Username = postgres; Password = 1234;
```
При этом заменив Username и Password на собственные. 
7. Запустим команду для сборки backend части сервиса:
```
sudo dotnet build
```
8. Запустим команду "publish", чтобы упаковать приложение в каталог, который сможет выполняться на сервере:
```
sudo dotnet publish
```
9. Теперь займемся сборкой frontend части сервиса. Перейдем в папку frontend/vue-bundle и установим все необходимые зависимости для запуска сборки frontend части:
```
cd ../../frontend/vue-bundle
sudo npm install --unsafe-perm
```
10. Теперь, когда все необходимые зависимости успешно установлены, мы можем собрать frontend build:
```
sudo npm run build
``` 
11. Воспользуемся функциональностью systemd, чтобы поддерживать сервис постоянно активным. Перейдем в директорию systemd и создадим новый файл сервиса systemd:
```
cd ~
cd /etc/systemd/system
sudo nano timeTrackingService.service
```
12. Добавим следующий текст в этот файл, заменив "user" на имя вашего пользователя:
```
[Unit]
Description=trackyourtime

[Service]
WorkingDirectory=/var/www/trackyourtime.ru/repository/sources/PL
ExecStart=/usr/bin/dotnet /var/www/trackyourtime.ru/repository/sources/PL/bin/Debug/n$
Restart=always
RestartSec=10
SyslogIdentifier=trackyourtime.ru
User=tima
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=true

[Install]
WantedBy=multi-user.target
```
13. Запустим новый сервис:
```
sudo systemctl enable timeTrackingService.service
sudo systemctl start timeTrackingService.service
```
14. Проверим состояние сервиса командой:
```
sudo systemctl status timeTrackingService.service
```
Если все шаги сделаны правильно, команда вернет результат наподобие следующего:
```
timeTrackingService.service - TimeTrackingService
Loaded: loaded (/etc/systemd/system/timeTrackingService.service; enabled; vendor preset: enabled)
Active: active (running) since Tue 2020-08-25 14:00:11 UTC; 5 days ago
```
15. Настроим Nginx с помощью сервер-блок файлов. Всего у нас будут настроены 2 файла конфигурации: для frontend приложения и для backend приложения. Для начала, сконфигурируем файл конфигурации для backend приложения. Перейдем в папку, содержащую файлы конфигурации и создадим новый файл конфигурации:
```
cd ~
cd /etc/nginx/sites-available
sudo nano api.trackyourtime.ru
```
И заменим содержимое файла на следующий текст:
```
server {
        server_name api.trackyourtime.ru www.api.trackyourtime.ru;

        location / {
             proxy_pass http://localhost:26038;
             proxy_http_version 1.1;
             proxy_set_header Upgrade $http_upgrade;
             proxy_set_header Connection keep-alive;
             proxy_set_header Host $host;
             proxy_cache_bypass $http_upgrade;
             proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
             proxy_set_header X-Forwarded-Proto $scheme;
        }
}
```
Теперь все запросы к домену api.trackyourtime.ru будут проксироваться на backend приложение

16. Настроим файл конфигурации для frontend приложения. Для начала, создадим его:
```
sudo nano trackyourtime.ru
```
А затем внесем в него следующее содержимое:
```
server {
    listen 80;
    listen [::]:80;

    server_name trackyourtime.ru www.trackyourtime.ru;
    error_page 404 /;

    location / {
        root /var/www/trackyourtime.ru/repository/frontend/vue-bundle/dist;
        index index.html;
        try_files $uri $uri/ /index.html;
    }
}
```
Теперь все запросы по адресу trackyourtime.ru будут обслуживаться frontend приложением

17. Перезагрузим Nginx:
```
sudo service restart nginx
```

Работу сервиса можно будет проверить, перейдя в браузере по адресу [trackyourtime.ru](http://trackyourtime.ru/).

## Установка сервиса с помощью Docker на ОС Linux
### Исходные данные
В данном примере мы будем использовать удаленный сервер на ОС Ubuntu.
Для развертывания данного сервиса нам понадобится:
1. Установленный Nginx сервер
2. Настроенные A домена, указывающие на публичный IP-адрес сервера
3. Установленный Docker и Docker Compose

### Процесс установки
1. Настроим прокси-сервер Nginx с помощью сервер-блок файла:
```
sudo nano /etc/nginx/sites-available/trackyourtime.ru
```
И заменим содержимое файла на следующий текст:
```
server {

    server_name trackyourtime.ruwww.trackyourtime.ru;

   location / {
     proxy_pass http://localhost:26038;
     proxy_http_version 1.1;
     proxy_set_header Upgrade $http_upgrade;
     proxy_set_header Connection keep-alive;
     proxy_set_header Host $host;
     proxy_cache_bypass $http_upgrade;
     proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
     proxy_set_header X-Forwarded-Proto $scheme;
    }
}
```
2. Скачаем репозиторий:
```
git clone https://github.com/timurash/timeTrackingService.git trackyourtime.ru
```
3. Настроем строку подключения к БД в файле appsettings.json.

Для этого откроем его в редакторе Nano:
```
sudo nano trackyourtime.ru/sources/PL/appsettings.json
```
и для параметра "DefaultConnection" впишем строку:
```
Host = postgres; Port = 5432; Database = TimeTrackingServiceDB; Username = postgres; Password = 1234;
```
При этом заменив Username и Password на собственные. 

4. Перейдем в папку sources и соберем проект:
```
cd trackyourtime.ru/sources
```
```
docker-compose build
```
5. Запустим проект:
```
docker-compose --project-name=trackyourtime --file=doscker-compose.yml up -d
```
6. Если все команды выполнены успешно, то после ввода команды:
```
docker ps
```
Вы увидите список, состоящий из двух запущенных контейнеров (pl -- сам REST API сервис и postgres -- база данных PostgreSQL). Работу сервиса можно будет проверить, перейдя в браузере по адресу [trackyourtime.ru](http://trackyourtime.ru/).