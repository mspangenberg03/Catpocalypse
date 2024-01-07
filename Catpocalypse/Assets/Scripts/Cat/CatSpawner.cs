using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("One possible spawn point for cats")]
    private Transform spawnPoint1;

    [SerializeField] private GameObject normalCat;
    [SerializeField] private Button startWaveButton;
    [SerializeField] private PlayerHealthManager healthManager;

    private int waveCount = 0;
    private int catsInWave = 5;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] cats = GameObject.FindGameObjectsWithTag("Cat");
        if (cats.Length == 0 && healthManager.GetPlayerOutOfHealth() != true)
        {
            startWaveButton.enabled = true;
        }                
    }
    public void StartNextWave()
    {
        waveCount++;
        if(waveCount == 1)
        {
            StartCoroutine(Spawner());
        }
        else
        {
            catsInWave += 2;
            StartCoroutine(Spawner());
            HUD.UpdateWaveNumberDisplay(waveCount);
        }
        startWaveButton.enabled = false;
    }
    public void StopSpawner()
    {
        StopCoroutine(Spawner());
    }
    IEnumerator Spawner()
    {
        
        for (int i = 0; i < catsInWave; i++)
        {
            
            Instantiate(normalCat, spawnPoint1);
            yield return new WaitForSeconds(2f);
        }
        

    }
}
