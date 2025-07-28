# ThothShop

## Overview
ThothShop is a modern, full-featured e-commerce platform built with ASP.NET Core. It provides a robust backend API for managing books, users, orders, payments, and more. The platform is designed for scalability, security, and extensibility, making it ideal for online bookstores and similar digital commerce solutions.

---

## Key Features

### 🛡️ Authentication & Authorization
- Secure JWT-based authentication
- Role-based access control (User, Admin, Owner)
- Password reset, email confirmation, and refresh token support

### 📚 Product Catalog
- Manage books, categories, authors, and images
- Advanced search and filtering
- Book details, stock management, and view tracking

### 📚 BookShelf (Used Book Marketplace)
- A peer-to-peer book listing platform inside ThothShop
- Users can list their used books for others to browse
- Support for adding book condition, price, and images
- View listings posted by other users
- Helping in Contact the seller privately OutSide the App

### 🛒 Shopping Cart
- Add, update, and remove items
- View cart contents and total price
- Cart item quantity management
- Clear cart functionality

### 💖 Wishlist
- Add/remove books to wishlist
- View and clear wishlist
- Prevent duplicate wishlist entries

### 📦 Orders
- Place orders from cart
- View order history and order details
- Order status tracking (Pending, Processing, Shipped, Completed, Cancelled)
- Admin order management (update status, delete)

### ⭐ Reviews
- Add, update, and delete book reviews
- View reviews for each book
- Average rating calculation

### 💳 Payments
- Stripe integration for secure checkout
- Payment status tracking (Pending, Completed, Failed)
- Refund support (extensible)

### 🛠️ Admin & Extensibility
- Admin endpoints for managing users, books, categories, authors, and orders
- Modular architecture for easy feature addition
- Clean separation of concerns (API, Core, Domain, Infrastructure, Service)
- CQRS with MediatR and AutoMapper
- FluentValidation for all commands and queries

### 📈 Reporting & Analytics (Extensible)
- Revenue calculation
- Order and payment status reports

---

## Technologies Used
- ASP.NET Core 7+
- Entity Framework Core
- SQL Server
- MediatR (CQRS)
- AutoMapper
- FluentValidation
- Stripe API

---

## Project Structure
```
ThothShop/
├── ThothShop.Api/           # API controllers and endpoints
├── ThothShop.Core/          # Application logic, CQRS handlers, DTOs, Validators
├── ThothShop.Domain/        # Entity models, enums, interfaces
├── ThothShop.Infrastructure/# EF Core repositories, data context
├── ThothShop.Service/       # Service contracts and implementations
└── README.md
```

---

## Getting Started

### Prerequisites
- .NET 7.0 SDK or later
- SQL Server
- Visual Studio 2022 or VS Code
- Stripe account (for payments)

### Installation
1. Clone the repository
   ```bash
   git clone https://github.com/KyRilloSmaher/ThothShop_BookStore.git
   cd ThothShop
   ```
2. Restore dependencies
   ```bash
   dotnet restore
   ```
3. Update database
   ```bash
   dotnet ef database update
   ```
4. Run the application
   ```bash
   dotnet run
   ```

The API will be available at `https://localhost:5001` or `http://localhost:5000`

---

## Configuration
- Update the connection string in `appsettings.json`
- Configure authentication and Stripe settings
- Set up environment variables as needed

---

## API Documentation
- See [`ThothShop.Api/README.api-endpoints.md`](ThothShop.Api/README.api-endpoints.md) for a full list of endpoints, request/response examples, and integration notes for frontend teams.

---

## Contributing
1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

---

## Support
For support, please open an issue in the GitHub repository or contact the development team.

---

Made with ❤️ by [Kyrillos Maher]