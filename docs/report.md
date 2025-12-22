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
### Prerequisites
1. Clone the repository. Follow the steps outlined in the previous section.
2. Install Playwright, which is required for the integration tests.
See the official documentation [here](https://playwright.dev/docs/intro) or if you are
on Windows, complete the following steps:
    1. Open a terminal in the project root folder.
    2. Run the following commands:
       ```
       cd .\tests\Chirp.Web.Tests\
       dotnet restore
       pwsh bin/Debug/net8.0/playwright.ps1 install
       ```
       _Tip: If the `pwsh` command does not work, your powershell version might be outdated._

### Execute tests
To run the tests...

Then, simply navigate to the project root folder in a terminal and run `dotnet test`.
This should run the entire test suite.

### Test suite describtion
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