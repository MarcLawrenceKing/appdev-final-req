# AttendanceMo'To 📝

**Your go-to for precise and easy attendance tracking.**  
AttendanceMo'To is a modern, responsive attendance management web application designed for schools, offices, and organizations. It ensures attendance is recorded **accurately**, **efficiently**, and in **real-time**.

---

## 📥 Installation Guide (Windows / macOS / Linux)

This app runs on **.NET 8** and can be developed using **Visual Studio 2022** (Windows only) or **Visual Studio Code** (cross-platform). Choose your preferred setup below.

---

### ⚙️ Prerequisites

| Requirement | Windows | macOS | Linux |
|------------|---------|-------|-------|
| [.NET SDK 8+](https://dotnet.microsoft.com/download) | ✅ | ✅ | ✅ |
| [SQL Server (Express)](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or [Azure SQL] | ✅ | ✅ | ✅ |
| [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) | ✅ | ❌ | ❌ |
| [Visual Studio Code](https://code.visualstudio.com/) | ✅ | ✅ | ✅ |
| [EF Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) | ✅ | ✅ | ✅ |
| Git | ✅ | ✅ | ✅ |

---

### 🖥 Installation Using Visual Studio 2022 (Windows Only)

1. **Install Visual Studio 2022**
    - Choose the **ASP.NET and web development** workload.
2. **Clone the Project**
    ```bash
    git clone https://github.com/MarcLawrenceKing/appdev-final-req.git
    cd AttendanceMoTo
    ```
3. **Open `.sln` File**
    - Open `appdev-final-req.sln` in Visual Studio.
4. **Configure Database**
    - Edit `appsettings.json`:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AttendanceMoTo;Trusted_Connection=True;"
    }
    ```
5. **Run Migrations**
    - Open Package Manager Console:
      ```powershell
      Update-Database
      ```
6. **Run the App**
    - Press `F5` or click "Start Debugging".

---

### 💻 Installation Using Visual Studio Code (Windows/macOS/Linux)

1. **Install .NET SDK**
    ```bash
    dotnet --version
    # Should return 8.0 or above
    ```

2. **Clone the Repo**
    ```bash
    git clone https://github.com/MarcLawrenceKing/appdev-final-req.git
    cd AttendanceMoTo
    ```

3. **Install VS Code Extensions**
    - C#
    - Razor
    - SQL Server (optional)
    - GitLens

4. **Configure Your Database**
    - Edit `appsettings.Development.json`:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=AttendanceMoTo;User Id=youruser;Password=yourpass;"
    }
    ```

5. **Apply Migrations**
    ```bash
    dotnet ef database update
    ```

6. **Run the Project**
    ```bash
    dotnet run
    ```

7. **Navigate to App**
    - Open your browser and go to:  
      [https://localhost:5001](https://localhost:5001)

---

## 📘 User Guide

### 1. 🔐 Register & Login

- Click **Register** → Create an account  
- Click **Login** → With existing login credentials  
- After registration, login with your new credentials  

📷 *Registration Page*  

![sample image](Documentations/images/sample.jpg "Sample Image")

📷 *Login Page*

![sample image](Documentations/images/sample.jpg "Sample Image")


---

### 2. 📊 Dashboard Overview

The dashboard provides:

- Overview of attendance logs  
- Quick access to Members, Events, and Reports  

📷 * Dashboard*

![sample image](Documentations/images/sample.jpg "Sample Image")

---

### 3. 👥 Manage Members

- Navigate to **Members**  
- Add new members, edit profiles, or remove entries  

📷 *Members List*

![sample image](Documentations/images/sample.jpg "Sample Image")

---

### 4. 📅 Create Events

- Go to **Events**  
- Click "Create New"  
- Set title, date/time, description  

📷 *Create Event Page*

![sample image](Documentations/images/sample.jpg "Sample Image")

---

### 5. 📝 Take Attendance

- Click on **Attendance**  
- Mark members as **Present**, **Late**, or **Absent**  
- Submit attendance and export reports if needed  

📷 *Attendance Table*

!![sample image](Documentations/images/sample.jpg "Sample Image")

---

## 🌗 Theme Toggle

- Dark/light mode toggle is located in the **top-right navbar**  
- User preference is saved using `localStorage`

📷 *Theme Toggle*

![sample image](Documentations/images/sample.jpg "Sample Image")

---

## 🎥 Optional Video Guide

Attendance 'Mo To walkthrough tutorial here:

[![Watch the walkthrough](https://img.youtube.com/vi/QdnVT22LBBs/hqdefault.jpg)](https://www.youtube.com/watch?v=QdnVT22LBBs)

---

## 📁 Project Structure

```plaintext
appdev-final-req/
│
├── Controllers/
├── Models/
├── Views/
│   └── Shared/
│       └── _Layout.cshtml
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── images/
├── appsettings.json
├── Program.cs
├── README.md
└── AttendanceMoTo.sln
```

---

## 🛠 Common Tasks

| Task | Command |
|------|---------|
| Update DB Schema | `dotnet ef migrations add MigrationName` |
| Apply Migration | `dotnet ef database update` |
| Run Project | `dotnet run` |
| Publish Project | `dotnet publish -c Release -o ./publish` |

---

## 🙋 FAQ

**Q: What database does this use?**  
A: It uses **SQL Server** by default. You can configure it for **Azure SQL**, **PostgreSQL**, or **SQLite** if needed.

**Q: Can I deploy this to production?**  
A: Yes. Use `dotnet publish`, set up a production-grade database, and host it on **IIS**, **Azure App Service**, or **Docker**.

**Q: Is this mobile responsive?**  
A: Yes. The UI uses Bootstrap 5, making it responsive by default.

---

## 🙌 Credits

- Developed by [Your Team]  
- Built using ASP.NET Core MVC, Bootstrap, Entity Framework  
- Icons by Bootstrap Icons  
- Fonts by Google Fonts

---

## 🧾 License

MIT License.  
See `LICENSE` file for full terms.

