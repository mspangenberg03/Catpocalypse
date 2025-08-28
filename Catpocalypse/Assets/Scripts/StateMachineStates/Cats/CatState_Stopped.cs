using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// This is the Moving State providing functionality for Cats that are moving
/// </summary>
public class CatState_Stopped : CatState_Base
{
    public CatState_Stopped(CatBase parent)
        : base(parent)
    {

    }

    public override void OnEnter()
    {
        _parent.gameObject.GetComponent<NavMeshAgent>().speed = 0;
        _parentCat.Animator.SetBool("Stopped", true);

    }

    public override void OnExit()
    {
        _parent.gameObject.GetComponent<NavMeshAgent>().speed = _parentCat.speed;
        _parentCat.Animator.SetBool("Stopped", false);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

}