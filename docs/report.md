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
The code base is structured according to the onion achitecture template,
with one notable exception. Usually, the domain entities are situated within core of the application
and this also was the case for this app, before EF Core Identity was implemented.
With this task came some decesions about how to handle the author/user semantics.
Ultimately, it was decided that EF Core users and cheep authors would be combined
to avoid complications between different types of user accounts.
As a result, the domain entities were moved to `Chirp.Infrastructure`
to accomodate EF Core Identity.
Besides this discrepancy, the Chirp! application adheres to the standard onion architecture template.
Below is an overview of the codebase structure.

![Illustration of code base](images/onion_architecture.png)
*Illustration of the Chirp! app codebase structure - based on onion architecture.*

- `Chirp.Core` contains the interfaces and data transfer objects (DTOs).
- `Chirp.Infrastructure` contains the domain entities, repositories and the Data package,
which is responsible for the DB context and initializing.
- `Chirp.Web` contains the Program.cs file, all razor pages and their respective page handlers.
The Cheep and Author services are also located here.
- The test suite resides in a seperate directory that mirrors the structure of the source code. I.e. the tests for
`src/Chirp.Infrastructure/Repositories/CheepRepository.cs`
are found in
`tests/Chirp.Infrastructure.Tests/Repositories/CheepRepositoryTest.cs`.
UI and end-to-end tests generated using playwright are located in `Chirp.Web.Tests`.

## Architecture of deployed application

## User activities
The first page any Chirp! user sees is the public timeline which displays all cheeps.
Unauthorized users are limited to browsing cheeps on this public timeline
and visiting other author's private timelines. As illustrated by the diagram below,
the majority of the app's functionality is exclusively available to authorized users.
Specifically, creating cheeps, liking cheeps and following other authors.
Authorized users can also view their user-information on the About Me page,
as well as choose to delete their account from the application.

![Illustration of code base](images/user_activities.png)
*Illustration of a typical user journey through the Chirp! application.*

## Sequence of functionality/calls trough _Chirp!_

# Process

## Build, test, release, and deployment

## Team work

## How to make _Chirp!_ work locally

## How to run test suite locally

# Ethics

## License

## LLMs, ChatGPT, CoPilot, and others
