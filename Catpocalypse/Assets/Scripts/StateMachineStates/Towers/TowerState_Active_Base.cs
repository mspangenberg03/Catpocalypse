using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class TowerState_Active_Base : TowerState_Base
{
    public TowerState_Active_Base(Tower parent)
        : base(parent)
    {
        
    }


    public override void OnEnter()
    {
        _parentTower.Animator.SetBool("Active", true);
    }

    public override void OnExit()
    {
        _parentTower.Animator.SetBool("Active", false);
    }

    public override void OnUpdate()
    {
        
    }
}
