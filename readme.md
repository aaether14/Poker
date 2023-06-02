# Poker Hands API

The Poker Hands API is a web application that provides a RESTful API for rolling and comparing
poker hands.

## Installation

To install and run the Poker Hands API, follow these steps:

1. Make sure you have Docker installed on your system.
2. Clone the repository to your local machine.
3. Build the Docker image using `docker build -t poker-hands-api .`
4. Run the Docker container using `docker run -p 5000:80 -e ASPNETCORE_ENVIRONMENT=Development poker-hands-api`

The API will be accessible at http://localhost:5000, and the Swagger API documentation can be found at http://localhost:5000/swagger.

## Example Requests

Example requests for interacting with the Poker Hands API are available in the [Requests](Requests/) folder. These requests are in HTTP format and can be used with tools like cURL or Postman.

## Unit tests

Some unit tests are available in the [Poker.Domain.Tests](Poker.Domain.Tests/) project.