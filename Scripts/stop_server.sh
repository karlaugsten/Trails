#!/bin/bash
echo "Stopping server"
sudo systemctl stop runnify.service
sudo rm -rf /var/www/runnify.ca/*