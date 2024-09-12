using CRUD_GameStoreExample.Entities;
using GameStore.dto;

namespace CRUD_GameStoreExample.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto newGame)
    {
            return new Game()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };
    }
    
    public static Game ToEntity(this UpdateGameDto newGame, int id)
    {
            return new Game()
            {
                Id = id,
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };
    }

    public static GameSummaryDto ToGameSummaryDto(this Game game)
    {
            return new GameSummaryDto(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate);
    }

    public static GameDetailsDto ToGameDetailsDto(this Game game)
    {
            return new GameDetailsDto(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate);
    }
}
