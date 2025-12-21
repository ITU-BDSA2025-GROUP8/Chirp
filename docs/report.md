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

## How to run test suite locally
Once the repository has been cloned, running the tests is quite strightforward.
However, to run the tests generated using Playwright, you need to install it first using
`dotnet playwright install`.
Then, simply navigate to the project root folder in a terminal and run `dotnet test`.
This should run the entire test suite.
<br><br>
The test suite consists of unit tests pertaining to the code found in
`Chirp.Core` and `Chirp.Infrastructure`.
These ensure that the internal processing of the application's functionality are tested
thoroughly and individually.
Besides these unit tests the test suite also contains integration tests found in `Chirp.Web`.
These include both end-to-end tests of larger scenarios as well as tests of individual UI
elements.

# Ethics

## License

## LLMs, ChatGPT, CoPilot, and others