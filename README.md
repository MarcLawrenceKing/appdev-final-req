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
| [SQL Server (Developer)](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or [Azure SQL] | ✅ | ✅ | ✅ |
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

https://github.com/user-attachments/assets/f7510d71-14f7-44b2-b05e-a828a28bf73a


📷 *Login Page*

https://github.com/user-attachments/assets/54da3dea-6e7c-4e23-b5df-8e2989df2a6f

---

### 2. 📊 Dashboard Overview

The dashboard provides:

- Overview of attendance logs  
- Quick access to Members, Events, and Reports

📷 *Dashboard*

https://github.com/user-attachments/assets/f78dc9ef-0ebf-4379-86c2-99e3b7abd8c3

---

### 3. 👥 Manage Members

The **Members** page gives you full control over your organization's member list.

#### ✅ Member CRUD Operations:

- **➕ Create Member** – Click the **New** button to add a new member. You'll be asked to provide their full name, email, phone number, and birthdate.

📷 *Create Member Form*
  
https://github.com/user-attachments/assets/b746202c-f989-4fda-91eb-524738f387d3

- **✏️ Edit Member** – Click the **Edit** button beside any member to update their profile details.

📷 *Edit Member Form*  

https://github.com/user-attachments/assets/830fb4d1-6093-4f75-94aa-0bb81611b8b1


- **📄 View** – The members are displayed in a responsive table with useful features like sorting and searching.

- **🗑️ Delete** – Select one or more members using checkboxes, then click the **Delete** button. A modal will confirm before deletion proceeds.

📷 *Delete Members Confirmation*  

https://github.com/user-attachments/assets/5863dae9-4dfd-4cae-b97f-25d6796a1681


---

#### 🔍 Search Functionality:

You can search across **any column** — such as ID, full name, email, phone number — using the search bar at the top. Results are filtered instantly as you type.

📷 *Search Filter on Members Table*  

https://github.com/user-attachments/assets/39822d07-2831-44be-bd66-b77d7f8ad889

---

#### 🔃 Column Sorting:

Click on any column header (ID, Full Name, Email, etc.) to sort the list in ascending or descending order. This feature supports text, numbers, and dates.

📷 *Sorted Members Table*  

https://github.com/user-attachments/assets/287857a6-a8c3-48bc-b031-ace40c451033

---

#### 📥 Batch Upload via CSV:

Click the **Import** button to upload a `.csv` file containing multiple members. Once uploaded, the app will automatically add all valid entries from the file.

📷 *CSV Upload Modal for Members*  

https://github.com/user-attachments/assets/e5826c67-5726-408e-b0ee-077ae954a1ec

---

### 4. 📅 Manage Events

The **Events** page provides tools to organize, edit, and track all events related to your organization.

#### ✅ Event CRUD Operations:

- **➕ Create Event** – Click the **New** button to open the event creation form. You can set the event title, date, and description.
  
📷 *Create Event Form*  


https://github.com/user-attachments/assets/bcc40689-a7a3-4316-8be3-da6b3bb4b1ff


- **✏️ Edit Event** – To modify an existing event, click the **Edit** button beside the event. You can update the event’s details such as the title, date, and description.

📷 *Edit Event Form*  

https://github.com/user-attachments/assets/b4c9ac51-b491-4d35-95a5-efac4a6cef1d


- **📄 View** – All events are displayed in a table with responsive design.

- **🗑️ Delete** – Select one or more events using checkboxes, then click **Delete**. A confirmation modal will appear before deletion.

📷 *Delete Events Confirmation*  

https://github.com/user-attachments/assets/a994f77c-f32c-4962-b7dc-f9c6e165a2e0


---

#### 🔍 Search Functionality:

The search bar filters events by keywords found in **any column** — ID, title, description, or date. It instantly narrows down the results to help you find specific entries.

📷 *Search Filter on Events Table*  

https://github.com/user-attachments/assets/deb36541-2d2f-4062-b0cb-e2935091016e


---

#### 🔃 Column Sorting:

Each column (ID, title, description, date) is sortable. Click once to sort ascending, click again to sort descending. Sorting supports text, numbers, and dates.

📷 *Sorted Events Table*  

https://github.com/user-attachments/assets/34929996-45be-46d3-ac9a-a87e61f39661


---

#### 📥 Batch Upload via CSV:

Click the **Import** button to open a modal that accepts `.csv` files. The system will read and create multiple events in bulk based on the provided file.

📷 *CSV Upload Modal for Events*  

https://github.com/user-attachments/assets/123213a1-127a-45ad-bc54-1e7ab5198ba7


---

### 5. 📝 Take Attendance

- Click on **Attendance**  
- Mark members as **Present**, or **Absent**  
- Submit attendance 

📷 *Attendance Table*

https://github.com/user-attachments/assets/423e08d9-c8dc-4bfc-91b8-8f376cdb4b7f


---

## 🌗 Theme Toggle

- Dark/light mode toggle is located in the **top-right navbar**  
- User preference is saved using `localStorage`

📷 *Theme Toggle*

https://github.com/user-attachments/assets/3316839a-82bd-4dd5-b67c-d136bac2eb69

---

## 📁 Project Structure

```plaintext
appdev-final-req/
│
├── Controllers/                # MVC Controllers (Attendance, Events, Members, Users, Home)
│
├── Data/
│   ├── ApplicationDbContext.cs
│   ├── DateOnlyConverter.cs
│   └── CSV Files/             # Sample CSV files for batch uploads
│
├── Migrations/                # EF Core migration files
│
├── Models/
│   ├── ViewModels/            # AddEventViewModel, AddMemberViewModel, etc.
│   └── Entities/              # Attendance.cs, Event.cs, Member.cs, User.cs
│
├── Views/
│   ├── Attendance/            # List, MarkAttendance
│   ├── Events/                # Add, Edit, List
│   ├── Members/               # Add, Edit, List
│   ├── Users/                 # Login, Register
│   ├── Shared/                # _Layout, _ValidationScriptsPartial, etc.
│   └── Home/                  # Dashboard, Index, Privacy
│
├── wwwroot/
│   ├── css/                   # Styles
│   ├── js/                    # Scripts
│   └── images/                # UI Images and Assets
│
├── appsettings.json           # Main app configuration
├── appsettings.Development.json
├── Program.cs
├── Properties/
│   └── launchSettings.json
└── appdev-final-req.csproj
```


---

## 🛠 Common Tasks

| Task               | Command                                               |
|--------------------|--------------------------------------------------------|
| Update DB Schema   | `dotnet ef migrations add MigrationName`              |
| Apply Migration    | `dotnet ef database update`                           |
| Run Project        | `dotnet run`                                          |
| Publish Project    | `dotnet publish -c Release -o ./publish`             |

---

## 🙋 FAQ

**Q: What database does this use?**  
A: By default, the app uses **SQL Server**. You may also reconfigure the connection string to use **Azure SQL**, **PostgreSQL**, or **SQLite** based on your hosting environment.

**Q: Is this mobile responsive?**  
A: Yes. The entire UI is built using **Bootstrap 5**, which ensures responsiveness across phones, tablets, and desktops.

**Q: Can I upload members/events in bulk?**  
A: Yes. The app supports **CSV file uploads** to batch import members and events easily.

**Q: Does this support dark mode?**  
A: Yes. There's a built-in **theme toggle** in the navbar to switch between dark and light mode. The preference is saved locally in your browser.

**Q: Can I deploy this to production?**  
A: Absolutely. You can publish the app using `dotnet publish` and deploy to platforms like **IIS**, **Azure App Service**, **Docker**, or **Linux servers**.

---

## 🙌 Credits

- Developed by **Alpornon, Edusma, King and Sebastian**  
- Built with **ASP.NET Core MVC**, **Bootstrap**, and **Entity Framework Core**  
- Icons by [Bootstrap Icons](https://icons.getbootstrap.com/)  
- Fonts by [Google Fonts](https://fonts.google.com/)

---

## 🧾 License

This project is licensed under the **MIT License**.  
You are free to use, modify, and distribute this software with proper attribution.

See the full terms in the `LICENSE` file included in this repository.
