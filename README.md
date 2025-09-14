# 🛒 Cross-Platform E-Commerce Application

<!-- Badges -->
![Build](https://img.shields.io/badge/build-passing-brightgreen)
![License](https://img.shields.io/badge/license-MIT-blue.svg)
![Spring Boot](https://img.shields.io/badge/Spring%20Boot-2.7.0-brightgreen?logo=springboot)
![Xamarin](https://img.shields.io/badge/Xamarin-Forms-blue?logo=xamarin)
![Platform](https://img.shields.io/badge/platform-Android%20%7C%20iOS-lightgrey?logo=android&logoColor=green)

A modern e-commerce solution built with **Spring Boot** backend and **Xamarin** mobile frontend, providing a seamless shopping experience across multiple platforms.

## 🏗️ Architecture

- **Backend**: Spring Boot (Java) RESTful API
- **Frontend**: Xamarin.Forms (C#) cross-platform mobile app
- **Database**: SQL Database with JPA/Hibernate
- **Authentication**: JWT-based security

## 🚀 Features

### 📱 Mobile App (Xamarin)
- ✅ User Authentication (Login/Signup)
- 🛍️ Product Browsing & Search
- 📦 Category Management
- 🛒 Shopping Cart Functionality
- 📋 Order Management
- 👨‍💼 Admin Panel for Product Management

### 🔧 Backend API (Spring Boot)
- 🔐 JWT Authentication & Authorization
- 👤 User Management
- 📦 Product & Category CRUD Operations
- 🛒 Cart Management
- 📋 Order Processing
- 🔒 Role-based Access Control

## 📁 Project Structure

```
📂 Backend_SpringBoot/
├── src/main/java/com/example/demo/
│   ├── 🏠 ECommerceApplication.java
│   └── Main/
│       ├── 🎛️ Controller/
│       ├── 📊 Entity/
│       ├── 🗃️ Repository/
│       ├── 🔒 Security/
│       ├── ⚙️ Service/
│       └── 🧩 Components/
└── src/main/resources/
└── application.properties

📂 Ecommerce1/
├── Ecommerce1/
│   ├── 📱 Pages (LoginPage, MainPage, etc.)
│   ├── 📋 Model/
│   └── 🔧 Services/
└── Ecommerce1.Android/
```

## 🛠️ Technologies Used

### Backend
- ☕ **Java** with **Spring Boot**
- 🗄️ **Spring Data JPA** for database operations
- 🔐 **Spring Security** with JWT
- 📦 **Maven** for dependency management

### Frontend
- 🎯 **C#** with **Xamarin.Forms**
- 📱 Cross-platform mobile development
- 🔗 HTTP client for API communication

## ⚡ Getting Started

### Prerequisites
- ☕ JDK 11 or higher
- 📦 Maven
- 🎯 .NET Framework/.NET Core
- 📱 Xamarin development environment

### 🚀 Running the Backend

1. Navigate to the backend directory:
   ```bash
   cd Backend_SpringBoot
   ```

2. Run the Spring Boot application:
   ```bash
   ./mvnw spring-boot:run
   ```

3. The API will be available at `http://localhost:8080`

### 📱 Running the Mobile App

1. Navigate to the Xamarin project:
   ```bash
   cd Ecommerce1
   ```

2. Open `Ecommerce1.sln` in Visual Studio

3. Build and run the application on your preferred platform (Android/iOS)

## 📋 API Endpoints

- 🔐 **Auth**: `/api/auth/*` - Authentication endpoints
- 👤 **Users**: `/api/users/*` - User management
- 📦 **Products**: `/api/products/*` - Product operations
- 📂 **Categories**: `/api/categories/*` - Category management
- 🛒 **Cart**: `/api/cart/*` - Shopping cart
- 📋 **Orders**: `/api/orders/*` - Order processing

## 🔧 Configuration

### Backend Configuration
Update `application.properties` with your database and JWT settings:
```properties
# Database configuration
spring.datasource.url=your-database-url
spring.datasource.username=your-username
spring.datasource.password=your-password

# JWT configuration
app.jwtSecret=your-secret-key
app.jwtExpirationInMs=86400000
```

### Mobile App Configuration
Update API base URL in your service classes to point to your backend server.

## 👥 Contributing

1. 🍴 Fork the repository
2. 🌟 Create your feature branch (`git checkout -b feature/amazing-feature`)
3. 💾 Commit your changes (`git commit -m 'Add some amazing feature'`)
4. 🚀 Push to the branch (`git push origin feature/amazing-feature`)
5. 🔄 Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 📧 Contact

For any questions or support, please contact the development team.

---

⭐ **Star this repository if you found it helpful!**

This README provides a comprehensive overview of your project with clear structure, setup instructions, and visual appeal using emojis to make it more engaging and easier to navigate.
