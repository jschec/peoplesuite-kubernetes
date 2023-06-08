# To install the application load balancer, run the following scripts:
1. ./scripts/configure-oidc.sh
2. ./scripts/assign-alb-role.sh
3. ./scripts/configure-oidc.sh
4. ./scripts/install-alb-controller.sh


# To deploy the services, run the following commands:
1. ./scripts/deploy-namespaces.sh
2. ./scripts/configure-profiles.sh
3. ./scripts/configure-service-accounts.sh
4. ./scripts/deploy-services.sh


# Retrieve logs
kubectl logs -n employee-web-api deployment/deployment-employee-web-api
kubectl logs -n department-web-api deployment/deployment-department-web-api


## Resources:
- https://docs.aws.amazon.com/eks/latest/userguide/getting-started-eksctl.html
- https://docs.aws.amazon.com/eks/latest/userguide/enable-iam-roles-for-service-accounts.html
- https://docs.aws.amazon.com/eks/latest/userguide/alb-ingress.html
- https://docs.aws.amazon.com/eks/latest/userguide/aws-load-balancer-controller.html


curl --request POST \
  --url 'https://dev-b27zvtvl0ndlrjdp.us.auth0.com/oauth/token' \
  --header 'content-type: application/x-www-form-urlencoded' \
  --data grant_type=client_credentials \
  --data 'client_id=zARSHxcPU5I0zlFezmckYpkA9P7NnXfd' \
  --data client_secret=CwxiV59Z53hZ1HWO1cbjlV2GrJrNzYIW00w1d-hHnnkdVYwBJOeR-_RfXwl0smmQ \
  --data 'audience=k8s-departme-ingressd-162b356b6f-1919503288.us-east-1.elb.amazonaws.com'


curl --request POST \
  --url 'https://dev-b27zvtvl0ndlrjdp.us.auth0.com/oauth/token' \
  --header 'content-type: application/x-www-form-urlencoded' \
  --data grant_type=client_credentials \
  --data 'client_id=3WJAijjOg9WN0xAU1VP5qHH7NurhS1ZI' \
  --data client_secret=HyIJx4IiPxjaCH3OzvhZHEQOBiY-XUmhVzu9Pu1P8SrhC5MGW1zoEjn2ufnltX1f \
  --data 'audience=k8s-employee-ingresse-60943369d2-726969466.us-east-1.elb.amazonaws.com'
