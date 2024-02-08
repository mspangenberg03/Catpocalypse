using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class TowerInfoPanel : MonoBehaviour
{
    [Tooltip("This is a reference to the parent of this panel. It is only used so that when you close this panel, it can reopen the main level select panel.")]
    [SerializeField] GameObject _ParentPanel;

    [SerializeField] TMP_Dropdown _Dropdown_TowerSelection;


    [Tooltip("This list controls which towers are displayed in this window. The tower info displayed by this panel is pulled from the scriptable objects in this list.")]
    [SerializeField] List<TowerInfo> _TowerInfoList;



    private void Awake()
    {
        _Dropdown_TowerSelection.options.Clear();

        // Spawn a button for each tower type in the list.
        for (int i = 0; i < _TowerInfoList.Count; i++) 
        {
            TowerInfo info = _TowerInfoList[i];
            _Dropdown_TowerSelection.options.Add(new TMP_Dropdown.OptionData(info.DisplayName));            

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelectedTowerChanged()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        Debug.Log($"Selected: {_Dropdown_TowerSelection.captionText.text}");
        //_Dropdown_TowerSelection.value;
    }

    public void ButtonClicked_Close()
    {
        _ParentPanel?.gameObject.SetActive(true);

        gameObject.SetActive(false);
    }

    public void ResetUI()
    {
        Debug.Log("RESET UI!");
    }
}
