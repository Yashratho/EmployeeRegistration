# Employee Registration Portal

This is a full-stack Employee Registration application built as an interview assessment. It demonstrates modern web development practices, focusing on clean code, scalability, and maintainability.

## 🚀 Tech Stack

### Backend (.NET 8)
- **Framework:** ASP.NET Core Web API
- **Architecture:** Clean Architecture (Domain, Application, Infrastructure, API layers)
- **Database:** SQL Server (LocalDB)
- **ORM:** Entity Framework Core (Code-First approach)
- **Design Patterns:** Repository Pattern, Dependency Injection (DI)

### Frontend (Angular 18)
- **Framework:** Angular (Standalone Components)
- **Form Handling:** Reactive Forms with Custom Validators
- **Styling:** Bootstrap 5 & Custom CSS
- **Features:** Client-side pagination, dynamic filtering, cascading dropdowns

---

## 🏗️ Architecture Overview

The backend strictly follows **Clean Architecture** to ensure separation of concerns:
1. **Domain:** Contains core entities (`Employee_Mst`, `Country_Mst`, `State_Mst`) and database validation rules (Data Annotations).
2. **Application:** Contains interfaces (`IEmployeeRepository`) and a standardized Global API Response wrapper (`ApiResponse<T>`).
3. **Infrastructure:** Contains the EF Core `AppDbContext` and concrete Repository implementations. Handles data access and seed data.
4. **API:** Contains the RESTful Controllers. It acts merely as an entry point, keeping controllers thin and delegating work to the repositories.

---

## ⚙️ Setup & Installation

### 1. Backend Setup
1. Open the `Backend/EmployeeRegistration.sln` in Visual Studio or VS Code.
2. The connection string is pre-configured to use SQL Server LocalDB in `appsettings.json`.
3. Run the API project (`dotnet run` in the `EmployeeRegistration.API` folder).
4. **Note:** On the first run, Entity Framework will automatically create the `EmployeeRegistrationDb` database and insert the mandatory Seed Data (Countries and States).
5. Swagger UI will automatically open at `http://localhost:5171/` allowing you to test endpoints immediately.

### 2. Frontend Setup
1. Open a terminal in the `Frontend` directory.
2. Run `npm install` to restore packages.
3. Run `npm start` (or `ng serve`) to launch the Angular development server.
4. Navigate to `http://localhost:4200` in your browser.

---

## ✅ Assessment Requirements Fulfilled

- **Database:** SQL Server with exact table schemas requested.
- **Validations:**
  - `Employee Name`: Alphabets only (Regex).
  - `Age`: 1 to 3 digits only, auto-calculated from Date of Birth.
  - `Mobile Number`: Exactly 10 digits. Duplicate numbers are blocked by the database with a UI Toaster notification.
  - `Date of Birth`: Future dates blocked via custom validator.
  - `Address`: Restricts `$, %, !, +` via custom validator.
- **Cascading Dropdowns:** Selecting a Country dynamically filters the State dropdown.
- **Grid:** Displays a paginated table (5 records per page) with search/filter functionality by Name and Mobile.
- **API Formatting:** All API endpoints return a globally standardized JSON response format.
