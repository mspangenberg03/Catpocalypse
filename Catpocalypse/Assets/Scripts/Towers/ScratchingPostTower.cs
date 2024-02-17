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

    [Header("Launcher Settings")]

    [Tooltip("The ScratchingPost prefab to launch")]
    [SerializeField]
    private GameObject _ScratchPost;

    [Tooltip("The time to wait between post destruction and launch")]
    [Min(0f)]
    [SerializeField]
    private float _TimeBetweenLaunches;

    private bool _IsLaunching = false;

    private int _MaxPosts = 1;

    public int postCount = 0;

    public void Update()
    {
       
        if(targets.Count <= 0)
        {
            return;
        } else if(_IsLaunching)
        {
            return;
        }
        if (postCount < _MaxPosts)
        {
            postCount++;
            StartCoroutine(LaunchPost());
        }
    }

    private Vector3 FindClosestPostDestination()
    {
        return targets[0].transform.position;
    }

    

    private IEnumerator  LaunchPost()
    {
        _IsLaunching = true;
        //TODO: Make the launch animation
        GameObject post = Instantiate(_ScratchPost, FindClosestPostDestination(), Quaternion.identity);
        post.GetComponent<ScratchingPost>().parentTower = gameObject;
        yield return new WaitForSeconds(_TimeBetweenLaunches);
        _IsLaunching = false;
    }



}
