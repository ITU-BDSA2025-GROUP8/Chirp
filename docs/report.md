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
The building, testing, releasing and deploying with GitHub is done using GitHub Actions and their respective workflow. For this project there are 3 workflow files that completes those actions.    
The first workflow builds and tests the Chirp! application upon a push or pull request to the main branch. The activity diagram for the workflow can be seen below, where the note for the initial node also mentions the condition for when the workflow runs.

![build and test](images/build_and_test_activity_diagram.png) <br> *Activity diagram for building and testing Chirp!*

The second workflow builds and releases the Chirp! application upon pushing a tag starting with a v. The activity diagram for the workflow can be seen below, where it also shows that the release job only runs if the build job succeeds.

![build and release](images/build_and_release_activity_diagram.png) <br> *Activity diagram for building and releasing Chirp!* 

The last workflow builds and deploys the Chirp! application to Azure upon a push to main. The activity diagram for the workflow can be seen below, where it also shows that the deploy job only runs if the build job succeeds.

![build and deploy](images/build_and_deploy_activity_diagram.png) <br>  *Activity diagram for building and deploying Chirp! to Azure*


## Team work
There is going to be a picture of project board here.

Shown in the flow of activities below is the workflow of our project development. 
<br>Development starts with issue creation in GitHub, specified using user stories that describes the desired functionality and acceptance criteria.
<br>After issue creation, the team agrees on responsibility for the task, researching the subject and consulting the lecture material. A dedicated feature branch is created, as we work with trunk-based development. The functionality is then developed and tested in isolation, often utilizing pair programming.
<br>When the feature is completed, a pull request is opened and thereafter reviewed by teammates. If the pull request is not approved, the assignee makes changes according to reviewer feedback. Once approved, the changes are merged into the main branch.

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
### Prerequisites
1. Clone the repository. Follow the steps outlined in the previous section.
2. Install Playwright, which is required for the integration tests.
Follow the official documentation [here](https://playwright.dev/docs/intro) - or if you are
on Windows, install by running the following commands from the project root:
    ```
    cd .\tests\Chirp.Web.Tests\
    dotnet restore
    pwsh bin/Debug/net8.0/playwright.ps1 install
    ```
    _Tip: If the `pwsh` command does not work, your powershell version might be outdated._

### Execute tests
To run the tests, open a terminal in the project root and run `dotnet test`.
This should run the entire test suite.

### Test suite description
The test suite consists of unit tests pertaining to the code found in
`Chirp.Core` and `Chirp.Infrastructure`.
These ensure that the internal processing of the application's functionality are tested
thoroughly and individually.
Besides these unit tests the test suite also contains integration tests found in `Chirp.Web`.
These include both end-to-end tests of larger scenarios and tests of individual UI
elements.

# Ethics

## License

## LLMs, ChatGPT, CoPilot, and others
