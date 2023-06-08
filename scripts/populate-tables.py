import json
import boto3
dynamodb = boto3.resource('dynamodb')

def bulk_write(table_name: str, json_path: str) -> None:
    table = dynamodb.Table(table_name)
    items = []
    
    with open(json_path) as f:
        file_contents = f.read()
        items = json.loads(file_contents)
      
    with table.batch_writer() as batch:
        for item in items:
            batch.put_item(Item=item)
            

if __name__ == '__main__':
    bulk_write('department', './services/DepartmentService/Data/departments.json')
    bulk_write('employee', './services/EmployeeService/Data/employees.json')