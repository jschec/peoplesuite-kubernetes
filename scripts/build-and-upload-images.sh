#!/usr/bin/env bash

SCRIPTS_DIR=$( cd "$(dirname "${BASH_SOURCE[0]}")" ; pwd -P )

# Load environmental variables
source $SCRIPTS_DIR/load-env-data.sh

# Building the service images
docker build -f ./services/DepartmentService/Dockerfile -t $ECR_DEPARTMENT_SERVICE .
docker build -f ./services/EmployeeService/Dockerfile -t $ECR_EMPLOYEE_SERVICE .

# Authenticate against AWS ECR
aws ecr get-login-password \
  --region $AWS_REGION | docker login \
  --username AWS \
  --password-stdin $ECR_ENDPOINT

# Upload built images
docker push $ECR_DEPARTMENT_SERVICE
docker push $ECR_EMPLOYEE_SERVICE
