# `sqlcmd` Cheat Sheet (Linux/macOS)

## ✅ Basic Connection

```bash
sqlcmd -S <server> -U <username> -P <password>
```

- `-S` : Server name or IP address (e.g. `localhost`, `localhost\SQLEXPRESS`)
- `-U` : SQL Server login (e.g. `sa`)
- `-P` : Password

---

## 🔐 Encryption Options

- `-N` : Encrypt the connection (SSL/TLS)
- `-C` : Trust the server certificate (skip validation)

```bash
sqlcmd -S localhost -U sa -P 'password' -N -C
```

---

## 📄 Input/Output

- `-i <filename>` : Execute SQL commands from a `.sql` file
- `-o <filename>` : Output results to a file

```bash
sqlcmd -S . -U sa -P 'pass' -i script.sql -o results.txt
```

---

## 🛠️ Query Options

- `-Q "<query>"` : Execute a query and exit
- `-q "<query>"` : Execute a query and stay in interactive mode

```bash
sqlcmd -S . -U sa -P 'pass' -Q "SELECT @@VERSION"
```

---

## 🖊️ Formatting

- `-s <separator>` : Column separator (default is a space)
- `-W` : Remove trailing spaces
- `-h <rows>` : Number of rows per header (0 = print header once)
- `-k <level>` : Remove extra spaces (1 = retain 1, 2 = remove all)

```bash
sqlcmd -S . -U sa -P 'pass' -Q "SELECT * FROM Users" -s "," -W
```

---

## 🌍 Authentication

- `-E` : Use Windows Authentication (not applicable on Linux/macOS)
- `-G` : Use Microsoft Entra ID (Azure AD)

---

## 🧩 Scripting and Variables

- `-v var="value"` : Define scripting variables
- `:setvar var value` : Inside script files

```bash
sqlcmd -S . -U sa -P 'pass' -v tablename="Users"
```

---

## 🆘 Help and Version

- `-?` : Show help
- `-X` : Disable commands like `!!`, `:!!` for security
- `-Z` : New line after batch

---

## 🧼 Miscellaneous

- `-b` : Exit on error
- `-I` : Enable Quoted Identifier (e.g., `"MyColumn"`)
- `-l <timeout>` : Login timeout in seconds
- `-t <seconds>` : Query timeout

---

## 💡 Examples

### Run interactive shell:

```bash
sqlcmd -S localhost -U sa -P 'password'
```

### Run SQL script:

```bash
sqlcmd -S localhost -U sa -P 'password' -i init.sql
```

### Run query inline:

```bash
sqlcmd -S localhost -U sa -P 'password' -Q "SELECT name FROM sys.databases"
```
