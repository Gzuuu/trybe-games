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
        // Implementar
        throw new NotImplementedException();
    }

}
