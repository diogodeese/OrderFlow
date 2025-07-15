# OrderFlow

**OrderFlow** is a microservice-based order management system built with **.NET 8**, **Docker**, and **RabbitMQ** using **event-driven architecture** principles. It is designed as a hands-on learning project for practicing microservices, clean architecture, message-driven communication, relational databases, and containerized development workflows.

---

## 🧠 Project Overview

OrderFlow is composed of independently deployable services that communicate through RabbitMQ events. Each microservice handles a dedicated business capability. So far, the project includes:

### 🧾 OrderService

- Handles order creation, retrieval, update, and deletion.
- Persists order data in a local SQLite database.
- Publishes `OrderCreated` events to RabbitMQ when an order is successfully created.

### 📦 ProductService

- Manages products, including name, price, and stock.
- Persists product data in a local SQLite database.
- Will soon subscribe to `OrderCreated` events to update stock (event-driven consumer coming soon).

Future plans include:

- PaymentService for processing payments
- InventoryService for more advanced stock management
- NotificationService to alert users

---

## 🚀 Technologies Used

- [.NET 8](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/)
- [RabbitMQ](https://www.rabbitmq.com/)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- [SQLite](https://www.sqlite.org/index.html)

---

## 📦 Prerequisites

Ensure the following are installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/)
- [Git](https://git-scm.com/)
- (Optional) [HTTPie](https://httpie.io/) or Postman for testing HTTP requests

---

## ⚙️ Configuration

The services and dependencies run with Docker Compose. Each service exposes its own HTTP API:

| Service        | URL                    |
| -------------- | ---------------------- |
| OrderService   | http://localhost:5064  |
| ProductService | http://localhost:5070  |
| RabbitMQ (UI)  | http://localhost:15672 |

RabbitMQ UI credentials:

- **Username:** guest
- **Password:** guest

---

## 🛠️ Running the Project

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/OrderFlow.git
cd OrderFlow
```
