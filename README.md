# Capstone CLI Pharmacy Shop

## Description

Console-based pharmacy store application built with **C# (.NET)** and **MySQL**.

This project demonstrates a clean layered architecture with clear separation of concerns between:

The goal of this project is to apply backend architecture principles in a structured and maintainable way.

---

## Architecture

```
UI  →  Service  →  Repository  →  Database
```
---
## Features Implemented
---

### Customer
- View entire product catalogue

### Admin
- Add product to catalogue
- Update existing product
- Delete product from catalogue

---

## Database Setup
---

### 1. Create Database
---

Open MySQL and run:

```sql
CREATE DATABASE capstone_pharmacy;
```

---

### 2. Run Schema Script

Create the required tables:

```bash
mysql -u YOUR_USERNAME -p capstone_pharmacy < database/schema.sql
```

---

### 3. Seed the Database

Insert initial data:

```bash
mysql -u YOUR_USERNAME -p capstone_pharmacy < database/seed.sql
```

---

### 4. Configure Connection String

Open `Database.cs` and update the connection string to match your local MySQL configuration:

```
server=localhost;
user=YOUR_USERNAME;
password=YOUR_PASSWORD;
database=capstone_pharmacy;
```
---

## Running the Application

After configuring the database:

```bash
dotnet run
```

---

## Project Structure

## Design Decisions

## Future Improvements
