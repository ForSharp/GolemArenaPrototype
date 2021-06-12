using System.Collections.Generic;
using GolemEntity;

public static class Game
{
    private static List<Golem> _allGolems;

    public static void AddToAllGolems(Golem golem)
    {
        //_allGolems.Add(golem);
    }

    public static Golem GetGolem(int index)
    {
        return _allGolems[index];
    }
}