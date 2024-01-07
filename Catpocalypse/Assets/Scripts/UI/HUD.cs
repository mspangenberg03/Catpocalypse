using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// This class manages the HUD display. Call the static methods to update the HUD GUI elements.
/// That way you can access the HUD from anywhere in our codebase.
/// 
/// This class is a singleton since we should only ever have one instance of it.
/// </summary>
public class HUD : MonoBehaviour
{
    public static HUD _Instance;


    [SerializeField] private Image _PlayerHealthBar;
    [SerializeField] private TextMeshProUGUI _PlayerHealthBarLabel;
    [SerializeField] private TextMeshProUGUI _WaveNumberLabel;
    [SerializeField] private TextMeshProUGUI _PlayerMoneyLabel;
    


    private void Awake()
    {
        // If an instance has already been created, then log error message and destroy self.
        if (_Instance != null)
        {
            Debug.LogError("An instance of HUD already exists! So this one destroyed itself.");
            Destroy(gameObject);
        }


        _Instance = this;
    }

    private void Start()
    {
        /* This is just test code.
        UpdatePlayerHealthDisplay(64, 100);
        UpdateWaveNumberDisplay(16);
        UpdatePlayerMoneyDisplay(542);
        */
    }



    public static void UpdatePlayerHealthDisplay(float currentHP, float maxHP)
    {
        _Instance.PlayerHealthBar.fillAmount = Mathf.Clamp01(currentHP / maxHP);
        _Instance.PlayerHealthBarLabel.text = $"{currentHP} of {maxHP} HP";
    }

    public static void UpdateWaveNumberDisplay(int waveNumber)
    {
        _Instance.WaveNumberLabel.text = $"Wave {waveNumber}";
    }

    public static void UpdatePlayerMoneyDisplay(float playerMoney)
    {
        _Instance.PlayerMoneyLabel.text = $"${playerMoney:N2}";
    }



    public Image PlayerHealthBar { get { return _PlayerHealthBar; } }
    public TextMeshProUGUI PlayerHealthBarLabel { get { return _PlayerHealthBarLabel; } }
    public TextMeshProUGUI WaveNumberLabel { get { return _WaveNumberLabel; } }
    public TextMeshProUGUI PlayerMoneyLabel { get { return _PlayerMoneyLabel; } }

}
