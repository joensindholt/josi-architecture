# Josi Architecture

An example implementation of a web api and frontend using ASP.NET Core 3.1 WebApi and Blazor - focus being on the web api part.

# Features

- Onion/Clean architecture separating core business logic, presentation logic and data access logic
- Light abstraction of the DBContext ensuring what is accessed and what is not
- Vertical feature slicing aka. "Folders by feature" instead og "Folder by type"
- Automatic built-in OpenApi documentation
- Mediator pattern keeping api controllers simple and clean
- Command/Query Seperation. Commands changes state. Queries query state.
- Entity Framework Core with Migrations
- Docker setup for dependencies (database, cache, etc.)
- Blazer Frontend
- Unit Tests using XUnit

# Todo

- Remove Mediatr. After further thought it does not bring much to the table when alreading having the ASP.NET framework at hand
- Correlation Id handling
- Security Context handling
- Add acceptance test framework focusing on how to easily handle external resources
