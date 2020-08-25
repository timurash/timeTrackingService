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
Для развертывания данного сервиса нам понадобится:
1. Установленный Nginx сервер с отдельным сервер-блоком
2. Настроенные A домена, указывающие на публичный IP адрес сервера
3. Установленный Postgres

Перейдем непосредственно к установке сервиса:

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
2. Создайте папку c именем вашего домена (в нашем примере она будет называться "your_domain"), которая будет являться корневой для нашего сервиса
```
sudo mkdir -p /var/www/your_domain
```
3. Поменяем владельца и группу папки для того, чтобы не root пользователи тоже имели полный доступ к ней (user поменять на имя вашего пользователя):
```
sudo chown user:user /var/www/you_domain
```
4. Перейдем в папку пользователя и скачаем репозиторий с GitHub:
```
cd /home/user
git clone https://github.com/timurash/timeTrackingService.git timeTrackingService
```
5. Отредактируем файл "appsettings.json" для успешного доступа к БД.
Для этого откроем его в редакторе Nano:
```
sudo nano timeTrackingService/sources/PL/appsettings.json
```
и для параметра "DefaultConnection" впишем строку:
```
Host = localhost; Port = 5432; Database = TimeTrackingServiceDB; Username = postgres; Password = 1234;
```
При этом заменив Username и Password на собственные. 
6. Перейдем в директорию уровня представления -- "PL" и запустим команду для сборки:
```
cd timeTrackingService/sources/PL
dotnet build
```
7. Запусим команду "publish", чтобы упаковать приложение в каталог, который может выполняться на сервере:
```
dotnet publish
```
8. Перенесем полученные файлы в корневую папку нашего сервиса
```
cd ~
mv -v ~/home/user/timeTrackingService/sources/PL/bin/Debug/netcoreapp3.1/publish/* ~/var/www/your_domain
```
9. Настроим прокси-сервер Nginx с помощью сервер-блок файла:
```
sudo nano /etc/nginx/sites-available/your_domain
```
И заменим содержимое файла на следующий текст, при этом заменив "your_domain" на имя вашего домена:
```
server {

    server_name your-domain  www.your-domain;

   location / {
     proxy_pass http://localhost:5000;
     proxy_http_version 1.1;
     proxy_set_header Upgrade $http_upgrade;
     proxy_set_header Connection keep-alive;
     proxy_set_header Host $host;
     proxy_cache_bypass $http_upgrade;
     proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
     proxy_set_header X-Forwarded-Proto $scheme;
    }
}

server {
if ($host = www.your-domain) {
    return 301 https://$host$request_uri;
} # managed by Certbot


if ($host = your-domain) {
    return 301 https://$host$request_uri;
} # managed by Certbot


    listen 80;
    listen [::]:80;

    server_name your-domain  www.your-domain;
return 404; # managed by Certbot
}
```

10. Перегзагрузим Nginx:
```
sudo nginx -s reload
```

11. Воспользуемся функциональностью systemd, чтобы поддерживать сервис постоянно активным. Перейдем в директорию systemd и создадим новый файл сервиса systemd:
```
cd /etc/systemd/system
sudo nano timeTrackingService.service
```
12. Добавим следующий текст в этот файл, заменив "your_domain" на имя вашего домена, а "user" на имя вашего пользователя:
```
[Unit]
Description=TimeTrackingService

[Service]
WorkingDirectory=/var/www/your_domain
ExecStart=/usr/bin/dotnet /var/www/your_domain/PL.dll
Restart=always
RestartSec=10
SyslogIdentifier=timeTrackingService
User=user
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```
13. Запустим новый сервис:
```
sudo systemctl enable timeTrackingService.service
sudo systemctl start timeTrackingService.service
```
14. Проверим сосотяние сервиса командой:
```
sudo systemctl status timeTrackingService.service
```
Если все шаги сделаны правильно, сервис должен заработать по адресу вашего домена.