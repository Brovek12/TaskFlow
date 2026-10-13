

# TaskFlow

Fullstack task management app — **ASP.NET Core 8 Web API + Blazor WebAssembly + MySQL**, dengan JWT authentication.

> Project ini saya bangun untuk transisi dari **Laravel → .NET**. Pemetaan konsep ada di [bawah](#-laravel--net-mapping).

---

## ✨ Fitur

- 🔐 **Auth** — Register & login dengan JWT (BCrypt untuk hash password)
- ✅ **Task CRUD** — Create, Read, Update, Delete task
- 👤 **User Isolation** — Setiap user hanya bisa melihat & mengelola task miliknya sendiri
- 📊 **Dashboard** — Statistik total, selesai, dan pending task
- 🎨 **Blazor UI** — Frontend interaktif dengan sidebar + dashboard
- 🧪 **Unit Tests** — xUnit untuk service & data layer
- 📘 **Swagger** — Dokumentasi API interaktif

---

## 🛠️ Tech Stack

| Layer | Teknologi |
|---|---|
| Backend | ASP.NET Core 8 Web API |
| Frontend | Blazor WebAssembly (.NET 8) |
| ORM | Entity Framework Core 8 |
| Database | MySQL (via XAMPP) |
| Auth | JWT Bearer + BCrypt |
| Testing | xUnit |
| API Docs | Swagger / OpenAPI |

---

## 🚀 Cara Menjalankan

### Prasyarat

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [XAMPP](https://www.apachefriends.org/) (untuk MySQL)
- (Opsional) [VS Code](https://code.visualstudio.com/) + C# Dev Kit

### 1. Setup Database

1. Buka **XAMPP Control Panel** → Start **MySQL**
2. Buka `http://localhost/phpmyadmin`
3. Buat database baru bernama **`TaskFlow`**

### 2. Jalankan Backend API

```bash
cd src/TaskFlow.Api
dotnet ef database update
dotnet run
