# nots-agls

"Aggles" an Automated Git Labeling System. A system to bring order to the chaos of what is known as git commit messages. Using .NET 6 with Blazor & ASP.NET

## Technologies

- Blazor
- .NET Core API
- Entity Framework
- Refit
- Mapster
- SeriLog
- ML.NET

## Programming concepts

- LINQ
- Generics
- Service Pattern
- MVVM Pattern
  - ViewModels in 3 different styles!
- RPC (Remote procedure call) (Process)
- Asynchrounous programming.

## Start Dev environment

1. copy `.env.example` to `.env` and `api/appsettings.Development.example.json` to `api/appsettings.Development.json`.
   > Fill in the blanks with anything to your liking.
2. Start the database by running `docker compose up`

   > (make sure you have [docker installed](https://docs.docker.com/get-docker/) and [docker compose separately](https://docs.docker.com/compose/install/))

3. Start the api by running `cd api && dotnet watch`.

   > Starting the api will automatically launch swaggerUI in your browser, displaying the available endpoints and used data structures.

4. Start the frontend by running `cd frontend && dotnet watch`
5. You can reach the frontend via `localhost:5116`.

## Migrations:

After working on Models you need to perform a database migration. You can do this by running the following command within the `/api` folder

> `dotnet ef migrations add`
