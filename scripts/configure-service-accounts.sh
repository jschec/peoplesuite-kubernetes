#!/usr/bin/env bash

SCRIPTS_DIR=$( cd "$(dirname "${BASH_SOURCE[0]}")" ; pwd -P )

# Load environmental variables
source $SCRIPTS_DIR/load-env-data.sh

# Create service accounts for both Employee and Department Web API services
employee_policy_arn=$(aws iam create-policy \
    --policy-name EmployeeWebApiControllerIAMPolicy \
    --policy-document file://infrastructure/policies/employee-api.json \
    | jq -r .Policy.Arn )

eksctl create iamserviceaccount \
  --cluster=$CLUSTER_NAME \
  --namespace=employee-web-api \
  --name=employee-web-api-controller \
  --role-name=EmployeeWebApiControllerRole \
  --attach-policy-arn=$employee_policy_arn \
  --override-existing-serviceaccounts \
  --approve
  
department_policy_arn=$(aws iam create-policy \
    --policy-name DepartmentWebApiControllerIAMPolicy \
    --policy-document file://infrastructure/policies/department-api.json \
    | jq -r .Policy.Arn )
    
eksctl create iamserviceaccount \
  --cluster=$CLUSTER_NAME \
  --namespace=department-web-api \
  --name=department-web-api-controller \
  --role-name=DepartmentWebApiControllerRole \
  --attach-policy-arn=$department_policy_arn \
  --override-existing-serviceaccounts \
  --approve
  