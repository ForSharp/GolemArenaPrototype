using System.Collections.Generic;

public static class Game
{
    public static List<GameCharacterState> AllGolems = new List<GameCharacterState>();

    

    public static void AddToAllGolems(GameCharacterState golem)
    {
        AllGolems.Add(golem);
    }

    public static GameCharacterState GetGolem(int index)
    {
        return AllGolems[index];
    }
    
    
}