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
The Domain Model of the Chirp! application consists of `Cheep`, `Author` and `IdentityUser`. A `Cheep` is written by an `Author` that inherits from `IdentityUser` as visualized below.

![Illustration of the _Chirp!_ data model as UML class diagram.](images/domain_model.png)
<br>
*Illustration of the _Chirp!_ Domain Model*

## Architecture — In the small
The code base is structured according to the onion architecture template,
with one notable exception. Usually, the domain entities are situated within the core of the application,
which was also the case for this app, before EF Core Identity was implemented.
With this task came some decisions about how to handle the author/Identity user semantics.
Ultimately, it was decided that Identity users and cheep authors would be combined
by having the Author class inherit from Identity user,
to avoid complications between different types of user accounts.
As a result, the domain entities were moved to `Chirp.Infrastructure`
to accommodate this decision.
Besides this discrepancy, the Chirp! application adheres to the standard onion architecture template.
Below is an overview of the codebase structure.

![Illustration of code base](images/onion_architecture.png)
<br>
*Illustration of the Chirp! app codebase structure - based on onion architecture.*

- `Chirp.Core` contains the interfaces and data transfer objects (DTOs).
- `Chirp.Infrastructure` contains the domain entities, repositories, and the Data package,
which is responsible for the DB context and initializing. The Cheep and Author services are also situated here.
- `Chirp.Web` contains the Program.cs file, all razor pages and their respective page handlers.
- The test suite resides in a separate directory that mirrors the structure of the source code. I.e. the tests for
`src/Chirp.Infrastructure/Repositories/CheepRepository.cs`
are found in
`tests/Chirp.Infrastructure.Tests/Repositories/CheepRepositoryTest.cs`.
UI and end-to-end tests generated using Playwright are located in `Chirp.Web.Tests`.

## Architecture of deployed application
The diagram below shows the deployment architecture of the Chirp! application. The system follows a client-server architecture, where users interact with the application through a web browser. Client requests are sent over HTTPS to an ASP.NET Core Razor Pages application deployed on Azure App Service.
<br> The web application is responsible for handling application logic. Data is stored in a local SQLite database file that is deployed together with the application. A single client node is shown in the diagram, representing multiple possible concurrent clients can interact with the deployed application.
<br>
![Illustration of architecture of deployed application](images/architecture_deployed_application.png)
<br>
*Deployment architecture of the Chirp! application.*

## User activities
The first page any Chirp! user sees is the public timeline which displays all cheeps.
Unauthorized users are limited to browsing cheeps on this public timeline
and visiting other authors' private timelines. If a user wishes to further interact with
cheeps and authors, authentication is required as illustrated by the diagram below.

![Illustration of unauthorized user and authentication process](images/user_activities_unauthorized.png)
<br>
*A typical unathorized user's journey before and through the authentication process.*

The majority of the app's functionality is exclusively available to authorized users.
Specifically, creating cheeps, liking cheeps and following other authors.
Authorized users can also view their user-information on the About Me page,
as well as choose to delete their account from the application.

![Illustration of code base](images/user_activities_authorized_alt.png)
<br>
*A typical authorized user's journey through the Chirp! application.*

## Sequence of functionality/calls trough _Chirp!_
![Illustration of code base](images/sequence_of_functionality_unauthorized.png)
<br>
*Illustration of the sequence of functionality/calls trough _Chirp!_ from an unauthorized user's perspective.*

The image above illustrates the sequence of interactions which occurs, when an unauthorized user first 
accesses the application on the public timeline. An HTTP GET request to the root endpoint "/" is received by 
ASP.Net Core, which calls the Public Model's (`public.cshtml.cs`) OnGetAsync method.
From there, the user is identified as unauthenticated by the ASP.NET Core authentication system and treated as 
anonymous. Through a couple of lifelines, a list of cheeps is collected and returned to ASP.Net Core. ASP.Net 
Core then renders the `public.cshtml` page through the Razor Page engine and returns the rendered HTML 
to the user.
TimelineBaseModel is illustrated in the diagram to for transparancy about communication as the calls between the 
Razor Pages otherwise would be be hidden due to being internal.



# Process

## Build, test, release, and deployment

## Team work

## How to make _Chirp!_ work locally

## How to run test suite locally

# Ethics

## License
This project is licensed under the MIT License. The MIT License was chosen since the nuget packages this project uses are either licensed under the MIT License or Apache-2.0 License. Since these licenses are permissive software licenses there does not arise any conflicts using the MIT License. 

## LLMs, ChatGPT, CoPilot, and others
ChatGPT and, to a smaller degree, Microsoft Copilot were used during the development of this project.
Let's briefly discuss how these were used and how helpful they were.
<br><br>
Debugging. Since no group members have prior experience with C# and many other concepts of this course,
when particularly tricky errors occured LLMs were frequently used to help debug the problem. However,
we would strive to first try and solve it ourselves. In this use case LLMs were invaluable,
as they served as a TA guiding us through the debugging process, in cases where Googling was not sufficient and no actual TA was nearby.
<br><br>
Coder's block. As mentioned, this project introduced many new and unfamiliar concepts,
which meant that for some tasks, we would have no clue where to begin.
LLMs were used as a spring board in these cases to get started on a new feature for example.
Specifically, when refactoring to Onion Architecture ChatGPT was used to help translate theory into practice
by suggesting a template for how our project should be organized. This saved a lot of time making decisions
and discussing semantics with the rest of the group, which allowed everyone to focus on their own tasks.
<br><br>
Code generation. Very little code in this project was generated by LLMs and copy/pasted directly,
and in cases where it was, it is marked by comments and/or in the commit message as co-author.
The group feels largely unconfortable using AI generated code and opted to only use it in certain cases.
For example, the GitHub workflows are mostly generated by ChatGPT and modified afterwards.
As these workflows are mostly boilerplate and not part of the application code,
it did not take away from our learning experience but allowed us to prioritize our time and ressources in other areas.
Additionally, smaller snippits were copy/pasted from ChatGPT when debugging.
<br><br>
Overall, the use of LLMs, ChatGPT in particular, has indeed sped up development significantly.
Not so much as a code-generation tool, but as a virtual, on-demand TA that helped
understanding new concepts, getting started on new tasks, and debug persistant errors.
