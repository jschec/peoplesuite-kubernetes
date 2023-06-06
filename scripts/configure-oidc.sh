#!/usr/bin/env bash

SCRIPTS_DIR=$( cd "$(dirname "${BASH_SOURCE[0]}")" ; pwd -P )

# Load environmental variables
source $SCRIPTS_DIR/load-env-data.sh

# Check if an OIDC provider is already registered to the cluster
oidc_id=$(aws eks describe-cluster --name $CLUSTER_NAME --query "cluster.identity.oidc.issuer" --output text | cut -d '/' -f 5)
aws iam list-open-id-connect-providers | grep $oidc_id | cut -d "/" -f4

eksctl utils associate-iam-oidc-provider --cluster $CLUSTER_NAME --approve