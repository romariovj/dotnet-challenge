# dotnet-challenge Project
Desafío técnico de una API Rest con arquitectura de N capas con enfoque en eñ dominio.


>## Patrones o Arquitectura utilizado

### Arquitectura N-Capas

La arquitectura N-Capas divide la aplicación en capas lógicas dentro de un mismo servidor, cada capa lógicas tiene una responsabilidad específica. En el caso del presente proyecto, se ha considerado las siguientes capas:

- **Capa de Presentación:** Responsable de la interfaz de usuario y la interacción con el usuario, en este caso la API Rest.
- **Capa de Aplicación:** Contiene la lógica de la aplicación y orquesta las interacciones entre la capa de presentación y la capa de dominio.
- **Capa de Dominio:** Enfocada en la lógica empresarial y los modelos de dominio.
- **Capa de Infraestructura:** Maneja detalles técnicos como acceso a datos, servicios externos, etc.

Asimismo, se utilizó los siguiente patrones:
### Patrón Mediator
Se utilizó el patrón Mediator para desacoplar los componentes del sistema y facilitar la comunicación entre ellos. El Mediator actúa como un intermediario, permitiendo que los diferentes componentes se comuniquen sin conocerse entre sí directamente.

### Patrón CQRS (Command Query Responsibility Segregation)

La arquitectura CQRS separa las operaciones de lectura (consultas) de las operaciones de escritura (comandos). Esto proporciona flexibilidad al modelar la lógica de la aplicación y permite escalar las operaciones de lectura y escritura de manera independiente.
En el presente proyecto los Commnands y Queries estan alojandos en la capa Aplicación.

### Patrón Repository

Se utilizó el patrón Repository para abstraer el acceso a la capa de almacenamiento de datos. Esto facilita la gestión de la persistencia de datos y permite cambiar la fuente de datos sin afectar la lógica de negocio.
En el presente proyecto la interfaz de repositorio se definio en la capa Dominio y se implemento en la capa Infraestructura.

### Inyección de Dependencias

Se adoptó el principio de Inyección de Dependencias (DI) para gestionar las dependencias entre los componentes del sistema. Esto mejora la modularidad, la prueba unitaria y facilita la sustitución de implementaciones.
En cada capa de la arquitectura se creo un método estático en donde defino las dependencias Interfaz-Clase.


### Ejemplo de Estructura de Carpetas

- **/Api:** Capa de presentación con controladores y Middlewares.
- **/Application:** Capa de aplicación con Commands y Queries.
- **/Domain:** Capa de dominio con modelos de dominio.
- **/Infrastructure:** Capa de infraestructura con implementaciones de repositorios, servicios externos, etc.



>## Levantar el Proyecto Localmente

Sigue estos pasos para ejecutar el proyecto en tu máquina local:

1. Clona este repositorio: `git clone https://github.com/romariovj/dotnet-challenge.git` o descarlo.
2. Navega al directorio del proyecto: `cd dotnet-challenge`
3. Instala las dependencias: `dotnet restore`
4. Construye los proyectos: `dotnet build`
5. Ejecutar el proyecto: `dotnet run -p src\DotnetChallenge.Api`
6. Abrir el postman y probar los endpoint:
-**GET** `http://localhost:5249/api/products`
-**GET** `http://localhost:5249/api/products/{id}`
-**POST** `http://localhost:5249/api/products`
-**PUT** `http://localhost:5249/api/products`

* En caso no utilizar la consola CMD, se puede abrir el proyecto desde Visual Studio 2022.

>## Información Adicional
1. URL de servicio de descuentos: `https://6593b8bd1493b0116068fdea.mockapi.io/api/v1/discounts`
2. Ruta de archivos log de tiempo de respuesta por request:
-`dotnet-challenge\src\DotnetChallenge.Api\bin\Debug\net8.0\logs` o
-`dotnet-challenge\src\DotnetChallenge.Api\bin\Release\net8.0\logs`


