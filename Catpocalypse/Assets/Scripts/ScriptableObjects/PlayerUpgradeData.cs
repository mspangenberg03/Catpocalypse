using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerUpgradeData : ScriptableObject
{
    [SerializeField,Tooltip("The player's starting health")]
    private int _maxHealth;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
    private int _scrap;
    public int Scrap
    {
        get { return _scrap; }
        set { _scrap = value; }
    }
    private CatTypes _catType;
    public CatTypes CatType
    {
        get { return _catType; }
        set { _catType = value; }
    }
}
