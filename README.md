

kubectl apply -f infrastructure/aws-observability-namespace.yaml
kubectl apply -f infrastructure/aws-logging-cloudwatch-configmap.yaml



# Run The configuration scripts in the following order:
1. ./scripts/configure-oidc.sh
2. ./scripts/assign-alb-role.sh
3. ./scripts/install-alb-controller.sh


https://docs.aws.amazon.com/eks/latest/userguide/getting-started-eksctl.html


https://docs.aws.amazon.com/eks/latest/userguide/enable-iam-roles-for-service-accounts.html


https://docs.aws.amazon.com/eks/latest/userguide/alb-ingress.html
https://docs.aws.amazon.com/eks/latest/userguide/aws-load-balancer-controller.html