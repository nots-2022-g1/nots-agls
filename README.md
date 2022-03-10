# nots-agls

"Aggles"

Automated Git Labeling System

A system to bring order to the chaos of what is known as git commit messages.

Using .NET 6 with Blazor & ASP.NET

## Usage

copy `.env.example` to `.env` and `api/appsettings.Development.example.json` to `api/appsettings.Development.json`. Fill in the blanks with anything to your liking.

Start the db by running `docker compose up -d` (make sure you have [docker installed](https://docs.docker.com/get-docker/) and [docker compose separately](https://docs.docker.com/compose/install/) only on linux)

Starting the api will automatically launch swaggerUI in your browser, displaying the available endpoints and used data structures.
