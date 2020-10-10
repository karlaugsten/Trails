#!/bin/bash
echo "Copying deployment files"

sudo cp -R /home/ubuntu/runnify.ca /var/www/runnify.ca

cd /var/www/runnify.ca

echo "Setting permissions"

set NODE_ENV=production
set GENERATE_SOURCEMAP=false

sudo chown -R ubuntu:ubuntu ./*

sudo chmod -R +x ./*

echo "Restoring dependencies..."
sudo /usr/bin/dotnet restore

echo "Updating database..."
/usr/bin/dotnet ef database update

echo "Building source code..."
sudo /usr/bin/dotnet publish --configuration Release

echo "Starting server..."
sudo systemctl start runnify.service