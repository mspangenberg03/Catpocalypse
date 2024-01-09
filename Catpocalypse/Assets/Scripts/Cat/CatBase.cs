using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

// This fixes the ambiguity between System.Random and UnityEngine.Random by
// telling it to use the Unity one.
using Random = UnityEngine.Random;


public class CatBase : MonoBehaviour
{
    // This event is static so we don't need to subscribe the money manager to every cat instance's OnCatDied event.
    public static event EventHandler OnCatDied;


    [Min(0)]
    [SerializeField] protected int distractionThreshold = 50; //The amount of distraction it takes to fully distract the cat
    [Min(0f)]
    [SerializeField] protected float damageToPlayer = 2f; //How much health the cat takes from the player

    [Min(0f)]
    [Tooltip("This sets how close the cat must get to the next WayPoint to consider itself to have arrived there. This causes it to then target the next WayPoint (or a randomly selected one if the current WayPoint has multiple next points set in the Inspector.")]
    [SerializeField] protected float _WayPointArrivedDistance = 2f;

    [SerializeField] protected int distractReward = 50;

    protected int distraction = 0; //How distracted the cat is currently
    protected bool isDistracted = false; // If the cat has been defeated or not.
    //Rigidbody rb;//The RigidBody component
    private NavMeshAgent agent;

    protected PlayerHealthManager healthManager;

    protected float _DistanceFromNextWayPoint = 0f;
    protected WayPoint _NextWayPoint;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        healthManager = GameObject.FindGameObjectWithTag("Goal").gameObject.GetComponent<PlayerHealthManager>();

        // Find the closest WayPoint and start moving there.
        FindNearestWayPoint();
        agent.SetDestination(_NextWayPoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (distraction >= distractionThreshold && isDistracted == false)
        {
            Distracted();
        }

        if (_NextWayPoint != null)
        {
            _DistanceFromNextWayPoint = Vector3.Distance(transform.position, _NextWayPoint.transform.position);

            if (HasReachedDestination())
            {
                GetNextWaypoint();

                // Check for null in case we are already at the last WayPoint, as GetNextWayPoint()
                // returns null if there is no next WayPoint.
                if (_NextWayPoint != null)
                    agent.SetDestination(_NextWayPoint.transform.position);
            }
        }
        else
        {
            _DistanceFromNextWayPoint = 0f;
        }
    }
    protected void Distracted()
    {
        isDistracted = true;
    }
    //I am intending this function to be called from either the tower or the projectile that the tower fires
    public void DistractCat(int distractionValue, Tower targetingTower)
    {
        distraction += distractionValue;
        if (this.distraction >= this.distractionThreshold)
        {
            targetingTower.targets.Remove(this.gameObject);
            KillCat();
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            healthManager.TakeDamage(damageToPlayer);
            KillCat();
        }
    }


    protected void KillCat()
    {
        // Fire the OnCatDied event.
        OnCatDied?.Invoke(this, EventArgs.Empty);

        Destroy(gameObject);
    }

    protected void GetNextWaypoint()
    {
        int count = _NextWayPoint.NextWayPoints.Count;

        if (count == 0)
        {
            _NextWayPoint = null;
        }
        else if (count == 1)
        {
            _NextWayPoint = _NextWayPoint.NextWayPoints[0];
        }
        else // count is greater than 1
        {
            // The current waypoint has multiple next waypoints, so we will
            // select one at random.
            _NextWayPoint = _NextWayPoint.NextWayPoints[Random.Range(0, count)];
        }

    }

    protected void FindNearestWayPoint()
    {
        float minDistance = float.MaxValue;
        WayPoint nearestWayPoint = null;

        foreach (WayPoint wayPoint in FindObjectsByType<WayPoint>(FindObjectsSortMode.None))
        {
            float distance = Vector3.Distance(transform.position, wayPoint.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestWayPoint = wayPoint;
            }
        }


        _NextWayPoint = nearestWayPoint;
    }

    public bool HasReachedDestination()
    {
        return _DistanceFromNextWayPoint <= _WayPointArrivedDistance &&
               agent.pathStatus == NavMeshPathStatus.PathComplete;
    }

}