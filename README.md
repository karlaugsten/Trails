# Trails
Curated trail running guide website

# Tools
Built with a React.js front-end and .Net Core backend using a minimal set of dependencies.

(TODO: Integrate Redux data flow to front end.)

# Database Initialization
This project runs on a MySQL database using EF core migrations for managing the SQL schema. You will need to initialize the database by installing MySQL and running these commands to create the schema:
* `dotnet ef database update`

# Debugging
* process running on the same port? Try `kill -9 $(sudo lsof -t -i:5001)`

## Server Debugging
Debugging tips on the ubuntu web-server:
* `sudo systemctl start runnify.service` - starts the web service
* `sudo systemctl status runnify.service` - status of web service
* `journalctl -u runnify.service -e` - Latest logs of web service
* `sudo vim /etc/nginx/sites-available/runnify.ca` - edit Nginx config
* `sudo nginx -s reload` - Reload nginx
* `sudo dotnet publish --configuration Release` - Build the project!
# Server Setup
These are the commands to run to setup an Ubuntu 18.04 LTS server for running the code (For reference in case of docker or other)
## Nginx Setup
* `sudo apt update`
* `sudo apt install nginx` - Install NGINX reverse proxy
* `sudo ufw allow 'Nginx Full'` - Allow full HTTP and HTTPS access
* `sudo ufw enable`
* `sudo mkdir -p /var/www/runnify.ca/html` - Make the runnify.ca domain
* `sudo chown -R $USER:$USER /var/www/runnify.ca/html` - Change permissions
* `sudo chmod -R 755 /var/www/runnify.ca` - Permissions
* Paste the below file
  <html>
      <head>
          <title>Welcome to runnify.ca!</title>
      </head>
      <body>
          <h1>Success!  The runnify.ca server block is working!</h1>
      </body>
  </html>
Into  /var/www/runnify.ca/index.html
* `sudo vim /etc/nginx/sites-available/runnify.ca` - Paste the following text:
  server {
          listen 80;
          listen [::]:80;

          root /var/www/runnify.ca/html;
          index index.html index.htm index.nginx-debian.html;

          server_name runnify.ca www.runnify.ca;

          location / {
                  try_files $uri $uri/ =404;
          }
  }
* `sudo ln -s /etc/nginx/sites-available/runnify.ca /etc/nginx/sites-enabled/`
* `sudo systemctl restart nginx` - Restart nginx
### Lets Encrypt Setup
For HTTPS certificates, we will be using lets encrypt
* `sudo add-apt-repository ppa:certbot/certbot`
* `sudo apt install python-certbot-nginx`
* `sudo certbot --nginx -d runnify.ca -d www.runnify.ca` - Obtain the HTTPS cert!
## Installing .Net Core
* `wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb`
* `sudo dpkg -i packages-microsoft-prod.deb`
* `sudo add-apt-repository universe`
* `sudo apt install apt-transport-https`
* `sudo apt update`
* `sudo apt install dotnet-sdk-3.1`
## Install MySQL:
* Follow instructions [here](https://www.digitalocean.com/community/tutorials/how-to-install-the-latest-mysql-on-ubuntu-18-04)
* `sudo mysql -u root -p` login to mysql to initialize the database
  * `mysql> CREATE DATABASE Runnify;`
  * `mysql> CREATE USER 'runnify-admin'@'localhost' IDENTIFIED BY 'choose a secure password';` - Creates the user runnify app will use to connect to the DB with.
  * `mysql> GRANT ALL PRIVILEGES ON Runnify.* TO 'runnify-admin'@'localhost';`
  * `mysql> FLUSH PRIVILEGES;`
## Install NodeJS
* `curl -sSL https://deb.nodesource.com/gpgkey/nodesource.gpg.key | sudo apt-key add -`
* `VERSION=node_10.x`
* `DISTRO="$(lsb_release -s -c)"`
* `echo "deb https://deb.nodesource.com/$VERSION $DISTRO main" | sudo tee /etc/apt/sources.list.d/nodesource.list`
* `echo "deb-src https://deb.nodesource.com/$VERSION $DISTRO main" | sudo tee -a /etc/apt/sources.list.d/nodesource.list`
* `sudo apt-get update`
* `sudo apt-get install nodejs`
* `sudo apt-get install npm`
## Clone the repository
* `cd /var/www`
* `sudo git clone https://github.com/karlaugsten/Trails runnify.ca`
* Set the "DatabaseConnectionString" with the user you created previously
* `sudo dotnet tool install -g dotnet-ef`
* `dotnet ef database update -v` - NOTE: might have to `sudo chown ubuntu -R runnify.ca`
