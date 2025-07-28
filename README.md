# ThothShop

## Overview
ThothShop is an e-commerce platform built with ASP.NET, designed to provide a seamless online shopping experience. Named after Thoth, the ancient Egyptian deity of wisdom and knowledge, this platform aims to deliver a smart and efficient shopping solution.

## Features
- User authentication and authorization
- Product catalog with categories
- Shopping cart functionality
- Secure checkout process
- Order management
- User profile management

## Technologies Used
- ASP.NET Core
- Entity Framework Core
- SQL Server


## Prerequisites
- .NET 7.0 SDK or later
- SQL Server
- Visual Studio 2022 or VS Code

## Getting Started

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

The application will be available at `https://localhost:5001` or `http://localhost:5000`

## Project Structure
```
ThothShop/
├── src/
│   ├── ThothShop.Api/                 # Endpoints
│   ├── ThothShop.Core/                # Core business logic
│   ├── ThothShop.Infrastructure/      # Data access and external services
│   └── ThothShop.Domain/              # Core Models
│   └── ThothShop.Service/             # business logic
└── README.md
```

## Configuration
1. Update the connection string in `appsettings.json`
2. Configure authentication settings
3. Set up environment variables as needed

## Contributing
1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request



## Support
For support, please open an issue in the GitHub repository or contact the development team.



---
Made with ❤️ by [Kyrillos Maher]