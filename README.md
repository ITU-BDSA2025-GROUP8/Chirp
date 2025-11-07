# Prerequisites
You need to have dotnet 8 installed.

# Running in development environment

Navigate to the `src/Chirp.Web` folder.
Run `dotnet run`

## Register user with OAuth

If you wish to use the application's OAuth functionality, you need to create an enviroment variables file and paste in the secrets.

In `src/Chirp.Web` create a `.env` file with the following contents: <br>
AUTHENTICATION_GITHUB_CLIENTID="[Put client ID here]" <br>
AUTHENTICATION_GITHUB_CLIENTSECRET="[Put client secret here]"

Client ID and secrets are stored in the Azure vault.

# Run in production environment

Under development...


# Run from release

Under development...
