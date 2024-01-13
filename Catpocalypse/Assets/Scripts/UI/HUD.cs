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


    [Header("Health Bar Refs")]
    [SerializeField] private Image _PlayerHealthBar;
    [SerializeField] private TextMeshProUGUI _PlayerHealthBarLabel;

    [Header("Cuteness Bar Refs")]
    [SerializeField] private Image _CutenessBar;
    [SerializeField] private TextMeshProUGUI _CutenessBarLabel;

    [Header("Wave Info Display Refs")]
    [SerializeField] private Button _StartWaveButton;
    [SerializeField] private TextMeshProUGUI _WaveNumberLabel;
    [SerializeField] private TextMeshProUGUI _CatsRemainingLabel;

    [Header("Player Money Display Refs")]
    [SerializeField] private TextMeshProUGUI _PlayerMoneyLabel;

    [Header("Victory Screen Panel")]
    [SerializeField] private GameObject _VictoryScreen;
    


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

    public static void UpdateCutenessDisplay(float currentCuteness, float maxCuteness)
    {
        _Instance.CutenessBar.fillAmount = Mathf.Clamp01(currentCuteness / maxCuteness);
        _Instance.CutenessBarLabel.text = $"{currentCuteness} / {maxCuteness} Cuteness";
    }

    public static void UpdateWaveInfoDisplay(int waveNumber, int catsRemaining)
    {
        _Instance.WaveNumberLabel.text = $"Wave {waveNumber}";
        _Instance.CatsRemainingLabel.text = $"Remaining Cats: {catsRemaining}";
    }

    public static void UpdatePlayerMoneyDisplay(float playerMoney)
    {
        _Instance.PlayerMoneyLabel.text = $"${playerMoney:N2}";
    }

    public static void HideWaveDisplay()
    {
        if (_Instance == null)
            return;


        _Instance.WaveNumberLabel.gameObject.SetActive(false);
        _Instance.CatsRemainingLabel.gameObject.SetActive(false);
        
        // The button state should always be the opposite of the labels' state.
        _Instance.StartWaveButton.gameObject.SetActive(true);
    }

    public static void ShowWaveDisplay()
    {
        if (_Instance == null)
            return;


        // Clear the text labels, so the player won't potentially see an old value before it updates.
        _Instance.WaveNumberLabel.text = "";
        _Instance.CatsRemainingLabel.text = "";

        _Instance.WaveNumberLabel.gameObject.SetActive(true);
        _Instance.CatsRemainingLabel.gameObject.SetActive(true);

        // The button state should always be the opposite of the labels' state.
        _Instance._StartWaveButton.gameObject.SetActive(false);
    }

    public static void RevealVictory()
    {
        _Instance._StartWaveButton.gameObject.SetActive(false);
        _Instance.WaveNumberLabel.gameObject.SetActive(false);
        _Instance.CatsRemainingLabel.gameObject.SetActive(false);

        _Instance._VictoryScreen.SetActive(true);

    }


    public Image PlayerHealthBar { get { return _PlayerHealthBar; } }
    public TextMeshProUGUI PlayerHealthBarLabel { get { return _PlayerHealthBarLabel; } }

    public Image CutenessBar { get { return _CutenessBar; } }
    public TextMeshProUGUI CutenessBarLabel { get { return _CutenessBarLabel; } }


    public Button StartWaveButton { get { return _StartWaveButton; } }
    public TextMeshProUGUI WaveNumberLabel { get { return _WaveNumberLabel; } }
    public TextMeshProUGUI CatsRemainingLabel { get { return _CatsRemainingLabel; } }

    public TextMeshProUGUI PlayerMoneyLabel { get { return _PlayerMoneyLabel; } }

}
