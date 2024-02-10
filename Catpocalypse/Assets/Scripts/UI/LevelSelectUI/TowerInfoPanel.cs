using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;


public class TowerInfoPanel : MonoBehaviour
{
    [Header("General")]

    [Tooltip("This is a reference to the parent of this panel. It is only used so that when you close this panel, it can reopen the main level select panel.")]
    [SerializeField] GameObject _ParentPanel;


    [Header("Info Pane Settings")]

    [SerializeField] TextMeshProUGUI _Text_DisplayName;
    [SerializeField] TextMeshProUGUI _Text_Cost;
    [SerializeField] TextMeshProUGUI _Text_Damage;
    [SerializeField] TextMeshProUGUI _Text_Range;
    [SerializeField] TextMeshProUGUI _Text_AOE_Range;
    [SerializeField] TextMeshProUGUI _Text_FireRate;
    [SerializeField] TextMeshProUGUI _Text_Cooldown;
    [SerializeField] TextMeshProUGUI _Text_UpgradeCost;
    [SerializeField] TextMeshProUGUI _Text_Upgrade;
    [SerializeField] TextMeshProUGUI _Text_Special;
    [SerializeField] TextMeshProUGUI _Text_Description;


    [Header("Tower Selection Pane Settings")]
    
    [SerializeField] TMP_Dropdown _Dropdown_TowerSelection;

    [Space(10)]

    [Tooltip("This list controls which towers are displayed in this window. The tower info displayed by this panel is pulled from the scriptable objects in this list.")]
    [SerializeField] List<TowerInfo> _TowerInfoList;



    private TowerInfoCollection _TowerInfoCollection;



    private void Awake()
    {
        _TowerInfoCollection = GetComponent<TowerInfoCollection>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PopulateCollectionDropdown();
        ResetUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PopulateCollectionDropdown()
    {
        _Dropdown_TowerSelection.options.Clear();


        // Spawn a button for each tower type in the list.
        for (int i = 0; i < _TowerInfoCollection.Count; i++)
        {
            TowerInfo info = _TowerInfoCollection.GetTowerInfo(i);
            _Dropdown_TowerSelection.options.Add(new TMP_Dropdown.OptionData(info.DisplayName));
        }
    }

    public void OnSelectedTowerChanged()
    {
        UpdateUI(_TowerInfoCollection.GetTowerInfo(_Dropdown_TowerSelection.value));
    }

    public void ButtonClicked_Close()
    {
        _ParentPanel?.gameObject.SetActive(true);

        gameObject.SetActive(false);
    }

    /// <summary>
    /// This function updates the UI whenever the selected tower is changed.
    /// </summary>
    /// <param name="info">The TowerInfo object for the newly selected tower. It contains all the info about that tower.</param>
    private void UpdateUI(TowerInfo info)
    {
        _Text_DisplayName.text = info.DisplayName;
        _Text_Cost.text = $"${info.Cost}";
        _Text_Damage.text = Utils.InsertSpacesBeforeCapitalLetters(info.Damage.ToString());
        _Text_Range.text = Utils.InsertSpacesBeforeCapitalLetters(info.Range.ToString());
        _Text_AOE_Range.text = Utils.InsertSpacesBeforeCapitalLetters(info.AOE_Range.ToString());
        _Text_FireRate.text = Utils.InsertSpacesBeforeCapitalLetters(info.FireRate.ToString());
        _Text_Cooldown.text = Utils.InsertSpacesBeforeCapitalLetters(info.CoolDown.ToString());
        _Text_UpgradeCost.text = Utils.InsertSpacesBeforeCapitalLetters(info.UpgradeCost.ToString());
        _Text_Upgrade.text = info.Upgrade;
        _Text_Special.text = info.Special.ToString();
        _Text_Description.text = info.Description.ToString();
    }

    public void ResetUI()
    {
        _Dropdown_TowerSelection.value = 0;
        _Dropdown_TowerSelection.RefreshShownValue();

        UpdateUI(_TowerInfoCollection.GetTowerInfo(0));
    }
}
