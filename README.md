# Josi Architecture

Boilerplate implementation of a web api and frontend using .NET 7 WebApi and Blazor - focus being on the web api part.

## Features

- Onion/Clean architecture separating core business logic, presentation logic and data access logic
- Vertical feature slicing aka. "Folders by feature" instead og "Folder by type"
- Automatic built-in OpenApi documentation
- Mediator pattern keeping api controllers simple and clean
- Entity Framework Core with Migrations
- Docker setup for dependencies (database, cache, etc.)
- Blazer Frontend
- Unit Tests using XUnit

## Todo

- Correlation id's
- Security context handling
- Add acceptance test framework focusing on how to easily handle external resources
- Add slugs to avoid exposing internal id's. This increases security by not allowing a user to guess other entity id's
- Background work handling

## Azure

Create Service Principal
```
az ad sp create-for-rbac --role="Contributor" --scopes="/subscriptions/fd949dc3-162d-4991-a986-02631830a313"
```