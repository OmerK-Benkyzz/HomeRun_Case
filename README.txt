# HomeRun Case

This project is a .NET 7.0 based application that includes notification and rating services. It provides APIs for managing notifications and ratings, along with comprehensive unit tests.

## Table of Contents
- [Overview](#overview)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Running the Application](#running-the-application)
- [Testing](#testing)
- [CI/CD Pipeline](#cicd-pipeline)

## Overview

The HomeRun Case solution consists of the following main components:
- **Notification Service**: Handles all operations related to notifications.
- **Rating Service**: Manages operations related to ratings.
- **Unit Tests**: Ensures the reliability and correctness of the services.

## Prerequisites

Before running this application, ensure you have the following installed:
- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Docker](https://www.docker.com/products/docker-desktop)

## Getting Started

Follow these steps to set up and run the project:

1. Clone the repository:
    ```sh
    git clone https://github.com/OmerK-Benkyzz/testutest123.git
    cd testutest123
    ```

2. Restore the dependencies:
    ```sh
    dotnet restore
    ```

3. Build the solution:
    ```sh
    dotnet build
    ```

## API Endpoints

### Notification Service

The Notification Service provides the following endpoints:

- **GET /api/notifications**: Retrieves a list of notifications.
- **GET /api/notifications/{id}**: Retrieves a specific notification by ID.
- **POST /api/notifications**: Creates a new notification.
- **PUT /api/notifications/{id}**: Updates an existing notification.
- **DELETE /api/notifications/{id}**: Deletes a notification by ID.

### Rating Service

The Rating Service provides the following endpoints:

- **GET /api/ratings**: Retrieves a list of ratings.
- **GET /api/ratings/{id}**: Retrieves a specific rating by ID.
- **POST /api/ratings**: Creates a new rating.
- **PUT /api/ratings/{id}**: Updates an existing rating.
- **DELETE /api/ratings/{id}**: Deletes a rating by ID.

## Running the Application

To run the application using Docker, follow these steps:

1. Build the Docker images:
    ```sh
    docker-compose -f notification-docker-compose-build.yml build
    docker-compose -f rating-docker-compose-build.yml build
    ```

2. Run the containers:
    ```sh
    docker-compose -f docker-compose-run.yml up
    ```

The services will be available at the following URLs:
- Notification API: `http://localhost:5001`
- Rating API: `http://localhost:5002`

## Testing

To run the unit tests, use the following command:

```sh
dotnet test Notification.UnitTests/Notification.UnitTests.csproj --no-restore --verbosity normal
dotnet test Rating.UnitTests/Rating.UnitTests.csproj --no-restore --verbosity normal
