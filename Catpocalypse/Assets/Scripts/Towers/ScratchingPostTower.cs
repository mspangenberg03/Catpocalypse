using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ScratchingPostTower : Tower
{

    [Header("Targeting Settings")]

    [Tooltip("The rate new scratching posts are launched in seconds")]
    [SerializeField]
    [Min(0f)]
    private float _RateOfFire;

    [Tooltip("The object the player sees when placing their target location")]
    [SerializeField]
    private GameObject _PlayerTarget;

    [Header("Launcher Settings")]

    [Tooltip("The ScratchingPost prefab to launch")]
    [SerializeField]
    private GameObject _ScratchPost;

    [Tooltip("The time to wait between post destruction and launch")]
    [Min(0f)]
    [SerializeField]
    private float _TimeBetweenLaunches;

    public bool PostExists = false;

    public void Update()
    {
        if(targets.Count <= 0)
        {
            return;
        }
        if (!PostExists)
        {
            PostExists = true;
            StartCoroutine(LaunchPost());
        }
    }

    

    private IEnumerator  LaunchPost()
    {
        yield return new WaitForSeconds(_TimeBetweenLaunches);
        Instantiate(_ScratchPost, _PlayerTarget.transform.position, Quaternion.identity);
    }



}
