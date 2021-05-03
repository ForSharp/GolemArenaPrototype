using System.Collections;
using System.Collections.Generic;
using __Scripts.ExtraStats;
using __Scripts.GolemEntity.Behavior;
using UnityEngine;

public class PetardCast : IFireBlast
{
    public void Cast(GolemExtraStats extraStats)
    {
        Blast(extraStats);
    }

    public void Blast(GolemExtraStats extraStats)
    {
        throw new System.NotImplementedException();
    }
}
