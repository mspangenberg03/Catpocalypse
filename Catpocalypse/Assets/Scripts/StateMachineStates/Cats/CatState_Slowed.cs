using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// This is the Moving State providing functionality for Cats that are moving
/// </summary>
public class CatState_Slowed : CatState_Base
{
    public CatState_Slowed(CatBase parent)
        : base(parent)
    {

    }

    public override void OnEnter()
    {
        UpdateSlowedModifier();
    }

    public override void OnExit()
    {
        _parent.gameObject.GetComponent<NavMeshAgent>().speed = _parent.GetComponent<CatBase>().speed;
    }

    public override void OnUpdate()
    {
        UpdateSlowedModifier();
    }

    private void UpdateSlowedModifier()
    {
        CatBase cat = _parent.GetComponent<CatBase>();
        float modifier = 0;
        foreach (GameObject obj in cat.slowingEntities)
        {
            if (obj != null)
            {
                ScratchingPost post = obj.GetComponent<ScratchingPost>();
                if (post.speedDebuff > modifier)
                {
                    modifier = post.speedDebuff;
                }
            }
        }
        _parent.GetComponent<NavMeshAgent>().speed = cat.speed / modifier;
    }

}