# Proyecto Base .NET Core - Arquitectura en Cebolla 🧅

Este proyecto es una base desarrollada en **.NET Core** siguiendo el patrón de **Arquitectura en Cebolla (Onion Architecture)**.  
Su propósito es servir como plantilla para el desarrollo de aplicaciones modulares, escalables y fáciles de mantener.

---

## Tecnologías utilizadas

- [.NET Core](https://dotnet.microsoft.com/) 6/7/8 (ajusta según tu versión)
- C#
- Entity Framework Core
- SQL Server (puede cambiarse por otro motor)
- Swagger (documentación de API)
- Inyección de dependencias nativa de .NET
- Arquitectura en capas (Domino, Aplicación, Infraestructura, API)

---

## Estructura del proyecto

```
ProyectoBaseNet/
│── ProyectoBaseNet.API        → Capa de presentación (controllers, endpoints, Swagger)
│── ProyectoBaseNet.Application → Casos de uso, servicios de aplicación, DTOs
│── ProyectoBaseNet.Domain      → Entidades de dominio, interfaces base
│── ProyectoBaseNet.Infrastructure → Repositorios
```

---

## ⚙️ Configuración

1. Clonar el repositorio:
   ```bash
   git clone https://github.com/usuario/ProyectoBaseNet.git
   ```


## Endpoints

Una vez corriendo, puedes ver la documentación en Swagger:  
[http://localhost:5000/swagger](http://localhost:5000/swagger)

---




## Autor

Proyecto desarrollado por **YILBER MOLINA DEVOZ**  
Contacto: yilbersilo@gmail.com
