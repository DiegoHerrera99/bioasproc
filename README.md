# Proyecto Full-Stack con Ionic y .NET Core 7

Este proyecto es una aplicación móvil construida con Ionic, junto con un backend dockerizado que utiliza .NET Core 7, Memcached y MariaDB. A continuación, se detallan los pasos necesarios para poner en marcha tanto la aplicación móvil como el backend.

## Estructura del Proyecto

- `/app`: Contiene la aplicación móvil desarrollada en Ionic.
- `/backend`: Contiene el backend que utiliza .NET Core 7, Memcached y MariaDB. El backend está configurado para correr en contenedores Docker.

## Requisitos Previos

- [Node.js](https://nodejs.org) (incluye npm)
- [Ionic CLI](https://ionicframework.com/getting-started#cli)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

## Configuración Inicial

Antes de levantar la aplicación y el backend, es necesario configurar los archivos de variables de entorno:

1. En el directorio `/backend`, renombra el archivo `appsettings.json.example` a `appsettings.json`. Completa este archivo con tus valores reales.
   
   ```bash
   mv /backend/appsettings.json.example /backend/appsettings.json
