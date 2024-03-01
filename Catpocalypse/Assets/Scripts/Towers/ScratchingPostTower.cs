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

    [Tooltip("The IrresistibleScratchingPost prefab to launch")]
    [SerializeField]
    private GameObject _IrScratchPost;

    [Tooltip("The time to wait between post destruction and launch")]
    [Min(0f)]
    [SerializeField]
    private float _TimeBetweenLaunches;

    private bool _IsLaunching = false;

    private int _MaxPosts = 1;

    public int postCount = 0;
    private float IrCooldown = 8;
    private bool ISPReady = false;
    [SerializeField, Tooltip("How much the upgrade increases the scratching post tower AOE")]
    private float AOEUpgrade = 5;
    [SerializeField, Tooltip("How much the upgrade increases the scratching post tower speed debuff")]
    private float debuffUpgrade = 5;

    private float speedDebuff;
    private float AOE;
    private void Start()
    {
        speedDebuff = _ScratchPost.GetComponent<ScratchingPost>().speedDebuff;
        AOE = _ScratchPost.GetComponent<SphereCollider>().radius;
    }
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
    public override void Upgrade()
    {
        base.Upgrade();
        switch (towerLevel)
        {
            case 2:
                AOE += AOEUpgrade;
                break;
            case 3:
                speedDebuff += debuffUpgrade;
                break;
            case 4:
                ISPReady = true;
                break;
        }
        
    }


    private IEnumerator  LaunchPost()
    {
        _IsLaunching = true;
        if (!ISPReady)
        {
            
            //TODO: Make the launch animation
            GameObject post = Instantiate(_ScratchPost, FindClosestPostDestination(), Quaternion.identity);
            post.GetComponent<ScratchingPost>().parentTower = gameObject;
            post.GetComponent<SphereCollider>().radius = AOE;
            post.GetComponent<ScratchingPost>().speedDebuff = speedDebuff;
        }
        else
        {
            GameObject post = Instantiate(_IrScratchPost, FindClosestPostDestination(), Quaternion.identity);
            post.GetComponent<IrresistableScratchingPost>().parentTower = gameObject;
            post.GetComponent<SphereCollider>().radius = AOE;
            StartCoroutine(ISPCooldown());
        }
        
        yield return new WaitForSeconds(_TimeBetweenLaunches);
        _IsLaunching = false;
    }
    private IEnumerator ISPCooldown()
    {
        ISPReady = false;
        yield return new WaitForSeconds(IrCooldown);
        ISPReady = true;
    }


}
