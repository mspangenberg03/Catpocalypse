using System.Collections;
using System.Collections.Generic;

using UnityEngine;


[CreateAssetMenu(fileName = "NewTowerInfo", menuName = "New TowerInfo Asset")]
public class TowerInfo : ScriptableObject
{
    public enum TowerTypes
    {
        LaserPointer,
        ScratchingPost,
        CucumberThrower,
        StringWaver,
        YarnBall,
        NonAllergic,
    }

    public enum Ratings
    {
        VeryLow,
        Low,
        Medium,
        High,
        VeryHigh,
    }

    public enum Sizes
    {
        None,
        VerySmall,
        Small,
        Medium,
        Long,
        VeryLong,
    }



    public TowerTypes TowerType;
    public string Description;
    public float Cost;
    public Ratings Damage;
    public Sizes Range;
    public Sizes AOE_Range;
    public Ratings FireRate;
    public string Special = "None";
}
