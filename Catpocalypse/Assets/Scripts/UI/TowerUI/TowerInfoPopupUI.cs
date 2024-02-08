using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TowerInfoPopupUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _TitleText;
    [SerializeField] private TextMeshProUGUI _DescriptionText;
    [SerializeField] private TextMeshProUGUI _CostText;
    [SerializeField] private TextMeshProUGUI _DamageText;
    [SerializeField] private TextMeshProUGUI _RangeText;
    [SerializeField] private TextMeshProUGUI _AOE_Text;
    [SerializeField] private TextMeshProUGUI _FireRateText;
    [SerializeField] private TextMeshProUGUI _SpecialText;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdatePopupUI(TowerInfo towerInfo)
    {
        //_Icon.sprite = towerInfo.Icon;
        _TitleText.text = InsertSpacesBeforeCapitalLetters(towerInfo.DisplayName);
        _DescriptionText.text = towerInfo.Description;
        _CostText.text = $"${towerInfo.Cost}";
        _DamageText.text = InsertSpacesBeforeCapitalLetters(Enum.GetName(typeof(TowerInfo.Ratings), towerInfo.Damage));
        _RangeText.text = InsertSpacesBeforeCapitalLetters(Enum.GetName(typeof(TowerInfo.Sizes), towerInfo.Range));
        _AOE_Text.text = InsertSpacesBeforeCapitalLetters(Enum.GetName(typeof(TowerInfo.Sizes), towerInfo.AOE_Range));
        _FireRateText.text = InsertSpacesBeforeCapitalLetters(Enum.GetName(typeof(TowerInfo.Ratings), towerInfo.FireRate));
        _SpecialText.text = towerInfo.Special;
    }

    private string InsertSpacesBeforeCapitalLetters(string input)
    {
        string output = "";

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            if (i > 0 && char.IsUpper(c))
                output += " ";

            output += c;
        }


        return output;
    }
}
