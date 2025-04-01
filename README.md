# 🛒 ynet – E-handelsapplikation (.NET 8 + Blazor WebAssembly)

Welcome to **ynet** (yes, obviously a joke – if you know, you know 😉) – a complete e-commerce solution built with **ASP.NET Core Web API** and **Blazor WebAssembly**. This project includes full product management, customer registration, order handling, and an admin panel secured with role-based authentication using JWT.  
⚠️ Just a heads-up: the app isn't fully responsive – so maybe don’t play too much with the screen size, alright? 😅

## 🚀 Functions

### 👥 Customer

- Register an account and log in
- Look at and update profile
- Place products in cart
- Order products (Create order)
- Look at order history (On profile page)

### 🔧 Admin

- View and manage all products (CRUD)
- Mark products as Discontinued or Out of Stock
- View all customers and their profiles
- View all orders and update order status (Unhandled → Packing → Sent → Delivered)

### 🛠 Backend (API)

- RESTful API built in .NET 8
- Repository Pattern
- JWT-authentication
- Rollbased access controll

---

## 🧰 Technologies

- **ASP.NET Core Web API** (backend)
- **Blazor WebAssembly** (frontend)
- **Entity Framework Core** + SQL Server
- **JWT Authentication**
- **Automapper**
- **Blazored.LocalStorage**
- **Bootstrap 5** (UI)

---

## 📦 Installation & Setup

### 🔧 Backend

1. Clone the project:
   ```bash
   git clone https://github.com/AdamAgerling/ynetFullstackWebstore
   cd ynet
   ```
2. Navigate to the backend project:
   ```bash
   cd Labb2Webb
   ```
3. Create and migrate the database:
   ```bash
   dotnet ef database update
   ```
4. Open Configure Startup Projects and set Labb2Webb action to Start then do the same with BlazorFrontend. Then you just run the program like normal.
   - ⚠️IF you do this, you don't have to do step 5. And you can skip step 1 and 2 in Frontend (blazor)⚠️
   
6. Start the API-server:
   ```bash
   dotnet run
   ```
   #### The backend is running on https://localhost:7278

### 🖥️ Frontend (Blazor)

1. Navigate to the fronted-project

   ```bash
   cd BlazorFrontend
   ```

2. Start the Blazor client

   ```bash
   dotnet run
   ```

   #### The backend is running on https://localhost:7245

## 🔐 Authentication

- #### When loggin in a JWT-Token is sent and stored in localStorage.
- #### The tokens are used in HTTP-requests with.
  ```cs
  Authorization: Bearer {token}
  ```
- #### The admin role is required to access the adminpanel and the protected endpoints.

## 📘 API-documentation

    Look at the API_Specification.md in the project root map, for a full specification of all the endpoints, methods and Authorization requirements.

## 🧪 Test users

```txt
    Admin:
    Email: admin@admin.com
    Passowrd: 1234

    Customer:
    Email: e@e.se
    Password: test123
```

There are more, but you may freely create however many you'd like :)

## 📄 License

    MIT License
