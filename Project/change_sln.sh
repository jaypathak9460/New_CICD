#!/bin/bash

# Define the project name or identifier you want to remove
PROJECT_NAME="TestTheProject"

# Define the path to your solution file
SOLUTION_FILE="angular_crud.sln"

# Use sed to remove the project reference from the solution file
sed -i "/$PROJECT_NAME/d" $SOLUTION_FILE
