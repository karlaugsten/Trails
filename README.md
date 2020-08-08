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

