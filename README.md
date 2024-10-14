# Blogging Platform

## Overview

This project is a full-stack web application developed as part of a .NET Developer job application. It includes a user interface (UI), backend API, and an SQL database. The application runs locally, demonstrating the following key features:

- Creating, reading, updating, and deleting blog posts (CRUD functionality).
- User authentication for login/logout (optional enhancement).
- Categorization of posts (optional enhancement).

### Technologies Used:
- **Frontend:** Blazor with Bootstrap for styling
- **Backend:** ASP.NET Core RESTful Web API
- **Database:** SQL Server
- **Authentication:** JWT-based token authentication
- **Architecture:** Clean Architecture

## Features

### Frontend (UI)
The frontend of this application is built using Blazor, focusing on componentization and a user-friendly design. The following features are available:

- **Homepage:** Displays a list of blog posts with title, content, creation date, and author.
- **Post Form:** A form to create new blog posts, including fields for title, content, and categories.
- **Post Editing:** Ability to update existing blog posts only for the user who created the blog post.
- **Post Deletion:** Ability to delete blog posts only for the user who created the blog post.
- **Responsive Design:** The UI is responsive, providing a seamless experience on different devices.
- **Error message Handling:** Toaster feedback for the user on most views.
- **Security:** Routh redirection and login/Register. 

### Backend (API)

The backend of this application is an ASP.NET Core RESTful Web API that supports CRUD operations for blog posts, categories, and users. It follows Clean Architecture principles, ensuring separation of concerns and maintainability. Key features include:

- **CRUD Operations:**
  - **BlogPost:** Create, read, update, and delete blog posts.
  - **Category:** Manage blog categories, including creating, assigning, and updating categories for posts.
  - **User:** User management, including user registration and handling related data such as blog post ownership.
  
- **Clean Architecture:** 
  The application follows Clean Architecture principles, organizing the code into layers: Domain, Application, Infrastructure, and WebApi. This approach promotes flexibility, testability, and separation of concerns between business logic and infrastructure.

- **JWT Bearer Authentication:**
  Authentication is handled using JWT (JSON Web Tokens) for securing API endpoints. The API validates tokens for user authentication and protects routes to ensure only authenticated users can access or modify resources.

- **Argon2Id Password Hashing:**
  User passwords are securely hashed and using salting and peppering with Argon2Id.
  
- **Global Exception Handling:**
  A global exception handler is implemented to ensure consistent error handling across the application. 
### Database
The application uses SQL Server as the database to store blog post data. The schema includes tables for:
- **BlogPosts:** Stores the title, content, created date, and author ID.
- **Users:** Stores user information for authentication.
- **Categories:** Stores post categories.
- **BlogPostCategory** Stores BlogpostID and CategoryID with many-to-many relationships between posts and categories.


## Setup Instructions

### Prerequisites
- .NET 8 SDK
- SQL Server (or use SQLite for an in-memory version)
- A code editor like Visual Studio or Visual Studio Code

### Running the Application Locally

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/Laszlo199/FlexyBoxBloggingApp.git
  
2. **Select the Backend:**
   ```bash
   cd FlexyBoxBloggingApp\Backend\src\WebApi
   dotnet run
   Listening: http://localhost:9090
   With Swagger: http://localhost:9090/swagger/index.html
   
3. **Select the Frontend:**
    ```bash
    cd FlexyBoxBloggingApp\Frontend\BloggingAppFrontend>
    dotnet run
    Listening: http://localhost:5138
    
4. **Docker-compose:**
   ```bash
   If you need then I can set it up. Just write me an email. 😊
   
4. **Seeded Database:**
   I already added some data to the database and have one test user.
   ```bash
   Email: user1@example.com
   Password: password123
   

## Challenges Faced

- **Learning Blazor:** This was my first time using Blazor, so a significant amount of time was spent on learning and implementing the frontend. This included understanding componentization, handling events, and managing the state and Security.
- **Security and Debugging on the Frontend:** Setting up security and debugging took additional time, especially with authentication.
- **State Management in Blazor:** Encountered multiple calls to `OnInitializedAsync`, which caused redundant rerenders. This issue was resolved by optimizing the state management process.
- **Authentication Issue:** A lot of time was spent troubleshooting an authentication issue, which was ultimately resolved by setting `InteractiveServerRenderMode` to `false`.

## Time Tracking

- **Initial project setup (Backend and Frontend):** around 1 hour
- **Frontend design and implementation:** 9-12 hours
- **Backend API development:** 4 hours
- **Database design and migration:** around 2 hours
- **Frontend authentication setup:** 3-5 hours
- **Backend authentication setup:** 1 hour
- **Testing and debugging:** 1-2 hour

## Enhancements Implemented

- **User Authentication:** JWT-based login/logout functionality.
- **Post Categorization:** Added a feature to categorize posts.
- **Search:** A really basic search functionality for blog posts(Definitely need a better one which can work with a lot of data to) 
