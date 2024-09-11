using CRUD_GameStoreExample.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// We have extended the WebApplication class and added MapGamesEndpoints
// That is where the endpoints are located now. Alongside the initial data
app.MapGamesEndpoints();

app.Run();
