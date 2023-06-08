#!/usr/bin/env bash

# Deploy Department Web API
#kubectl apply -f ./services/DepartmentService/app-spec.yaml

# Deploy Employee Web API
kubectl apply -f ./services/EmployeeService/app-spec.yaml