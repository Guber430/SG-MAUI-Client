# Summer Games Web API

This project is a **Web API** created to manage a **Summer Games** event, similar to the Canada Summer Games held in the Niagara Region. The Web API is built using **C#**, **.NET 8**, and **SQLite** for the database, and is designed to handle data about athletes, sports, and contingents (provinces/territories).

## Project Overview

The Web API is intended to manage the following entities:
- **Sport**: Represents different sports being competed in.
- **Contingent**: Represents a province or territory, each containing multiple athletes.
- **Athlete**: Represents an individual athlete who can compete in one or more sports and belong to one contingent.

### Key Features:
- **CRUD operations** for managing athletes, sports, and contingents.
- **Validation** for age, BMI, and gender for athletes to ensure fair competition.
- **Unique athlete code** generation with the prefix `A:`.
- **Filtering** of athletes by **sport** or **contingent**.
- **Error handling** for validation rules, database errors, and concurrency issues.

### Technologies Used:
- **Backend**: C#, .NET 8
- **Database**: SQLite
- **API Documentation**: Swagger for API testing and documentation
- **Authentication**: Not included in this version, but can be added as needed

## Entities

- **Contingent**
  - Represents a **province** or **territory**.
  - Each contingent has a unique 2-letter code.
  - A contingent can have multiple athletes.
  
- **Athlete**
  - Represents an **athlete** participating in the event.
  - Each athlete has a unique 7-digit code (e.g., `A:1234567`).
  - Athletes are assigned to a specific **contingent** and can compete in a **sport**.
  - There are restrictions on age, BMI, and gender for fair competition.

- **Sport**
  - Represents a **sport** (e.g., swimming, running).
  - Each sport has a unique 3-letter code.
  - Multiple athletes can compete in the same sport.

### API Endpoints
- **GET** `/api/sports`: Retrieve a list of all sports.
- **GET** `/api/contingents`: Retrieve a list of all contingents.
- **GET** `/api/athletes`: Retrieve a list of all athletes. Can be filtered by sport or contingent.
- **POST** `/api/athletes`: Add a new athlete.
- **PUT** `/api/athletes/{id}`: Update an existing athlete.
- **DELETE** `/api/athletes/{id}`: Delete an athlete.

## Setup Instructions

### Prerequisites:
- .NET 8 SDK
- SQLite
- Visual Studio or your preferred IDE

### Clone the Repository
```bash
git clone https://github.com/yourusername/SummerGamesAPI.git
cd SummerGamesAPI
