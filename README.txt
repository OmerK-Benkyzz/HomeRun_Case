# HomeRun Case

This project includes Notification and Rating microservices. It uses RabbitMQ message queue to facilitate communication between these services.

## Project Purpose
This project aims to run Notification and Rating services within Docker containers using microservice architecture and to facilitate communication between these services using RabbitMQ.

## Installation and Running

### Requirements
- Docker
- Docker Compose

### Running the Project with Docker Compose
Follow these steps to run the project using Docker Compose:

1. Navigate to the project directory:
    ```sh
    cd /path/to/HomeRun_Case
    ```

2. Start all services with Docker Compose:
    ```sh
    docker-compose up --build
    ```

### Services and Ports

- **Notification Service**: http://localhost:5001
- **Rating Service**: http://localhost:5002
- **RabbitMQ Management**: http://localhost:15672 (default user: guest, password: guest)

## API Endpoints

### Notification API
- **GET /notifications**: Retrieves all notifications.
    ```sh
    curl -X GET http://localhost:5001/notifications
    ```
- **POST /notifications**: Creates a new notification.
    ```sh
    curl -X POST http://localhost:5001/notifications -H "Content-Type: application/json" -d '{"message": "Hello, World!"}'
    ```

### Rating API
- **GET /ratings**: Retrieves all ratings.
    ```sh
    curl -X GET http://localhost:5002/ratings
    ```
- **POST /ratings**: Creates a new rating.
    ```sh
    curl -X POST http://localhost:5002/ratings -H "Content-Type: application/json" -d '{"score": 5, "comment": "Great service!"}'
    ```

## Project Structure

