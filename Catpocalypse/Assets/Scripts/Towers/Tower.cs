using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Runtime.CompilerServices;
using System;

public class Tower : MonoBehaviour
{

    [SerializeField]
    protected float buildCost;
    [SerializeField]
    protected float refundAmount;
    [SerializeField]
    protected SphereCollider range;
    [SerializeField]
    protected float distractValue;
    [SerializeField]
    protected int numberOfTargets;

    protected Vector3 targetDirection;
    public List<GameObject> targets;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("Cat"))
        {
            targets.Add(collider.gameObject);
        }
    }

    void OnMouseEnter()
    {
        this.gameObject.GetComponentInParent<TowerBase>().hoveredOver = true;
        this.gameObject.GetComponent<Renderer>().material = this.gameObject.GetComponentInParent<TowerBase>().towerHovered;
    }

    void OnMouseExit()
    {
        this.gameObject.GetComponentInParent<TowerBase>().hoveredOver = false;
        this.gameObject.GetComponent<Renderer>().material = this.gameObject.GetComponentInParent<TowerBase>().towerNotHovered;
    }

    void OnMouseUpAsButton()
    {

        if (enabled)
        {
            

        }
    }


    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Cat"))
        {
            targets.Remove(collider.gameObject);
            if(targets.Count > 0) {
                targets[0] = targets.First();
            }
            
           
        }
    }

    private void OnCatDied(object sender, EventArgs e)
    {
        targets.Remove(sender as GameObject);
    }

    public void OnDestroy()
    {
        Destroy(this);
    }
    public float GetDistractionValue()
    {
        return distractValue;
    }

    public float GetBuildCost()
    {
        return buildCost;
    }

    public float GetRefundValue()
    {
        return refundAmount;
    }

}
