
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RobotUpgradeCard : MonoBehaviour, IUpgradeCard
{


    public bool PurchaseUpgrade()
    {
        return false;
    }

    public bool UpdateLabels(int level)
    {
        return false;
    }
}