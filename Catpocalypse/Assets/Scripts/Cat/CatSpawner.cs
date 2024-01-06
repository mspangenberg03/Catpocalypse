using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("One possible spawn point for cats")]
    private Transform spawnPoint1;
    private int waveCount = 0;
    private int catsInWave = 5;
    [SerializeField] private GameObject normalCat;
    private Image button;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Spawner());
        button = GetComponent<Image>();
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] cats = GameObject.FindGameObjectsWithTag("Cat");
        if (cats.Length == 0)
        {
            button.enabled = true;
            text.enabled = true;
        }
        
        
    }
    public void NextWave()
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
        }
        button.enabled = false;
        text.enabled = false;
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
