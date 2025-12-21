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
There is going to be a picture of project board here.

Shown in the flow of activities below is the workflow of our project development. 
<br>Development starts with issue creation in GitHub, specified using user stories that describes the desired functionality and acceptance criteria.
<br>From the issue, a dedicated feature branch is created, as we work with trunk-based development. The functionality is then developed and tested in isolation.
<br>When the feature is completed, a pull request is opened and thereafter reviewed by teammates. If the pull request is approved, the changes are merged into the main branch.

![Workflow of project development](images/team_work.png)
<br>*Workflow showing the development process from issue creation to merging into the main branch.*
## How to make _Chirp!_ work locally
The Chirp! application runs on .NET 8 so you need to have that installed before you can run it. It can be downloaded here: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

To run Chirp! locally you first need to clone the repository. There are different ways to do this depending on your operation system and own preferences using git. Here is a general guide on how to do it: https://docs.github.com/en/repositories/creating-and-managing-repositories/cloning-a-repository?tool=webui 

After you have cloned the repository, open it in either a code editor or terminal and locate the solution folder. Before you can run the program you first need to navigate to the directory that has the program.cs file, which can be done with the following command:

`cd .\src\Chirp.Web\`

Now you can run Chirp! using the the command:  

`dotnet run`

If execution was succesful you should receive a terminal output that looks like so:
<br>
<img src="images/terminalOutputV2.png" alt="terminal output" width="50%"/>

Then open the url. The default is http://localhost:5273.

Because of how OAuth works, you will not be able register or login with GitHub locally, as stated in the terminal:

> Could not find Github Client ID and or Github Client Secret. OAuth with Github will not be available

If you want to try the OAuth functionallity, use the deployed version found here: https://bdsa2024group8chirprazor2025.azurewebsites.net/
## How to run test suite locally

# Ethics

## License

## LLMs, ChatGPT, CoPilot, and others
