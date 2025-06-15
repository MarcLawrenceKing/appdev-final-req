# .NET CLI Cheatsheet

## 🛋 Project & Solution Management

| Task                        | Command                                                  |
| --------------------------- | -------------------------------------------------------- |
| Create a new solution       | `dotnet new sln -n MySolution`                           |
| Add a project to a solution | `dotnet sln MySolution.sln add path/to/MyProject.csproj` |
| List projects in a solution | `dotnet sln list`                                        |

---

## 🧱 Project Creation (Templates)

| Project Type           | Command                                |
| ---------------------- | -------------------------------------- |
| Console app            | `dotnet new console -n MyConsoleApp`   |
| Class library          | `dotnet new classlib -n MyLibrary`     |
| ASP.NET Web API        | `dotnet new webapi -n MyApi`           |
| ASP.NET MVC web app    | `dotnet new mvc -n MyMvcApp`           |
| ASP.NET Razor Pages    | `dotnet new razor -n MyRazorApp`       |
| Blazor WebAssembly app | `dotnet new blazorwasm -n MyBlazorApp` |
| xUnit test project     | `dotnet new xunit -n MyTests`          |
| NUnit test project     | `dotnet new nunit -n MyTests`          |
| MSTest project         | `dotnet new mstest -n MyTests`         |
| Empty project          | `dotnet new classlib --no-restore`     |

> 📋 To see all templates available:  
> `dotnet new list`

---

## 🚀 Build & Run

| Task                     | Command                                         |
| ------------------------ | ----------------------------------------------- |
| Build current project    | `dotnet build`                                  |
| Build a specific project | `dotnet build path/to/MyProject.csproj`         |
| Run current project      | `dotnet run`                                    |
| Run a specific project   | `dotnet run --project path/to/MyProject.csproj` |
| Clean project            | `dotnet clean`                                  |

---

## 🥪 Testing

| Task                            | Command                              |
| ------------------------------- | ------------------------------------ |
| Run tests                       | `dotnet test`                        |
| Run tests from specific project | `dotnet test path/to/MyTests.csproj` |

---

## 🔌 NuGet & Dependencies

| Task                    | Command                             |
| ----------------------- | ----------------------------------- |
| Restore dependencies    | `dotnet restore`                    |
| Add NuGet package       | `dotnet add package PackageName`    |
| Remove NuGet package    | `dotnet remove package PackageName` |
| List installed packages | `dotnet list package`               |

---

## 📄 Project File Management

| Task                             | Command                                               |
| -------------------------------- | ----------------------------------------------------- |
| Add reference to another project | `dotnet add reference path/to/OtherProject.csproj`    |
| List project references          | `dotnet list reference`                               |
| Remove a reference               | `dotnet remove reference path/to/OtherProject.csproj` |

---

## 🌐 Publishing

| Task                        | Command                       |
| --------------------------- | ----------------------------- |
| Publish a project           | `dotnet publish`              |
| Publish with release config | `dotnet publish -c Release`   |
| Publish to specific folder  | `dotnet publish -o ./publish` |

---

## 📊 Info & Help

| Task                    | Command                                           |
| ----------------------- | ------------------------------------------------- |
| Show .NET SDK version   | `dotnet --version`                                |
| List installed SDKs     | `dotnet --list-sdks`                              |
| List installed runtimes | `dotnet --list-runtimes`                          |
| See available templates | `dotnet new list`                                 |
| Get help on any command | `dotnet help <command>` (e.g., `dotnet help new`) |

## 🛠️ Entity Framework Core (EF Core) Migration Commands

### Install EF Core CLI (if not installed)

```bash
dotnet tool install --global dotnet-ef
```

### Check EF Core version

```bash
dotnet ef --version
```

### Add a migration

```bash
dotnet ef migrations add <MigrationName>
# Example:
dotnet ef migrations add InitialCreate
```

### Apply migration (create/update database schema)

```bash
dotnet ef database update
```

### Remove the last migration

```bash
dotnet ef migrations remove
```

### List applied migrations

```bash
dotnet ef migrations list
```

### Drop the database

```bash
dotnet ef database drop
```

### Notes

- Make sure your `DbContext` is correctly set up.
- Add your connection string to `appsettings.json` or `Program.cs`.
- Run from the project folder containing your `.csproj` file.
