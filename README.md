# FameFinds – Location-Based Shop Discovery Platform

**Connecting local artisans with customers through intelligent location-based discovery**

FameFinds is a comprehensive web platform that bridges the gap between local businesses and customers by showcasing city-specific products, cultural specialties, and artisan crafts. The application empowers shop owners with digital visibility while providing customers with an intuitive map-based discovery experience.

---

## ✨ Core Value Proposition

- **For Customers:** Discover authentic local products, hidden gems, and famous specialties in any city through interactive map-based exploration
- **For Shop Owners:** Gain digital presence, reach new customers, and showcase products without complex e-commerce infrastructure
- **For Communities:** Preserve cultural heritage and support local economies through improved business visibility

---

## 🎯 Key Features

### Authentication & User Management
- **OTP-based Registration:** Secure account creation with email verification
- **Dual Role System:** Separate experiences for Shop Owners and Customers
- **JWT Authentication:** Stateless, secure session management
- **Profile Management:** Comprehensive user profile customization

### Shop Discovery & Management
- **Interactive Map Integration:** Visual shop discovery using Google Maps
- **Pinpoint Accuracy:** Add shop locations by selecting exact coordinates on the map
- **Advanced Search:** Filter by category, distance, ratings, and product types
- **Category Organization:** Structured browsing by product type and specialty
- **Real-time Navigation:** Direct Google Maps integration for directions

### Social Proof & Engagement
- **Rating System:** Multi-factor shop and product ratings
- **Customer Reviews:** Detailed feedback and testimonials
- **Photo Uploads:** Visual product showcases
- **Verified Purchases:** Authentic review indicators

### Technical Excellence
- **Global Exception Handling:** Graceful error management across the application
- **Request Validation:** Comprehensive input sanitization and validation
- **Optimized Data Access:** Hybrid EF Core and Dapper approach for performance
- **Responsive Design:** Seamless experience across devices
- **Email Notifications:** Automated communication system

---

## 🏛️ Architecture

### Monolithic Layered Architecture

```
┌─────────────────────────────────────────┐
│          Presentation Layer             │
│     (Angular SPA + API Controllers)     │
└─────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────┐
│          Business Logic Layer           │
│         (Services & Validators)         │
└─────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────┐
│          Data Access Layer              │
│      (Repositories + EF Core/Dapper)    │
└─────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────┐
│            SQL Server Database          │
└─────────────────────────────────────────┘
```

**Design Principles:**
- **Separation of Concerns:** Clear boundaries between layers
- **Dependency Injection:** Loose coupling and testability
- **Repository Pattern:** Abstracted data access
- **Service Layer:** Centralized business logic
- **DTO Pattern:** Clean data transfer between layers

---

## 📊 Application Modules

### 1. User Module
- Multi-factor authentication with OTP verification
- Role-based authorization (Shop Owner/Customer)
- Email verification workflows
- User profile management and preferences
- Password reset functionality

### 2. Shop Module
- Shop registration with business details
- Interactive map-based location selection
- Category and subcategory management
- Operating hours and contact information
- Product catalog management
- Shop profile customization

### 3. Search & Discovery Module
- Geolocation-based shop discovery
- Radius-based proximity search
- Advanced filtering (category, rating, distance, price range)
- Full-text search capabilities
- Trending and popular shops
- Recently added listings

### 4. Review & Rating Module
- Five-star rating system
- Detailed written reviews
- Photo attachments in reviews
- Review helpfulness voting
- Shop owner responses
- Moderation and reporting system

### 5. Notification Module
- Email verification messages
- OTP delivery system
- Review notifications for shop owners
- Promotional announcements
- System alerts and updates

### 6. Map Integration Module
- Google Maps API integration
- Shop location markers and clusters
- Distance calculation and routing
- Directions and navigation
- Street view integration
- Mobile-optimized map controls

---

## 🔄 Request Processing Flow

```
1. Client Request (Angular) → 2. API Controller
                                      ↓
                              3. Request Validation
                                      ↓
                              4. Service Layer (Business Logic)
                                      ↓
                              5. Repository Layer (Data Access)
                                      ↓
                              6. Database (SQL Server)
                                      ↓
                              7. Response Transformation (DTO)
                                      ↓
                              8. HTTP Response → Client
```

**Cross-Cutting Concerns:**
- Global exception handling middleware
- Request/response logging
- Performance monitoring
- Security validation at each layer

---

## 🛠️ Technology Stack

### Backend
| Technology | Purpose |
|------------|---------|
| **ASP.NET Core 8.0** | Web framework and API development |
| **C# 12** | Primary programming language |
| **Entity Framework Core** | ORM for complex queries and relationships |
| **Dapper** | Micro-ORM for high-performance operations |
| **LINQ** | Data querying and manipulation |
| **JWT (JSON Web Tokens)** | Authentication and authorization |
| **FluentValidation** | Request validation framework |
| **AutoMapper** | Object-to-object mapping |

### Frontend
| Technology | Purpose |
|------------|---------|
| **Angular 17** | SPA framework |
| **TypeScript** | Type-safe development |
| **RxJS** | Reactive programming |
| **Angular Material** | UI component library |
| **Leaflet/Google Maps** | Map visualization |

### Database & Storage
| Technology | Purpose |
|------------|---------|
| **SQL Server** | Primary database |
| **Redis** (optional) | Caching layer |
| **Azure Blob Storage** (future) | Image and file storage |

### Third-Party Services
| Service | Purpose |
|---------|---------|
| **Google Maps API** | Location services and navigation |
| **SendGrid/SMTP** | Email delivery |
| **Twilio** (optional) | SMS for OTP |

### Development & Testing
| Tool | Purpose |
|------|---------|
| **xUnit/NUnit** | Unit testing framework |
| **Moq** | Mocking framework |
| **Swagger/OpenAPI** | API documentation |
| **Git** | Version control |
| **GitHub** | Repository hosting |

---

## 📋 Prerequisites

- **.NET SDK 8.0** or higher
- **Node.js 18.x** or higher
- **SQL Server 2019** or higher
- **Visual Studio 2022** or **VS Code**
- **Google Maps API Key**
- **Email Service** (SMTP or SendGrid account)

---

## ⚙️ Installation & Setup

### 1. Clone the Repository
```bash
git clone https://github.com/jesuisPraful/FameFinds.git
cd FameFinds
```

### 2. Backend Configuration

#### Update Database Connection
Edit `appsettings.json` in the API project:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=FameFindsDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "SecretKey": "your-super-secret-key-min-32-chars",
    "Issuer": "FameFinds",
    "Audience": "FameFindsUsers",
    "ExpirationMinutes": 60
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SenderEmail": "your-email@gmail.com",
    "SenderPassword": "your-app-password"
  },
  "GoogleMapsSettings": {
    "ApiKey": "your-google-maps-api-key"
  }
}
```

#### Apply Database Migrations
```bash
cd FameFinds.API
dotnet ef database update
```

#### Run the Backend
```bash
dotnet run
```
API will be available at `https://localhost:5001`

### 3. Frontend Configuration

#### Install Dependencies
```bash
cd FameFinds.Client
npm install
```

#### Update Environment Configuration
Edit `src/environments/environment.ts`:
```typescript
export const environment = {
  production: false,
  apiUrl: 'https://localhost:5001/api',
  googleMapsApiKey: 'your-google-maps-api-key'
};
```

#### Run the Frontend
```bash
ng serve
```
Application will be available at `http://localhost:4200`

### 4. Seed Initial Data (Optional)
```bash
cd FameFinds.API
dotnet run --seed
```

---

## 🧪 Testing

### Run Unit Tests
```bash
cd FameFinds.Tests
dotnet test
```

### Run with Coverage
```bash
dotnet test /p:CollectCoverage=true /p:CoverageReportsDirectory=./coverage
```

---

## 📁 Project Structure

```
FameFinds/
├── FameFinds.API/              # ASP.NET Core Web API
│   ├── Controllers/            # API endpoints
│   ├── Middleware/             # Custom middleware
│   ├── Program.cs              # Application entry point
│   └── appsettings.json        # Configuration
├── FameFinds.Core/             # Business logic layer
│   ├── Services/               # Business services
│   ├── Interfaces/             # Service contracts
│   ├── DTOs/                   # Data transfer objects
│   └── Validators/             # Input validation
├── FameFinds.Data/             # Data access layer
│   ├── Repositories/           # Data repositories
│   ├── Context/                # EF Core context
│   ├── Entities/               # Database models
│   └── Migrations/             # Database migrations
├── FameFinds.Client/           # Angular frontend
│   ├── src/
│   │   ├── app/
│   │   │   ├── components/     # Reusable components
│   │   │   ├── services/       # API services
│   │   │   ├── guards/         # Route guards
│   │   │   ├── interceptors/   # HTTP interceptors
│   │   │   └── models/         # TypeScript interfaces
│   │   └── assets/             # Static files
└── FameFinds.Tests/            # Unit tests
    ├── Services/               # Service tests
    └── Controllers/            # Controller tests
```

---

## 🔒 Security Features

- **JWT-based Authentication:** Secure, stateless session management
- **Role-based Authorization:** Granular access control
- **OTP Verification:** Two-factor authentication for registration
- **Input Validation:** Comprehensive request sanitization
- **SQL Injection Prevention:** Parameterized queries
- **XSS Protection:** Output encoding
- **CORS Configuration:** Controlled cross-origin access
- **HTTPS Enforcement:** Encrypted communication

---

## 🚦 API Endpoints Overview

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/verify-otp` - Verify OTP code
- `POST /api/auth/login` - User login
- `POST /api/auth/refresh` - Refresh JWT token

### Shops
- `GET /api/shops` - List all shops (with filtering)
- `GET /api/shops/{id}` - Get shop details
- `POST /api/shops` - Create new shop (Shop Owner)
- `PUT /api/shops/{id}` - Update shop (Shop Owner)
- `DELETE /api/shops/{id}` - Delete shop (Shop Owner)

### Search
- `GET /api/search/location` - Search shops by location
- `GET /api/search/nearby` - Find nearby shops

### Reviews
- `GET /api/reviews/shop/{shopId}` - Get shop reviews
- `POST /api/reviews` - Submit review (Customer)
- `PUT /api/reviews/{id}` - Update review (Customer)

---

## 🗺️ Roadmap

### Phase 1 (Current)
- ✅ Core platform functionality
- ✅ Map-based shop discovery
- ✅ Rating and review system

### Phase 2 (Upcoming)
- 🔄 Mobile application (iOS/Android)
- 🔄 In-app messaging between customers and shop owners
- 🔄 Advanced analytics dashboard
- 🔄 Multi-language support

### Phase 3 (Future)
- 📋 Online ordering integration
- 📋 Payment gateway integration
- 📋 AI-powered product recommendations
- 📋 Social media integration

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 👨‍💻 Author

**Praful**  
GitHub: [@jesuisPraful](https://github.com/jesuisPraful)

---

## 🙏 Acknowledgments

- Google Maps Platform for location services
- The ASP.NET Core and Angular communities
- All contributors and testers

---

## 📞 Support

For issues, questions, or suggestions:
- Open an issue on [GitHub](https://github.com/jesuisPraful/FameFinds/issues)
- Contact: [Your Email]

---

**⭐ If you find this project useful, please consider giving it a star on GitHub!**
