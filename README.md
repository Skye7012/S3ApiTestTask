<p>
    <h1 align="center">S3ApiTestTask</h1>
</p>

<p align="center">
    Web API для тестового задания на ASP.NET 6, работающий с S3
</p>

<p align="center">
  <img src="https://img.shields.io/static/v1?label=&message=c%23&style=flat-square&color=0000ff"
      height="40">
  <img src="https://img.shields.io/badge/ASP.NET-purple?style=flat-square"
      height="40">
  <img src="https://img.shields.io/static/v1?label=&message=Entity-Framework&style=flat-square&color=blueviolet"
      height="40">
    <img src="https://img.shields.io/static/v1?label=&message=MinIO&style=flat-square&color=d10000"
      height="40">
  <img src="https://img.shields.io/static/v1?label=&message=PostgreSql&style=flat-square&color=1A5276&logo=postgresql&logoColor=white"
      height="40">
  <img src="https://img.shields.io/static/v1?label=&message=Swagger&style=flat-square&color=green&logo=swagger&logoColor=white"
      height="40">
  <img src="https://img.shields.io/static/v1?label=&message=MediatR&style=flat-square&color=blue"
      height="40">
</p>



# Table Of Contents

- [Table Of Contents](#table-of-contents)
- [ТЗ](#тз)
- [Общее описание](#общее-описание)
- [Реализация](#реализация)
    - [Ссылка на загрузку](#ссылка-на-загрузку)
    - [Скачивание файла](#скачивание-файла)
    - [TODO](#todo)
- [Локальный запуск](#локальный-запуск)



# ТЗ
![image](assets/ТЗ.png)



# Общее описание
Реализован API на ASP.NET 6  

Реализована поддержка docker-compose [(см. "Локальный запуск")](#локальный-запуск)  

В качестве s3 совместимого хранилища используется `MinIO`  
API задокументирован с помощью `Swagger`  
Проект структурирован по принципам `clean architecture`  
Используется `CQRS` через [`MediatR`](https://github.com/jbogard/MediatR)  
В качестве ORM используется `Entity Framework Core`, в качестве СУБД `PostgreSql`  
  



# Реализация
### Ссылка на загрузку  
- При запросе ссылки на загрузку файла генерируется presigned URL и создается file в БД  
  
### Скачивание файла
- Скачивание происходит по постоянной ссылке `/AppFile/Download/{file_id}`  
- Если файл был загружен в s3, то генерируется presigned Url для скачивание файла, на которую редиректится клиент
- Если файл не был загружен в s3, то выкидывается ошибка
- Если файл не был загружен в s3 и время жизни ссылки на загрузку закончилось, файл удаляется из БД

### TODO
- асинхронное создание файла в БД при срабатывание event на загрузку файла в MinIO



# Локальный запуск
- `git clone https://github.com/Skye7012/S3ApiTestTask.git`

- `cd S3ApiTestTask`

- `docker-compose build`

- `docker-compose up`

- **Swagger**: [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)

- **MinIO**: [http://localhost:9003/login](http://localhost:9003/login)
  
<br/>
  
Volumes для БД будет создан на уровень выше корневой директории

