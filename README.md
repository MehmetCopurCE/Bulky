# Bulky - MVC App

This project is a book management system called **Bulky**, an MVC application developed using **ASP.NET Core 8**. The application provides a simple interface to manage books. There are two types of users: **Admin** and **Customer**. Admin users can add, edit and delete books, while Customer users can view books.

https://github.com/user-attachments/assets/79b75f9b-94bd-4a18-bc96-0e44a2a0f3d2


## Technologies and Architectures Used

- **ASP.NET Core Identity**: Used for authentication and user management.
- **MySQL**: MySQL is used as the database.
- **Entity Framework Core**: Used for database operations.
- **Scaffold**: Automatically generated codes for CRUD operations.
- **N-Tier Architecture**: The application architecture is in a layered structure (N-Tier). The layers are divided as follows:
    - **Bulky.DataAccess**: Database operations, `AppDbContext` and migration files are located here.
    - **Bulky.Models**: Data models used throughout the application are located in this layer.
    - **Bulky.Utilities**: Helper classes and general purpose tools. For example, the `SD` class created for roles is located here.
    - **BulkyWeb**: The main layer of the web application, the user interface, controllers and views are located here.


## Project Structure


### Solution Structure
<img src="BulkyWeb/wwwroot/images/AppScreenshots/solution_architecture.png" alt="Solution Structure" width="300"/>



## Features

- **Admin Panel**: Admin users can add, edit and delete books.
- **Book Listing**: All users can view books.
- **Authentication**: User login and registration processes using **ASP.NET Core Identity**.
- **Database**: Database operations are performed with **Entity Framework Core** and **MySQL**.
- **Areas**: Areas reserved for Admin and Customer are used.
- **Scaffold Operations**: Scaffold operations are used for CRUD operations.


## Installation

1. **Clone the project**:
    ```bash
    git clone https://github.com/MehmetCopurCE/Bulky.git
    ```

2. **Go to the project directory**:
    ```bash
    cd bulky
    ```

3. **Install the required dependencies**:
    ```bash
    dotnet restore
    ```

4. **Configure the database**:
- Update the database connection string (`ConnectionStrings`) in the `appsettings.json` file with your own MySQL database information.
- Perform the migration operations:
    ```bash
    dotnet ef database update
    ```

5. **Run the application**:
    ```bash
    dotnet run
    ```

## User Roles

- **Admin**:
    - Can add, edit and delete books.
    - Can manage other users.
- **Customer**:
    - Can view books.
    - Does not have admin privileges.

## Used NuGet Packages

<img src="BulkyWeb/wwwroot/images/AppScreenshots/nuget_packages.png" alt="Solution Structure" width="600"/>

## Database

This project uses the **MySQL** database. **Entity Framework Core** is preferred for database operations. You can configure the database by following the steps below:

1. **Update Database Connection String**:
   - Add the MySQL database connection string to the `ConnectionStrings` section in the `appsettings.json` file.

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "server=localhost;database=bulkydb;user=root;password=yourpassword"
     }
   }
2. **Create Migration and Update Database**:
    - Run the following commands for migration operations:
   ```csharp
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

<img src="BulkyWeb/wwwroot/images/AppScreenshots/Ekran Resmi 2024-08-15 15.32.46.png" alt="Solution Structure" width="300"/>


## Resources

I followed the following training course while developing this project:

- **Course Name**: [Introduction to ASP.NET Core MVC (.NET 8)](https://www.youtube.com/watch?v=AopeJjkcRvU)
    - This course teaches you how to develop basic and advanced web applications using ASP.NET Core MVC. It covers many topics from project structure to authentication processes.


## Contact & Feedback

Feel free to share your ideas or suggestions about the project. You can reach me through the following channels:

<a href="https://www.linkedin.com/in/mehmet-copur/"><img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS0bGEl9v47XieEtHyj0TqTr1tOXJmib-KHtw&s" height = "50"/></a> <a href="mailto:mhmtcpr120@gmail.com?"><img src="https://img.shields.io/badge/gmail-%23DD0031.svg?&style=for-the-badge&logo=gmail&logoColor=white" height = "50"/></a> <a href="https://medium.com/@mhmtcpr120/nette-dependency-injection-transient-scoped-ve-singleton-ya%C5%9Fam-d%C3%B6ng%C3%BCleri-aa9aa4f38193"><img src="https://miro.medium.com/v2/resize:fit:1400/1*RB1rxSK_TBmcC5D2PN30JA.png" height = "50"/></a> 


Any feedback you give me is valuable and will help me make the project better.
