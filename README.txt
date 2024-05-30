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
    git clone https://github.com/OmerK-Benkyzz/HomeRun_Case.git
    cd HomeRun_Case
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

### Notification API

- **GET /GetNotification/{serviceProviderId}**: Retrieves notifications for a specific service provider with pagination support.
    ```sh
    curl -X GET "http://localhost:5001/GetNotification/{serviceProviderId}?pageIndex=0&pageSize=10"
    ```

### Rating API

- **POST /RateServiceProvider**: Submits a rating for a service provider.
    ```sh
    curl -X POST http://localhost:5002/api/Rating/RateServiceProvider -H "Content-Type: application/json" -d '{"serviceProviderId": "guid", "score": 5, "comment": "Great service!"}'
    ```

- **GET /GetAvarageRating/{serviceProviderId}**: Retrieves the average rating for a specific service provider.
    ```sh
    curl -X GET http://localhost:5002/api/Rating/GetAvarageRating/{serviceProviderId}
    ```

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

## CI/CD Pipeline

This project uses a CI/CD pipeline to automate the build and deployment processes. The pipeline is configured using GitHub Actions, which are located in the `.github/workflows` directory.

### Setting Up the CI/CD Pipeline

1. Create a new branch for your feature or bug fix:
    ```sh
    git checkout -b task/your-feature-name
    ```

2. Make your changes and commit them:
    ```sh
    git add .
    git commit -m "Description of your changes"
    ```

3. Push the changes to your branch:
    ```sh
    git push origin task/your-feature-name
    ```

4. Open a pull request on GitHub to merge your branch into the `main` branch.

### CI/CD Pipeline Overview

The CI/CD pipeline performs the following tasks:
- **Build**: Compiles the application and runs unit tests.
- **Docker Build**: Builds Docker images for the services.
- **Deploy**: Deploys the Docker containers.

### Running the Pipeline Manually

If you need to run the pipeline manually, you can trigger it through the GitHub Actions interface on the repository page.

