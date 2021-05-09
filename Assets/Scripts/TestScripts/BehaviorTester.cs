using System.Collections;
using System.Collections.Generic;
using __Scripts.GolemEntity.Behavior;
using UnityEngine;

public class BehaviorTester : GolemBehavior
{
    protected override void InitBehaviors()
    {
        _movable = new WalkBehavior(transform, new Vector3[5]);
        _hittable = new CommonHitBehavior();
        _defendable = new BlockBehavior();
        _castable = new ArmageddonCast();
    }
    
}
