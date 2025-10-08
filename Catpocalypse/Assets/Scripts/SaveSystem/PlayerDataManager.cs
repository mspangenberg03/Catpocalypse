using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerDataManager: MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }

    private List<PlayerData> _PlayerData;
    private PlayerData _trackedData = new PlayerData(null);
    private PlayerUpgradeData _playerUpgradeData;
    private int maxSlots = 3;
    private int _CurrentData;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
        }
        // Tell Unity to not destroy this game object when a new scene is loaded.
        DontDestroyOnLoad(gameObject);
        _playerUpgradeData = (PlayerUpgradeData) Resources.Load("PlayerUpgrades");
        LoadData();
        _CurrentData = 0;
        if (_PlayerData.Count > 0)
        {
            _trackedData = _PlayerData[0];
            int currentSaveSlot = _CurrentData;
            foreach (PlayerData playerData in _PlayerData)
            {
                if (playerData.date > _trackedData.date)
                {
                    _trackedData.date = playerData.date;
                    _CurrentData = currentSaveSlot;
                }
                currentSaveSlot++;
            }
        }
        else
        {
            _trackedData = new PlayerData(_playerUpgradeData);
        }
    }

    private void Start()
    {
        
    }

    private void LoadData()
    {
        _PlayerData = new List<PlayerData>(maxSlots);
        for (int i = 0; i < maxSlots; i++)
        {
            if (!LoadGame(i))
            {
                _PlayerData.Add(new PlayerData(_playerUpgradeData));
            }
        }
    }

    public IReadOnlyList<PlayerData> ViewPlayerData()
    {
        return _PlayerData.ToList().AsReadOnly();
    }

    public void SaveGame(int i)
    {
        UpdateTimePlayed();
        UpdateDatePlayed();
        _PlayerData[i] = _trackedData;
        string saveFilePath = BuildSaveFilePath(i);
        string savePlayerData = JsonUtility.ToJson(_PlayerData[i]);
        File.WriteAllText(saveFilePath, savePlayerData);

        Debug.Log("Save file created at: " + saveFilePath);
    }

    public bool LoadGame(int i)
    {
        string saveFilePath = BuildSaveFilePath(i);
        if (File.Exists(saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(saveFilePath);
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(loadPlayerData);
            _PlayerData.Add(loadedData);
            _CurrentData = i;
            Debug.Log("Load game complete! \nLevels Completed: " + _PlayerData[i].levelsCompleted);
            return true;
        }
        else
        {
            Debug.Log("There are no save files to load!");
            return false;
        }
            
   

    }

    public bool DeleteSaveFile()
    {
        string saveFilePath = BuildSaveFilePath(_CurrentData);
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);

            Debug.Log("Save file deleted!");
            return true;
        }
        else
            Debug.Log("There is nothing to delete!");
        return false;
    }

    private string BuildSaveFilePath(int i)
    {
        return Application.persistentDataPath + "/PlayerData" + i + ".json";
    }

    #region Update Methods
    public void SetName(string name)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData.name = name;
    }

    public void UpdateScrap(int amount)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData.scrap += amount;
    }
    public void UpdateLevelsCompleted(int amount)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData.levelsCompleted += amount;
    }
    public void UpdateRobotUpgrades(int amount)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData.robotUpgrades += amount;
    }
    public void UpdateFortificationUpgrades(int amount)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData.fortificationUpgrades += amount;
    }
    public void UpdateLaserUpgrades(int amount)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData.laserUpgrades += amount;
    }
    public void UpdateScratchUpgrades(int amount)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData.scratchUpgrades += amount;
    }
    public void UpdateNAUpgrades(int amount)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData.nAUpgrades += amount;
    }
    public void UpdateYarnUpgrades(int amount)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData.yarnUpgrades += amount;
    }
    public void UpdateStringUpgrades(int amount)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData.stringUpgrades += amount;
    }
    public void UpdateCucumberUpgrades(int amount)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData.cucumberUpgrades += amount;
    }

    public void UpdateRewardUpgrade(int amount)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData.catRewardUpgrades += amount;
    }

    private void UpdateTimePlayed()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData.time = Time.realtimeSinceStartup;
    }

    private void UpdateDatePlayed()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData.date = DateTime.Now;
    }

    public void UpdateMasterVolume(float amount)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData._MasterVolume = amount;
    }

    public void UpdateMusicVolume(float amount)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData._MusicVolume = amount;
    }
    public void UpdateSFXVolume(float amount)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData._SFXVolume = amount;
    }

    public void UpdateResolutionSize(int amount)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData._ResolutionSize = amount;
    }

    public void UpdateWindowed(bool value)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData.windowed = value;
    }

    public void UpdateXInversion(bool value)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData._MouseXInvert = value;
    }

    public void UpdateYInversion(bool value)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData._MouseYInvert = value;
    }

    public void UpdateMouseSensitivity(float amount)
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        _trackedData._MouseSensitivity = amount;
    }
    #endregion

    #region Get Methods
    public string GetName()
    {
        if( _trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData.name;
    }

    public int GetScrap()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData.scrap;
    }
    public int GetLevelsCompleted()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData.levelsCompleted;
    }
    public int GetRobotUpgrades()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData.robotUpgrades;
    }
    public int GetFortificationUpgrades()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData.fortificationUpgrades;
    }
    public int GetLaserUpgrades()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData.laserUpgrades;
    }
    public int GetScratchUpgrades()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData.scratchUpgrades;
    }
    public int GetNAUpgrades()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData.nAUpgrades;
    }
    public int GetYarnUpgrades()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData.yarnUpgrades;
    }
    public int GetStringUpgrades()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData.stringUpgrades;
    }
    public int GetCucumberUpgrades()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData.cucumberUpgrades;
    }

    public int GetRewardUpgrade()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData.catRewardUpgrades;
    }

    public float GetTimePlayed()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData.time;
    }

    public DateTime GetDatePlayed()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData.date;
    }

    public float GetMasterVolume()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData._MasterVolume;
    }

    public float GetMusicVolume()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData._MusicVolume;
    }
    public float GetSFXVolume()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData._SFXVolume;
    }

    public int GetResolutionSize()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData._ResolutionSize;
    }

    public bool GetWindowed()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData.windowed;
    }

    public bool GetXInversion()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData._MouseXInvert;
    }

    public bool GetYInversion()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData._MouseYInvert;
    }

    public float GetMouseSensitivity()
    {
        if (_trackedData == null)
        {
            Debug.LogWarning("No current save data used, initialized filler data to prevent crash");
            _trackedData = new PlayerData(_playerUpgradeData);
        }
        return _trackedData._MouseSensitivity;
    }
    #endregion
    //private PlayerData CurrentData { get { return _trackedData; } }
    public PlayerUpgradeData Upgrades { get { return _playerUpgradeData; } }

}



public class PlayerData
{
    public PlayerData(PlayerUpgradeData data)
    {
        name = "";
        scrap = 300;
        levelsCompleted = 0;
        robotUpgrades = 0;
        fortificationUpgrades = 0;
        laserUpgrades = 0;
        scratchUpgrades = 0;
        nAUpgrades = 0;
        yarnUpgrades = 0;
        stringUpgrades = 0;
        cucumberUpgrades = 0;
        catRewardUpgrades = 0;
        time = 0;
        date = DateTime.Now;
        _MasterVolume = 1f;
        _MusicVolume = 1f;
        _SFXVolume = 1f;
        _ResolutionSize = 0;
        _MouseXInvert = false;
        _MouseYInvert = false;
        _MouseSensitivity = 0.5f;
}

    public string name;
    public int scrap;
    public int levelsCompleted;
    public int robotUpgrades;
    public int fortificationUpgrades;
    public int laserUpgrades;
    public int scratchUpgrades;
    public int nAUpgrades;
    public int yarnUpgrades;
    public int stringUpgrades;
    public int cucumberUpgrades;
    public int catRewardUpgrades;
    public float time;
    public DateTime date;

    //Settings
    public float _MasterVolume;
    public float _MusicVolume;
    public float _SFXVolume;
    public int _ResolutionSize;
    public bool windowed;
    public bool _MouseXInvert;
    public bool _MouseYInvert;
    public float _MouseSensitivity;
}