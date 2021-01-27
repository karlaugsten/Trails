#!/bin/bash
cd /home/ubuntu/runnify.ca

echo "Setting permissions"

set NODE_ENV=production
set GENERATE_SOURCEMAP=false

sudo chown -R ubuntu:ubuntu ./*

sudo chmod -R +x ./*

sudo rm -rf ./**/bin
sudo rm -rf ./**/obj

echo "Restoring dependencies..."

sudo /usr/bin/dotnet restore

cd /home/ubuntu/runnify.ca/Trails

echo "Updating database TrailContext..."
/usr/bin/dotnet ef database update --context TrailContext -v

echo "Updating database FileProcessingDbContext..."
/usr/bin/dotnet ef database update --context FileProcessingDbContext -v

echo "Building source code..."
sudo /usr/bin/dotnet publish --configuration Release ./Trails.csproj

echo "Copying source files"
sudo cp -Rrf /home/ubuntu/runnify.ca/Trails/* /var/www/runnify.ca

echo "Starting server..."
sudo systemctl start runnify.service