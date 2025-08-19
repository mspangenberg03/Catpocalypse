using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class TowerState_Disabled_Base : TowerState_Base
{
    public TowerState_Disabled_Base(Tower parent)
        : base(parent)
    {
        
    }


    public override void OnEnter()
    {
        _parentTower.Animator.SetBool("Disabled", true);
    }

    public override void OnExit()
    {
        _parentTower.Animator.SetBool("Disabled", false);
    }

    public override void OnUpdate()
    {

    }
}
