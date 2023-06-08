#!/usr/bin/env bash
set -o allexport

export AWS_ACCOUNT_ID=$(aws sts get-caller-identity --query "Account" --output text)
export AWS_REGION=us-east-1
export CLUSTER_NAME=people-suite
export CLUSTER_ROLE_NAME=eksctl-people-suite-cluster-ServiceRole-1T177WG0YIXDZ
export VPC_ID=vpc-039f46a722a0fa7c0
export ECR_ENDPOINT=$AWS_ACCOUNT_ID.dkr.ecr.$AWS_REGION.amazonaws.com
export ECR_REPO=peoplesuite
export DEPARTMENT_SERVICE_TAG=department
export EMPLOYEE_SERVICE_TAG=employee
export ECR_EMPLOYEE_SERVICE=$ECR_ENDPOINT/$ECR_REPO:$EMPLOYEE_SERVICE_TAG
export ECR_DEPARTMENT_SERVICE=$ECR_ENDPOINT/$ECR_REPO:$DEPARTMENT_SERVICE_TAG

set +o allexport