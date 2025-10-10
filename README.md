To run this program you need to have dotnet 8 installed

# Run on Ubunto or Mac

In a ubunto or mac terminal run the following commands in the Chirp.Razor dirctory:

`sqlite3 /tmp/chirp.db < ./data/schema.sql`

`sqlite3 /tmp/chirp.db < ./data/dump.sql`

This will make the database in the tmp directory. Then run:

`dotnet run`

and the program will run

# Run on Windows
In i windows terminal you first need to set an enviroment variable, that can be done using the command:

`$env:CHIRPDBPATH = "./chirp.db" `

Then it can be run using:

`dotnet run`
