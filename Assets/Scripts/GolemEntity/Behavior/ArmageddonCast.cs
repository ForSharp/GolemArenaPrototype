using System.Collections;
using System.Collections.Generic;
using __Scripts.ExtraStats;
using __Scripts.GolemEntity.Behavior;
using GolemEntity.ExtraStats;
using UnityEngine;

public class ArmageddonCast : IFireBlast
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
