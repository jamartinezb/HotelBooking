# HotelBooking
Hotel Booking API es un servicio backend diseñado para gestionar reservas de hotel.

# Hotel Booking API

## Descripción

Hotel Booking API es un sistema de gestión de reservas que permite a los usuarios buscar y reservar habitaciones de hotel. La API maneja operaciones de reservas, gestión de habitaciones y notificaciones por correo electrónico.

## Características

- Búsqueda de hoteles y habitaciones disponibles.
- Reservas de habitaciones y gestión de estadías.
- Envío de confirmaciones de reserva por correo electrónico.
- Gestión de habitaciones y hoteles.

## Tecnologías Utilizadas

- .NET 6
- Entity Framework Core
- SQL Server
- SMTP para envío de correos electrónicos
- RestSharp (para integraciones de servicios RESTful)

## Patrones de Diseño

- Patrón Repositorio para abstracción de acceso a datos.
- Inyección de Dependencias para desacoplamiento y gestión de dependencias.
- DTOs para la transferencia segura de datos entre la API y los clientes.
- Servicios para encapsular la lógica de negocio.

## Instalación

Describir los pasos para instalar y configurar el proyecto localmente.

```bash
git clone https://github.com/your-username/hotel-booking-api.git
cd hotel-booking-api
dotnet ef database update
