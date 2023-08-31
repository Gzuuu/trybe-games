using System.Reflection.Metadata.Ecma335;

namespace TrybeGames;

public class TrybeGamesDatabase
{
    public List<Game> Games = new List<Game>();

    public List<GameStudio> GameStudios = new List<GameStudio>();

    public List<Player> Players = new List<Player>();

    // 4. Crie a funcionalidade de buscar jogos desenvolvidos por um estúdio de jogos
    public List<Game> GetGamesDevelopedBy(GameStudio gameStudio)
    {
        // implementar
        IEnumerable<Game> filteredGames = from game in Games
        where game.DeveloperStudio == gameStudio.Id
        select game;

        return filteredGames.ToList();
    }

    // 5. Crie a funcionalidade de buscar jogos jogados por uma pessoa jogadora
    public List<Game> GetGamesPlayedBy(Player player)
    {
        // Implementar
        IEnumerable<Game> filteredGames = from game in Games
        from playerId in game.Players
        where playerId == player.Id
        select game;

        return filteredGames.ToList();
    }

    // 6. Crie a funcionalidade de buscar jogos comprados por uma pessoa jogadora
    public List<Game> GetGamesOwnedBy(Player playerEntry)
    {
        IEnumerable<Game> filteredGames = from game in Games
        from gameId in playerEntry.GamesOwned
        where gameId == game.Id
        select game;

        return filteredGames.ToList();
    }


    // 7. Crie a funcionalidade de buscar todos os jogos junto do nome do estúdio desenvolvedor
    public List<GameWithStudio> GetGamesWithStudio()
    {
        var filteredGames = from game in Games
        from studio in GameStudios
        where game.DeveloperStudio == studio.Id
        select new GameWithStudio{
            GameName = game.Name,
            StudioName = studio.Name,
            NumberOfPlayers = game.Players.Count,
        };

        return filteredGames.ToList();                    
    }
    
    // 8. Crie a funcionalidade de buscar todos os diferentes Tipos de jogos dentre os jogos cadastrados
    public List<GameType> GetGameTypes()
    {
        var filteredGamesTypes = from game in Games
        select game.GameType;
        return filteredGamesTypes.Distinct().ToList();
    }

    // 9. Crie a funcionalidade de buscar todos os estúdios de jogos junto dos seus jogos desenvolvidos com suas pessoas jogadoras
    public List<StudioGamesPlayers> GetStudiosWithGamesAndPlayers()
    {
        var result = from studio in GameStudios
                 join game in Games on studio.Id equals game.DeveloperStudio
                 from playerId in game.Players
                 join player in Players on playerId equals player.Id
                 where studio.Id == game.DeveloperStudio
                 select new { Studio = studio, Game = game, Player = player } into data
                 group data by data.Studio into studioGroup
                 select new StudioGamesPlayers
                 {
                     GameStudioName = studioGroup.Key.Name,
                     Games = studioGroup.GroupBy(groupItem => groupItem.Game).Select(gameGroup => new GamePlayer
                     {
                         GameName = gameGroup.Key.Name,
                         Players = gameGroup.Select(item => item.Player).ToList()
                     }).ToList()
                 };

    return result.ToList();
    }
}
