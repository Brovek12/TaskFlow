


https://github.com/user-attachments/assets/fc3ba93b-5426-4f22-a245-f3be35adbc06






# TaskFlow

Fullstack task management app — **ASP.NET Core 8 Web API + Blazor WebAssembly + MySQL**, dengan JWT authentication dan role-based access control (RBAC).

> Project ini saya bangun untuk transisi dari **Laravel → .NET**. Pemetaan konsep ada di [bawah](#-laravel--net-mapping).

---

## ✨ Fitur

### 🔐 Authentication & Authorization
- Register & login dengan **JWT** (BCrypt untuk hash password)
- **Role-based access control** — User & Admin
- User pertama otomatis jadi Admin
- Endpoint admin diproteksi `[Authorize(Roles = "Admin")]`

### ✅ Task Management
- CRUD task lengkap (Create, Read, Update, Delete)
- **Data isolation** — setiap user hanya bisa lihat & kelola task miliknya sendiri
- **Search** task berdasarkan judul/deskripsi
- **Filter** status: Semua / Aktif / Selesai
- Toggle selesai dengan checkbox
- Dialog create/edit dengan **MudBlazor**

### 📊 Dashboard
- Statistik personal: Total Task, Selesai, Pending, Progress %
- List 5 task terbaru
- Greeting user

### 🛡️ Admin Panel
- Statistik global: Total User, Total Task, Selesai, Pending
- Tabel user dengan role & jumlah task
- Tabel semua task (lintas user) dengan owner
- Hapus user dengan **confirmation dialog**
- Search di kedua tabel

### 🎨 UI/UX
- **MudBlazor** components — Material Design
- Sidebar collapsible dengan icon
- Responsive — mobile friendly
- **Snackbar** notifikasi
- **Loading states** & empty states

---

## 🛠️ Tech Stack

| Layer | Teknologi |
|---|---|
| Backend | ASP.NET Core 8 Web API |
| Frontend | Blazor WebAssembly (.NET 8) |
| UI Library | MudBlazor (Material Design) |
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
