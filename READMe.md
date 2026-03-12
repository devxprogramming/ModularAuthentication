# ModularAuth

ModularAuth is a microservice-based authentication and authorization system. It provides a modular architecture that allows you to easily integrate authentication and authorization into your existing applications.

## Prerequisites

Before running the project, make sure you have the following prerequisites installed:

- Docker
- Docker Compose

## Getting Started

To get started with ModularAuth, follow these steps:

1. Clone the repository:
`git clone https://github.com/your-username/modularauth.git`

2. Navigate to the project directory:
`cd modularauth`

## Running the Project with a single command from run.sh

To run the project, you can use the `run.sh` script. This script will build the Docker images, run the Docker containers, and open the API documentation.

To run the project, open a terminal and navigate to the project directory. Then, run the following command:

`chmod +x run.sh` # this will make the script executable


`./run.sh` # this will run the script

Disclaimer
Using the `run.sh` script will automatically build the Docker images, run the Docker containers, and open the API documentation.



If you want to run the project locally step by step, you can follow these procedures:

3. Build the Docker images:
`docker-compose build`

4. Run the Docker containers:
`docker-compose up -d`

5. Access the API documentation:
Open your web browser and navigate to http://localhost:5191/swagger

## Contributing

We welcome contributions to the ModularAuth project. Please follow the [Contributing Guidelines](https://github.com/your-username/modularauth/blob/main/CONTRIBUTING.md) to contribute.

## License
