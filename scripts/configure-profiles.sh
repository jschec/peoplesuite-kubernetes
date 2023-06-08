#!/usr/bin/env bash

SCRIPTS_DIR=$( cd "$(dirname "${BASH_SOURCE[0]}")" ; pwd -P )

# Load environmental variables
source $SCRIPTS_DIR/load-env-data.sh

# Create Fargate profiles for both Employee and Department Web API services
eksctl create fargateprofile \
    --cluster $CLUSTER_NAME \
    --name employee-web-api-fargate-profile \
    --namespace employee-web-api
    
eksctl create fargateprofile \
    --cluster $CLUSTER_NAME \
    --name departments-web-api-fargate-profile \
    --namespace departments-web-api