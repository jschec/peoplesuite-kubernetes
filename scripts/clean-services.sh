#!/usr/bin/env bash

kubectl delete -f ./services/EmployeeService/app-spec.yaml

kubectl delete -f ./services/DepartmentService/app-spec.yaml