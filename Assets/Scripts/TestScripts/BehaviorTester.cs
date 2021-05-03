using System.Collections;
using System.Collections.Generic;
using __Scripts.GolemEntity.Behavior;
using UnityEngine;

public class BehaviorTester : GolemBehavior
{
    protected override void InitBehaviors()
    {
        _movable = new WalkBehavior(transform, new Vector3[5]);
        _hitable = new CommonHitBehavior();
        _defencable = new BlockBehavior();
        _castable = new ArmageddonCast();
    }
    
}
