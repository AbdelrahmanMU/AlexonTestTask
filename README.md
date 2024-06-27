Alexon Test Task API
This is a simple ASP.NET Core Web API application that supports basic CRUD operations on a Product entity, with each product belonging to one or more product categories. The API interacts with a SQL Server database.

Requirements
.NET 8.0 SDK or later
SQL Server
Docker (optional for containerization)
Setup Instructions
1. Clone the Repository
bash
git clone <repository_url>

2. Configure the Database Connection
Edit appsettings.json to add your SQL Server connection string:

json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=AlexonTestTask;Integrated Security=True"
  }
}

3. Run Database Migrations
Once you run the application the database migration will apply.

4. Run the Application
You can run the application using the .NET CLI:

bash
dotnet run
Or, if you prefer using Visual Studio, open the solution file AlexonTestTask.API.sln and press F5 to run the project.

The API will be available at http://localhost:7238.

5. Access the API Documentation
Swagger is used for API documentation. Once the application is running, you can access the API documentation at:

bash
http://localhost:7238/swagger

6. Docker Deployment (Optional)
To run the application in a Docker container, you need to have Docker installed.

Build the Docker Image:

bash
docker build -t AlexonTestTaskApi .
Run the Docker Container:

Feel free to submit issues and enhancement requests.