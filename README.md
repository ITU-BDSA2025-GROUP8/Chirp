To run this program you need to have dotnet 8 installed

# Run on Linux or Mac with temperary database

Please note that you need to have sqlite 3 installed, and only works when you have downloaded the repository

In a ubunto or mac terminal run the following commands in the Chirp.Razor dirctory:

`sqlite3 /tmp/chirp.db < ./src/Chirp.Infrastructure/Data/schema.sql`

`sqlite3 /tmp/chirp.db < ./src/Chirp.Infrastructure/Data/dump.sql`

This will make the database in the tmp directory. Then run:

`dotnet run`

and the program will run

# Run on Windows using environment variables
In windows terminal you first need to set an environment variable, that can be done using the command:

`$env:CHIRPDBPATH = "./chirp.db" `

Then it can be run using:

`dotnet run`

# Run on Linux or Mac using environment variables
Set the environment variable with the comamand:

`export CHIRPDBPATH=./chirp.db`

Then it can be run using:

`dotnet run`

