# Summer Games Web API

Welcome to the **Summer Games Web API**! This API is designed to manage a **Summer Games** event, similar to the Canada Summer Games held in the Niagara Region. The API is built using **C#**, **.NET 8**, and **SQLite** for the database. It handles data about athletes, sports, and contingents (provinces/territories) involved in the event.

---

## Table of Contents
- [Project Overview](#project-overview)
- [Key Features](#key-features)
- [Technologies Used](#technologies-used)
- [Entities](#entities)
  - [Contingent](#contingent)
  - [Athlete](#athlete)
  - [Sport](#sport)
- [API Endpoints](#api-endpoints)
- [Setup Instructions](#setup-instructions)
  - [Prerequisites](#prerequisites)
  - [Clone the Repository](#clone-the-repository)

---

## Project Overview

The Web API allows you to manage the following entities:
- **Sport**: Represents different sports being competed in.
- **Contingent**: Represents a province or territory, with each contingent containing multiple athletes.
- **Athlete**: Represents individual athletes who can compete in one or more sports and belong to one contingent.

### Key Features

- **⚙️ CRUD Operations**: Create, Read, Update, and Delete operations for managing athletes, sports, and contingents.
- **⚖️ Athlete Validation**: Ensures athletes meet age, BMI, and gender requirements for fair competition.
- **🔢 Unique Athlete Codes**: Generates unique athlete codes with the `A:` prefix (e.g., `A:1234567`).
- **🔍 Filtering**: Filter athletes by **sport** or **contingent**.
- **🚨 Error Handling**: Includes validation checks, database error handling, and concurrency control.

### Technologies Used

- **Backend**: C#, .NET 8
- **Database**: SQLite
- **API Documentation**: Swagger for API testing and documentation
- **Authentication**: Not included in this version, but can be added as needed.

---

## Entities

### Contingent
- Represents a **province** or **territory**.
- Each contingent has a **unique 2-letter code**.
- A contingent can have multiple **athletes**.

### Athlete
- Represents an **athlete** participating in the event.
- Each athlete has a **unique 7-digit code** (e.g., `A:1234567`).
- Athletes are assigned to a **contingent** and compete in a **sport**.
- There are restrictions on **age**, **BMI**, and **gender** to ensure fair competition.

### Sport
- Represents a **sport** (e.g., swimming, running).
- Each sport has a **unique 3-letter code**.
- Multiple athletes can compete in the same sport.

## API Endpoints

| **Method**  | **Endpoint**                        | **Description**                        |
|-------------|-------------------------------------|----------------------------------------|
| **GET**     | `/api/sports`                       | Retrieve a list of all sports.         |
| **GET**     | `/api/contingents`                  | Retrieve a list of all contingents.    |
| **GET**     | `/api/athletes`                     | Retrieve a list of all athletes. You can filter by sport or contingent. |
| **POST**    | `/api/athletes`                     | Add a new athlete.                     |
| **PUT**     | `/api/athletes/{id}`                | Update an existing athlete.            |
| **DELETE**  | `/api/athletes/{id}`                | Delete an athlete.                     |


---

## Setup Instructions

### Prerequisites

To set up the project locally, make sure you have the following installed:

- **.NET 8 SDK**
- **SQLite**
- **Visual Studio** (or your preferred IDE)

### Clone the Repository

```bash
git clone https://github.com/Guber430/SG-MAUI-Client.git
cd SG-MAUI-Client
