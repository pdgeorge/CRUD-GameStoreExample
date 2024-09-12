using System;
using CRUD_GameStoreExample.Data;
using CRUD_GameStoreExample.Entities;
using CRUD_GameStoreExample.Mapping;
using GameStore.dto;
using Microsoft.EntityFrameworkCore;

namespace CRUD_GameStoreExample.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameSummaryDto> games = [
        new (
            1,
            "Street Fighter 2",
            "Fighting",
            19.99M,
            new DateOnly(1992, 7, 15)),
        new (
            2,
            "Final Fantacy 14",
            "RPG",
            29.99M,
            new DateOnly(2010, 7, 15)),
        new (
            3,
            "FIFA 23",
            "Sports",
            69.99M,
            new DateOnly(2022, 7, 15))
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        // Added NuGet package MinimalApis.Extensions 0.11.0 to add WithParameterValidation() to group
        // DTO's each have validations attached
        var group = app.MapGroup("games")
                        .WithParameterValidation();

        // GET /games
        group.MapGet("/", () => games);

        // GET /games/1
        group.MapGet("/{id}", (int id, GameStoreContext dbContext) => 
        {
            // Searches the cache first, then searches the DB
            Game? game = dbContext.Games.Find(id);

            return game is null ?
                Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
            .WithName(GetGameEndpointName);

        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            // Taking the received information, converting to common type
            Game game = newGame.ToEntity();
            // Would use this line if we were returning Genre as a string
            // game.Genre = dbContext.Genres.Find(game.GenreId);

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            

            return Results.CreatedAtRoute(
                GetGameEndpointName,
                new { id = game.Id },
                game.ToGameDetailsDto());
        });

        // PUT /games/1
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) => 
        {
            var existingGame = dbContext.Games.Find(id);

            // If unable to find in list, index = -1
            // Other Option is to turn Put into Post.
            // However, that risks conflicting Id's
            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
                    .CurrentValues
                    .SetValues(updatedGame.ToEntity(id));
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", (int id, GameStoreContext dbContext) => 
        {
            dbContext.Games
                     .Where(game => game.Id == id)
                     .ExecuteDelete();

            return Results.NoContent();
        });

        return group;
    }
}
