# Prerequisites
You need to have dotnet 8 installed.

# Running in development environment

Navigate to the `src/Chirp.Web` folder.
Run `dotnet run`

## Register user with OAuth

If you wish to use the application's OAuth functionality, you need to create an enviroment variables file and paste in the secrets.

In `src/Chirp.Web` create a `.env` file with the following contents: <br>
```
AUTHENTICATION_GITHUB_CLIENTID="[Put client ID here]"
AUTHENTICATION_GITHUB_CLIENTSECRET="[Put client secret here]"
```
Client ID and secrets are stored in the Azure for this web app, bdsa2024group8chirprazor2025, under settings/environment variables.

There should be a `env.sample` file in the same folder, you can use as a template.


# Run from release

To run from release download the release for your operating system and run the Chirp.Web executable. In the realeased version you can not registor or login using github. If you want those functions you can use the deployed version at https://bdsa2024group8chirprazor2025.azurewebsites.net/

# Deployed version

The deployed version is the same as the released version and can be found at https://bdsa2024group8chirprazor2025.azurewebsites.net/
