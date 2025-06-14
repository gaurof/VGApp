using VGAppDb.Models;

namespace VGApp.ViewModels;

public class GameWithUserViewModel
{
    public required Game Game;
    public User? User;
}

public class UserWithGamesViewModel
{
    public List<Game> Games = [];
    public User? User;
}