#!/bin/bash
cd /var/www/runnify.ca

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