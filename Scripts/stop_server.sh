#!/bin/bash
echo "Stopping server"
sudo systemctl stop runnify.service
sleep 5
sudo rm -rf /var/www/runnify.ca/*
