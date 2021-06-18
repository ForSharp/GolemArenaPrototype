using System.Collections.Generic;

public static class Game
{
    private static List<GameCharacterState> _allGolems = new List<GameCharacterState>();

    public static void AddToAllGolems(GameCharacterState golem)
    {
        _allGolems.Add(golem);
    }

    public static GameCharacterState GetGolem(int index)
    {
        return _allGolems[index];
    }
    
    
}