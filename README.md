To run this program from either development or realese you need to have dotnet 8 installed

# Running from repository code

## Run on Linux or Mac with temperary database

In a ubunto or mac terminal run the following commands in the Chirp.Razor dirctory:

`sqlite3 /tmp/chirp.db < ./src/Chirp.Infrastructure/Data/schema.sql`

`sqlite3 /tmp/chirp.db < ./src/Chirp.Infrastructure/Data/dump.sql`

This will make the database in the tmp directory. Then go to the directory that has the program.cs file, which is `src/Chirp.Web` and run:

`dotnet run`

and the program will run

## Run on Windows using environment variables
In windows terminal you first need to set an environment variable, that can be done using the command:

`$env:CHIRPDBPATH = "./chirp.db" `

Then go to the directory that has the program.cs file, which is `src/Chirp.Web` and run:

`dotnet run`

## Run on Linux or Mac using environment variables
Set the environment variable with the comamand:

`export CHIRPDBPATH=./chirp.db`

Then go to the directory that has the program.cs file, which is `src/Chirp.Web` and run:

`dotnet run`


# Run from realese

## Windows

To run from the realese files you first need to download the folder for your OS, and open the directory in a terminal.
Then you need to set the environment variable with the command:

`$env:CHIRPDBPATH = "./chirp.db" `

Then run:
`.\Chirp.Web.exe`

## Linux and Mac

To run from the realese files you first need to download the folder for your OS and open the directory in a terminal
Then you need to set the environment variable with the command:

`export CHIRPDBPATH=./chirp.db`

Then make it an executebal with the command:
`chmod +x Chirp.Web`

And then you can run it using

`./Chirp.Web`

