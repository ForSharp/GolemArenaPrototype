using System.Collections.Generic;
using Fight;

namespace GameLoop
{
    public class Game
    {
        public List<GameCharacterState> AllGolems = new List<GameCharacterState>();

        public void AddToAllGolems(GameCharacterState golem)
        {
            AllGolems.Add(golem);
        }

        public void PrepareNewRound()
        {
            
        }
    
    }
}