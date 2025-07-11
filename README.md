# OrderFlow

**OrderFlow** is a microservice-based order management system built with **.NET 8**, **Docker**, and **event-driven architecture** principles. It is designed as a learning project to practice microservices, clean architecture, message brokers, relational databases, and containerized development workflows.

---

## 🧠 Project Overview

OrderFlow is composed of independently deployable services that communicate through events (planned: via RabbitMQ). Each service is responsible for a specific business capability, starting with:

- `OrderService`: Handles creation and retrieval of customer orders.

This project is intended for learning purposes and will evolve to include more services such as PaymentService, InventoryService, and NotificationService.

---

## 🚀 Technologies Used

- [.NET 8](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- SQLite (for simplicity during development)

---

## 📦 Prerequisites

Make sure the following are installed on your system:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [Git](https://git-scm.com/)
- (Optional) [HTTPie](https://httpie.io/) for testing APIs easily

---

## 🔧 Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/OrderFlow.git
cd OrderFlow
```
