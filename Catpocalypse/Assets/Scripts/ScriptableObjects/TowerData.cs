using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class TowerData : ScriptableObject
{
    [SerializeField]
    protected float distractValue;
    public float DistractValue { set { distractValue = value; } get { return distractValue; } }
    [SerializeField]
    protected float fireRate;
    public float FireRate
    {
        set
        {
            fireRate = value;
        }
        get
        {
            return fireRate;
        }
    }
    [SerializeField]
    protected float buildCost;
    public float BuildCost 
    { 
        get 
        { 
            return buildCost; 
        }
        set
        {
            buildCost = value;
        }
    }
    [SerializeField]
    protected float upgradeCost;
    public float UpgradeCost
    {
        get
        {
            return upgradeCost;
        }
        set
        {
            upgradeCost = value;
        }
    }
    [SerializeField]
    protected float range;
    public float Range
    {
        set
        {
            range = value;
        }
        get
        {
            return range;
        }
    }
    private bool _tierFiveReached = false;
    public bool TierFiveReached
    {
        set { _tierFiveReached = value; }
        get { return _tierFiveReached; }
    }
    private float _nonAllergicPersonMoveSpeed = 5;
    public float NonAllergicPersonMoveSpeed
    {
        get { return _nonAllergicPersonMoveSpeed; }
        set { _nonAllergicPersonMoveSpeed = value; } 
    }
    public float _cucumberTowerAimingSpeed = 30f;

}
