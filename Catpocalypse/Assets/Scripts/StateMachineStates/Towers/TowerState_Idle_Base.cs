using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class TowerState_Idle_Base : TowerState_Base
{
    public TowerState_Idle_Base(Tower parent)
        : base(parent)
    {

    }


    public override void OnEnter()
    {
        _parentTower.Animator.SetBool("Idle", true);
    }

    public override void OnExit()
    {
        _parentTower.Animator.SetBool("Idle", false);
    }

    public override void OnUpdate()
    {

    }
}
