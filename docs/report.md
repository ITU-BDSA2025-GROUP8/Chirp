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
The first workflow builds and tests the Chirp application when a pull request that wants to merge into main is made and when something is pushed to main. The activity diagram for the workflow can be seen below, where the note also mentions the condition for when the workflow runs.

![build and test](images/build_and_test_activity_diagram.png) *Activity diagram for buildiong and testing Chirp*

The second workflows builds and releases the Chirp application and the activity diagram can be seen below. As the note points out this workflow only runs when a tag starting with v is pushed. The release job also only runs if the build job succeeds.

![build and release](images/build_and_release_activity_diagram.png) *Activity diagram for buildning and releasing Chirp* 

The last workflow builds and deploys the Chirp application to Azure. The activity diagram can be seen below. Here it only runs when there has been a push to main and, like the previous workflow, the deploy job only runs if the build job succeeds.

![build and deploy](images/build_and_deploy_activity_diagram.png) *Actvivity diagram for building and deploying Chirp to Azure*


## Team work

## How to make _Chirp!_ work locally

## How to run test suite locally

# Ethics

## License

## LLMs, ChatGPT, CoPilot, and others
