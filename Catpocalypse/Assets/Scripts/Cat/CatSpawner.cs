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


    private int waveCount = 0;
    private int catsInWave = 5;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Spawner());
        Debug.Log("Button: " + startWaveButton.enabled);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] cats = GameObject.FindGameObjectsWithTag("Cat");
        if (cats.Length == 0)
        {
            startWaveButton.enabled = true;
        }                
    }
    public void StartNextWave()
    {
        Debug.Log("$^#$%&#$%&%$");
        waveCount++;
        if(waveCount == 1)
        {
            StartCoroutine(Spawner());
        }
        else
        {
            catsInWave += 2;
            StartCoroutine(Spawner());
        }
        startWaveButton.enabled = false;
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
