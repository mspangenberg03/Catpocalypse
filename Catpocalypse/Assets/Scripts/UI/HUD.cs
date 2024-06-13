using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;


/// <summary>
/// This class manages the HUD display. Call the static methods to update the HUD GUI elements.
/// That way you can access the HUD from anywhere in our codebase.
/// 
/// This class is a singleton since we should only ever have one instance of it.
/// </summary>
public class HUD : MonoBehaviour
{
    public static HUD Instance;


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

    [Header("Level End Panels")]
    [SerializeField] private GameObject _DefeatScreen;
    [SerializeField] private GameObject _VictoryScreen;
    


    private void Awake()
    {
        // If an instance has already been created, then log error message and destroy self.
        if (Instance != null)
        {
            Debug.LogError("An instance of HUD already exists! So this one destroyed itself.");
            Destroy(gameObject);
        }


        Instance = this;
    }

    private void Start()
    {
        /* This is just test code.
        UpdatePlayerHealthDisplay(64, 100);
        UpdateWaveNumberDisplay(16);
        UpdatePlayerMoneyDisplay(542);
        */
    }

    private void Update()
    {
        // This test function opens the victory or defeat screen when you press the space bar.
        //TestRandomWinLoseText(false);
    }

    /// <summary>
    /// This function allows you to quickly test the victory or defeat screens by pressing the spacebar.
    /// It should be called from the Update() method.
    /// Pressing the space bar a second time will close the screen it opened.
    /// If the parameter is true, the victory screen is opened. Otherwise the defeat screen is opened.
    /// </summary>
    /// <param name="testVictoryScreen"></param>
    private void TestRandomWinLoseText(bool testVictoryScreen = true)
    {
        GameObject screen = testVictoryScreen ? _VictoryScreen : _DefeatScreen;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (screen.activeSelf)
            {
                screen.SetActive(false);
            }
            else
            {
                if (testVictoryScreen)
                    HUD.RevealVictory();
                else
                    HUD.RevealDefeat();
            }
        }
    }

    public static void UpdatePlayerHealthDisplay(float currentHP, float maxHP)
    {
        Instance.PlayerHealthBar.fillAmount = Mathf.Clamp01(currentHP / maxHP);
        Instance.PlayerHealthBarLabel.text = $"";
    }

    public static void UpdateCutenessDisplay(float currentCuteness, float maxCuteness)
    {
        Instance.CutenessBar.fillAmount = Mathf.Clamp01(currentCuteness / maxCuteness);
        Instance.CutenessBarLabel.text = $"";
    }

    public static void UpdateWaveInfoDisplay(int waveNumber, int catsRemaining)
    {
        Instance.WaveNumberLabel.text = $"{waveNumber}";
        Instance.CatsRemainingLabel.text = $"{catsRemaining}";
    }

    public static void UpdatePlayerMoneyDisplay(float playerMoney)
    {
        Instance.PlayerMoneyLabel.text = $"${playerMoney:N2}";
    }

    public static void HideWaveDisplay()
    {
        if (Instance == null)
            return;


        Instance.WaveNumberLabel.gameObject.SetActive(false);
        Instance.CatsRemainingLabel.gameObject.SetActive(false);
        
        // The button state should always be the opposite of the labels' state.
        Instance.StartWaveButton.gameObject.SetActive(true);
    }

    public static void ShowWaveDisplay()
    {
        if (Instance == null)
            return;


        // Clear the text labels, so the player won't potentially see an old value before it updates.
        Instance.WaveNumberLabel.text = "";
        Instance.CatsRemainingLabel.text = "";

        Instance.WaveNumberLabel.gameObject.SetActive(true);
        Instance.CatsRemainingLabel.gameObject.SetActive(true);

        // The button state should always be the opposite of the labels' state.
        Instance._StartWaveButton.gameObject.SetActive(false);
    }

    public static void RevealVictory()
    {
        Instance._StartWaveButton.gameObject.SetActive(false);
        Instance.WaveNumberLabel.gameObject.SetActive(false);
        Instance.CatsRemainingLabel.gameObject.SetActive(false);

        Instance._VictoryScreen.SetActive(true);
    }

    public static void RevealDefeat()
    {
        Instance._StartWaveButton.gameObject.SetActive(false);
        Instance.WaveNumberLabel.gameObject.SetActive(false);
        Instance.CatsRemainingLabel.gameObject.SetActive(false);

        Instance._DefeatScreen.SetActive(true);
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
