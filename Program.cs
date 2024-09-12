using CRUD_GameStoreExample.Data;
using CRUD_GameStoreExample.Endpoints;

var builder = WebApplication.CreateBuilder(args);

//connfiguration will be found in appsettings.json
var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

// We have extended the WebApplication class and added MapGamesEndpoints
// That is where the endpoints are located now. Alongside the initial data
app.MapGamesEndpoints();

app.MigrateDb();

app.Run();
