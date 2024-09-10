namespace GameStore.dto;

public record class CreateGameDTO(
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);