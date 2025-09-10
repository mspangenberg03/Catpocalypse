using System.Collections;
using System.Collections.Generic;

using UnityEngine;


[CreateAssetMenu(fileName = "NewTowerInfo", menuName = "New TowerInfo Asset")]
public class TowerInfo : ScriptableObject
{
    public TowerTypes TowerType;
    public string DisplayName;
    public string Classification;
    public Sprite Icon;
    public float Cost;
    public Ratings Damage;
    public Ratings Range;
    public TargetTypes AOE_Range;
    public Ratings FireRate;
    public Ratings CoolDown;
    public Ratings UpgradeCost;
    public string Upgrade;
    public List<string> Abilities;

    [Space(10)]
    [TextArea(4, 10)]
    public string Special = "None";

    [Space(10)]
    [TextArea(4, 10)]
    public string Description;

}
