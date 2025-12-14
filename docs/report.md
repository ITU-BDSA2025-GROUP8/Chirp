---
title: _Chirp!_ Project Report
subtitle: ITU BDSA 2025 Group 8
author:
- "Charlotte Plateig <cpla@itu.dk>"
- "Frederik Hørup <frap@itu.dk>"
- "Marie Johansen <majoh@itu.dk>"
- "Nikolej Lundquist <nivl@itu.dk>"
- "Sara Bagger <salb@itu.dk>"
numbersections: true
---

# Design and Architecture of _Chirp!_

## Domain model

Here comes a description of our domain model.

![Illustration of the _Chirp!_ data model as UML class diagram.](docs/images/domain_model.png)

## Architecture — In the small

## Architecture of deployed application

## User activities

## Sequence of functionality/calls trough _Chirp!_

# Process

## Build, test, release, and deployment

## Team work

## How to make _Chirp!_ work locally
The Chirp application is made using dotnet 8 so you need to have that installed before you can run it. If you dont have it installed it can be done here: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

To run Chirp locally you first need to clone the repository. There are different ways to do this depending on you operation system and own preferences using git, but here is a general guide on how to do it: https://docs.github.com/en/repositories/creating-and-managing-repositories/cloning-a-repository?tool=webui 

After you have cloned the repository and have opened it in either a code editor or terminal you will be in the solution folder. Before you can run the program you first need to change to the directory that has the program.cs file. This can be done with the following command:

`cd .\src\Chirp.Web\`

Now you can run Chirp using the the command:  

`dotnet run`

After running the command the terminal should have output something that looks like this:  
<img src="images/terminalOutputV2.png" alt="terminal output" width="50%"/>

Then click on the url, for the picture it is http://localhost:5273 to see the program runnning 

Because of how OAuth works, you will not be able use that functionallity locally which can also be seen in the terminal where it writtes:

> Could not find Github Client ID and or Github Client Secret. OAuth with Github will not be available

If you want to try the OAuth functionallity, use the deployed version which can be found here: https://bdsa2024group8chirprazor2025.azurewebsites.net/
## How to run test suite locally

# Ethics

## License

## LLMs, ChatGPT, CoPilot, and others
