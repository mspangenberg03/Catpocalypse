using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class TowerState_Upgrading_Base : TowerState_Base
{
    public TowerState_Upgrading_Base(Tower parent)
        : base(parent)
    {

    }

    public override void OnEnter()
    {
        _parentTower.DisableTargetDetection();
        _parentTower.Animator.SetBool("Upgrade", true);
    }

    public override void OnExit()
    {
        _parentTower.EnableTargetDetection();
        _parentTower.Animator.SetBool("Upgrade", true);
    }

    public override void OnUpdate()
    {

    }
}
