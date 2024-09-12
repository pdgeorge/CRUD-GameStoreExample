# CRUD-GameStoreExample
Example of a CRUD using a Game Store

## Personal Notes:
These notes are written in a summarised manner for myself to understand.
If you read them and you read them as incorrect, please remember that. There are people who have many years of experience in the field who SHOULD be trusted over my summarised 'initial understanding'

### Endpoints
Endpoint setup and usage can be seen within `GamesEndpoints`
This is 'called' from `Program` with `app.MapGamesEndpoints()`


### Quickly testing endpoints
Can be done within `games.http` within vscode
Selecting "Send request" to send each of the requests
Each of those requests are examples

### DTO is Data Transfer Objects. 
The way that the each of those specific objects 'are', the way the client understands things
Can set validation information such as `[required]`, `[StringLength(x)]` or `[Range(x,y)]`
Validation additionally requires `MinimalApis.Extensions`
This validation implementation can be seen in both `CreateGameDTO` and `GamesEndpoints` and makes use of `.WithParameterValidation()`

### Entities
Mapping the external DB tables to 'our' app
`Game` is the game item as a whole, `Genre` is the sub-table/foreign key example

### Data
Requires `microsoft.entityframeworkcore.sqlite` to interract with sqlite db's
Can use other entityframeworkcore's to interract with other db's
`GameStoreContext` is the example. Inherits from `DbContext`

### Migrations
To create the db/tables/etc. you need to do a migration
Need to install `dotnet-ef` and `Microsoft.EntityFrameworkCore.Design` and run `dotnet ef migrations InitialCreate --output-dir Data\Migrations` (InitialCreate can be changed to anything. That is just suggested, such as Initial version for git repos)
Migrations now created in Data\Migrations
To run the DB update:
`dotnet ef database update`

### Mapping
To make it easy to convery from one type to another type
From external type to internal and back