#!/usr/bin/env bash

SCRIPTS_DIR=$( cd "$(dirname "${BASH_SOURCE[0]}")" ; pwd -P )

# Load environmental variables
source $SCRIPTS_DIR/load-env-data.sh

# Create ALB Policy
policy_arn=$(aws iam create-policy \
    --policy-name EksFargateLoggingPolicy \
    --policy-document file://infrastructure/observability-policy.json \
    | jq -r .Policy.Arn )

# Create service account
aws iam attach-role-policy \
  --policy-arn $policy_arn \
  --role-name $CLUSTER_ROLE_NAME