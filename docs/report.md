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
![Illustration of code base](docs/diagrams/onion_architecture.png)
<br><br>
TODO: SERVICES WILL BE MOVED TO INFRASTRUCTURE.
ENTITIES IN INFRASTRUCTURE BECAUSE EF CORE - BRIEFLY EXPLAIN WHY
<br><br>
The code base is structured according to the onion achitecture template,
with a couple of notable exceptions.
<br><br>
`Chirp.Core` contains the interfaces and data transfer objects (DTOs).
<br>
`Chirp.Infrastructure` contains the domain entities, repositories and the Data package,
which is responsible for the DB context and initializing.
<br>
`Chirp.Web` contains the Program.cs file and all razor pages and their respective page handlers.
The Cheep and Author services are also located here.
<br><br>
The test suite directory mirrors that of the source code. I.e. the tests for
`src/Chirp.Infrastructure/Repositories/CheepRepository.cs`
are found in
`tests/Chirp.Infrastructure.Tests/Repositories/CheepRepositoryTest.cs`.
UI and end-to-end tests generated using playwright are located in `Chirp.Web.Tests`.

## Architecture of deployed application

## User activities

## Sequence of functionality/calls trough _Chirp!_

# Process

## Build, test, release, and deployment

## Team work

## How to make _Chirp!_ work locally

## How to run test suite locally

# Ethics

## License

## LLMs, ChatGPT, CoPilot, and others