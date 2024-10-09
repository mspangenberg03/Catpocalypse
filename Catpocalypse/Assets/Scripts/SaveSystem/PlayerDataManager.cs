using System.IO;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }

    private PlayerData[] _PlayerData;
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
        LoadData();

        // Tell Unity to not destroy this game object when a new scene is loaded.
        DontDestroyOnLoad(gameObject);
        _CurrentData = 0;
    }

    private void LoadData()
    {
        _PlayerData = new PlayerData[maxSlots];
        for (int i = _PlayerData.Length - 1; i >= 0; i--)
        {
            if (!LoadGame(i))
            {
                _PlayerData[i] = new PlayerData();
                _CurrentData = i;
            }
        }
    }

    public void SaveGame(int i)
    {
        string saveFilePath = BuildSaveFilePath(_CurrentData);
        string savePlayerData = JsonUtility.ToJson(_PlayerData[i - 1]);
        File.WriteAllText(saveFilePath, savePlayerData);

        Debug.Log("Save file created at: " + saveFilePath);
    }

    public bool LoadGame(int i)
    {
        string saveFilePath = BuildSaveFilePath(_CurrentData);
        if (File.Exists(saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(saveFilePath);
            _PlayerData[i] = JsonUtility.FromJson<PlayerData>(loadPlayerData);
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

    public void SetName(string name)
    {
        _PlayerData[_CurrentData].name = name;
    }

    public void UpdateScrap(int amount)
    {
        _PlayerData[_CurrentData].scrap += amount;
    }
    public void UpdateLevelsCompleted(int amount)
    {
        _PlayerData[_CurrentData].levelsCompleted += amount;
    }
    public void UpdateRobotUpgrades(int amount)
    {
        _PlayerData[_CurrentData].robotUpgrades += amount;
    }
    public void UpateFortificationUpgrades(int amount)
    {
        _PlayerData[_CurrentData].fortificationUpgrades += amount;
    }
    public void UpdateLaserUpgrades(int amount)
    {
        _PlayerData[_CurrentData].laserUpgrades += amount;
    }
    public void UpdateScratchUpgrades(int amount)
    {
        _PlayerData[_CurrentData].scratchUpgrades += amount;
    }
    public void UpdateNAUpgrades(int amount)
    {
        _PlayerData[_CurrentData].nAUpgrades += amount;
    }
    public void UpdateYarnUpgrades(int amount)
    {
        _PlayerData[_CurrentData].yarnUpgrades += amount;
    }
    public void UpdateStringUpgrades(int amount)
    {
        _PlayerData[_CurrentData].stringUpgrades += amount;
    }
    public void UpdateCucumberUpgrades(int amount)
    {
        _PlayerData[_CurrentData].cucumberUpgrades += amount;
    }

    public PlayerData CurrentData { get { return _PlayerData[_CurrentData]; } }

}

public class PlayerData
{
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

}