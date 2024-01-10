using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCutenessManager : MonoBehaviour
{
    [SerializeField] private float maxCuteness = 100f;

    private float cuteness = 0f;

    private void Start()
    {
        
        HUD.UpdateCutenessDisplay(cuteness, maxCuteness);
    }
    private void Update()
    {

    }

    public void AddCuteness(int amount)
    {
        cuteness = Mathf.Clamp(cuteness + amount, 0, maxCuteness);
        HUD.UpdateCutenessDisplay(cuteness, maxCuteness);
    }
    
}
