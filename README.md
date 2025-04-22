# 💊 Apotek Backend API

## Overview

Backend aplikasi Apotek berbasis **.NET 8 Web API**, menggunakan **PostgreSQL** sebagai database utama. Mendukung autentikasi **JWT**, dokumentasi API menggunakan **Swagger**, dan fitur seeding data awal (user, kategori, satuan, dll).

## 🛠 Tech Stack

- ✅ .NET 8 Web API
- ✅ PostgreSQL
- ✅ Entity Framework Core
- ✅ JWT Authentication
- ✅ Swagger (API Docs)
- ✅ Auto Seeding

## ✅ Prerequisites

Pastikan kamu sudah menginstall:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Git](https://git-scm.com/)
- [dotnet-ef CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) (untuk migrasi)
- [Postman](https://www.postman.com/) (untuk testing API)

## 📥 Setup Instructions

### 1. Clone Repository

```bash
git clone https://github.com/arbim09/BackEndApotekGabut.git
cd BackEndApotekGabut
```

### 2. Create PostgreSQL Database

Access your PostgreSQL instance and create a new database:

```sql
CREATE DATABASE apotekdb;
```

### 3. Configure Database Connection

Edit the `appsettings.json` file to configure the database connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=apotekdb;Username=postgres;Password=your_password"
}
```

### 4. Migrate & Seed Database

Run the following command to apply migrations and seed the database:

```bash
dotnet ef database update
```

### 5. Run the Application

Start the application using:

```bash
dotnet run
```

## Additional Information

- Ensure that your PostgreSQL server is running before attempting to connect.
- Replace `your_password` in the connection string with your actual PostgreSQL password.
- The API documentation will be available at `https://localhost:5001/swagger` when the application is running.

## Contributing

Feel free to contribute to this project by submitting issues or pull requests.
