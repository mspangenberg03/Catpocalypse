using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;


public class LaserPointerTower : Tower
{
    // This is the object that the tower will parent it's selected path indicator to. This is necessary, as otherwise the arrow gets messed up by the scale/rotation of the tower and its parents.
    public static Transform SelectedPathIndicatorsParent;

    // This is the same idea, but for laser end point objects that laser towers instantiate.
    public static Transform LaserEndPointsParent;

        

    [SerializeField]
    private GameObject laserPrefab; // The laser prefab to be copied
    [SerializeField]
    private GameObject laserEndPointPrefab; // The laser end point effect prefab
    [Tooltip("The max number of simulateous lasers this tower can have.")]
    [SerializeField, Min(1)]
    private int MaxLasers = 1; // The number of lasers a tower can instantiate
    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField] 
    private Transform laserSpawn; //The spawn point of the laser


    [Tooltip("This sets the radius that the tower will search for the nearest path junction within.")]
    [Min(1f)]
    [SerializeField]
    private float _PathJunctionDetectionRadius = 10f;
    
    [Header("Laser End Point")]
    [Tooltip("The laser sweeps back and forth across the path, and this value sets how wide the laser's sweep is.")]
    [Min(0f)]
    private float _LaserSweepWidth = 1f;
    [Tooltip("This sets how long (in seconds) it takes for the laser to go back and forth one time.")]
    [Min(0.1f)]
    private float _LaserSweepTime = 2f;
    [Tooltip("This sets how far the laser should target in front of a cat.")]
    [Min(0f)]
    private float _DistanceInFrontOfTargetToAimFor = 2f;


    // This holds a reference to the nearest node that has more than one possible next node.
    private WayPoint _PathJunction;

    // This is the arrow used to show which direction is currently selected while in the tower manipulation UI.
    private GameObject _Arrow;


    private List<LaserInfo> _Lasers;
    private int _ActiveLasersCount; // The number of lasers that are currently active


    /*
    private List<TargetInfo> _ActiveTargets;
    private List<GameObject> lasers; // The list of instantiated lasers, both active and inactive
    private List<GameObject> laserEndPoints; // The list of instantiated end point effects for the lasers.
    private List<float> laserSweepTimers; // Holds the elapsed time for each laser. This is used to make the laser sweep back and forth.

    */


    void Awake()
    {
        _Lasers = new List<LaserInfo>();

        /*
        _ActiveTargets = new List<TargetInfo>();
        lasers = new List<GameObject>();
        laserEndPoints = new List<GameObject>();
        laserSweepTimers = new List<float>();
        */

        if (SelectedPathIndicatorsParent == null)
        {
            SelectedPathIndicatorsParent = new GameObject("Laser Pointer Towers Selected Path Indicators").transform;
            SelectedPathIndicatorsParent.transform.position = Vector3.zero;
        }

        if (LaserEndPointsParent == null)
        {
            LaserEndPointsParent = new GameObject("Laser End Point Objects").transform;
            LaserEndPointsParent.transform.position = Vector3.zero;
        }
    }

    private void Start()
    {
        // Find the path junction that is near this tower.
        _PathJunction = FindAssociatedPathJunction();
        if (_PathJunction == null)
            throw new Exception("There is no path junction within range of this laser pointer tower!");

        // Spawn the arrow that is used to show the select path.
        _Arrow = Instantiate(arrowPrefab, _PathJunction.transform.position + (Vector3.up * 1f), Quaternion.identity, SelectedPathIndicatorsParent);
        _Arrow.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_Lasers.Count < MaxLasers)
        {
            StartCoroutine(SpawnLasers());
        }
        StartCoroutine(LaserControl());


        if (_ActiveLasersCount < MaxLasers)
            SelectTargets();

        
        CheckActiveTargets();
    }

    protected override void InitStateMachine()
    {
        // NOTE: This code looks the same as the base class for now, but there is a subtle difference.
        //       The condition for entering the activeState from the idleState is different.


        // Create tower states.
        TowerState_Active_Base activeState = new TowerState_Active_Base(this);
        TowerState_Disabled_Base disabledState = new TowerState_Disabled_Base(this);
        TowerState_Idle_Base idleState = new TowerState_Idle_Base(this);
        TowerState_Upgrading_Base upgradingState = new TowerState_Upgrading_Base(this);


        // Create and register transitions.
        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        _stateMachine.AddTransitionFromState(idleState, new Transition(activeState, () => _ActiveLasersCount > 0));
        _stateMachine.AddTransitionFromState(disabledState, new Transition(idleState, () => IsTargetDetectionEnabled));

        _stateMachine.AddTransitionFromAnyState(new Transition(disabledState, () => !IsTargetDetectionEnabled));
        _stateMachine.AddTransitionFromAnyState(new Transition(idleState, () => IsTargetDetectionEnabled));

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        // Tell state machine to write in the debug console every time it exits or enters a state.
        //_stateMachine.EnableDebugLogging = true;

        // Set the starting state.
        _stateMachine.SetState(idleState);
    }

    /// <summary>
    /// This function is called when we have fewer targets than numOfLasers.
    /// It will select more targets from targets and add them to _ActiveTargets if they
    /// have not passed the path junction associated with this tower yet.
    /// </summary>
    private void SelectTargets()
    {
        foreach (GameObject target in targets)
        {
            CatBase cat = target.GetComponent<CatBase>();

            if (cat.NextWayPoint == null)
            {
                continue;
            }

            // Is this cat before the junction point associated with this tower?
            WaypointUtils.WayPointCompareResults result = WaypointUtils.CompareWayPointPositions(cat.NextWayPoint, _PathJunction);
            
            //Debug.Log($"Result: {result}    CatNextWaypoint: \"{cat.NextWayPoint.name}\"    JunctionWayPoint: \"{_PathJunction.name}\"");
            
            if (result == WaypointUtils.WayPointCompareResults.A_IsBeforeB ||
                result == WaypointUtils.WayPointCompareResults.A_And_B_AreSamePoint)
            {
                // Make this cat an active target for one of the lasers.
                TargetInfo info = new TargetInfo();
                info.TargetCat = cat;
                if (cat.NextWayPoint == PathJunction)
                    info.IsApproachingJunction = true;

                int laserIndex = GetIndexOfFirstInactiveLaser();
                if (laserIndex >= 0)
                {
                    ActivateLaser(laserIndex, info);
                }
                
                if (_ActiveLasersCount >= MaxLasers)
                    break;
            }

        } // end foreach

    }

    private void CheckActiveTargets()
    {
        for (int i = 0; i < _ActiveLasersCount; i++) 
        { 
            TargetInfo info = _Lasers[i].TargetInfo;
            if (info == null)
                continue;

            WayPoint nextWaypoint = info.TargetCat.NextWayPoint;

            // If IsApproachingJunction is false but the cat's next point is the path junction associated
            // with this tower, then set that flag to true now.
            if (info.IsApproachingJunction == false && nextWaypoint == PathJunction)
                info.IsApproachingJunction = true;

            // If IsApproachingJunction is true but the cat's next point is NOT the path junction associated
            // with this tower, then it means the cat has reached the path junction waypoint and targeted
            // the next one. So in this case, we now change it's next waypoint to be the path this
            // tower is set to distract cats to.
            else if (info.IsApproachingJunction == true && nextWaypoint == PathJunction)
                info.TargetCat.NextWayPoint = PathJunction.NextWayPoints[SelectedPathIndex];

        }
    }

    /// <summary>
    /// Finds the first inactive laser.
    /// </summary>
    /// <returns>The first inactive laser.</returns>
    private int GetIndexOfFirstInactiveLaser()
    {
        for (int i = 0; i < _Lasers.Count; i++)
        {
            if (_Lasers[i].TargetInfo == null)
                return i;
        }


        // There are no inactive lasers at this time, so return -1 as an error code.
        return -1;
    }

    /// <summary>
    /// Checks targets found by the base class' OnTriggerEnter() method, and only adds them if
    /// they are the right type.
    /// </summary>
    /// <remarks>
    /// See the comments on this function in the Tower class for more info.
    /// </remarks>
    /// <param name="target">A potential target passed in by the base class.</param>
    protected override void OnNewTargetEnteredRange(GameObject target)
    {
        // Only accept cats of the type this tower is set to target.
        if (target.GetComponent(_TargetCatType) != null)
            targets.Add(target);
    }

    protected override void OnTargetWentOutOfRange(GameObject target)
    {
        RemoveActiveTarget(target);
    }

    protected override void OnTargetHasDied(GameObject target)
    {
        RemoveActiveTarget(target);
    }
    private void RemoveActiveTarget(GameObject target)
    {
        CatBase cat = target.GetComponent<CatBase>();

        if (cat == null)
        {
            Debug.LogError("Target does not have a cat component!");
            return;
        }


        for (int i = 0; i < _ActiveLasersCount; i++)
        {
            if (_Lasers[i].TargetInfo != null && _Lasers[i].TargetInfo.TargetCat == cat)
            {
                DeactivateLaser(i);

                break;
            }
        } // end for i

    }

    private WayPoint FindAssociatedPathJunction()
    {
        WayPoint closestWayPoint = null;
        float closestDistance = float.MaxValue;

        foreach (Collider collider in Physics.OverlapSphere(transform.position, _PathJunctionDetectionRadius, LayerMask.GetMask("WayPoints")))
        {
            //Debug.Log("Found way point in range: " + collider.name);
            WayPoint p = collider.GetComponent<WayPoint>();

            // Is this object a waypoint, and is it a path branch point?
            if (p != null && p.NextWayPoints.Count > 1)
            {
                float distance = Vector3.Distance(transform.position, p.transform.position); 
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestWayPoint = p;
                }
            }
        } // end foreach


        //Debug.Log("Closest waypoint with branches: " + closestWayPoint.gameObject.name);
        return closestWayPoint;
    }

    IEnumerator LaserControl()
    {
        for( int i = 0; i < MaxLasers; i++)
        {
            //Base case for the tower. If any tower's still have target info, disable them.
            if(_ActiveLasersCount == 0 && _Lasers[i].TargetInfo != null)
            {
                DeactivateLaser(i);
                yield return new WaitForSeconds(1f);
            }           
/*            else if (_Lasers[i].TargetInfo == null)
            {
                //DeactivateLaser(i);
            }*/
            else if (_Lasers[i].TargetInfo != null && _ActiveLasersCount > 0)
            {
                // Checks for a null value in case a cat moves out of range
                if(_Lasers[i].TargetInfo != null)
                {
                    // Activates a laser if it is inactive
                    if (!_Lasers[i].Laser.activeSelf)
                    {
                        ActivateLaser(i, _Lasers[i].TargetInfo);
                    }

                    // Changes the laser's length and _ActiveTargets the cat with it
                    Vector3[] linePositions = new Vector3[2];
                    linePositions[0] = _Lasers[i].Laser.transform.position;

                    Vector3 targetPoint = CalculateLaserEndPoint(i);                   

                    linePositions[1] = targetPoint;

                    _Lasers[i].LaserEndPoint.transform.position = targetPoint;

                    //Debug.Log(linePositions[0] + "    " + _Lasers[i].TargetInfo.TargetCat.name + " " + linePositions[1] + "  " + _Lasers[i].TargetInfo.TargetCat.position);

                    _Lasers[i].Laser.GetComponent<LineRenderer>().SetPositions(linePositions);
                    

                    // This is commented out since laser should no longer do damage.
                    //_Lasers[i].TargetInfo.TargetCat.GetComponent<CatBase>().DistractCat(distractValue, this);
                }              

            }
            
        } // end for i
        
        yield return new WaitForSeconds(1f);

    }

    private void ActivateLaser(int laserIndex, TargetInfo targetInfo)
    {
        _Lasers[laserIndex].Activate(targetInfo);
        _Lasers[laserIndex].SweepTimer = _LaserSweepTime;
        _Lasers[laserIndex].SweepWidth = _LaserSweepWidth;
        _ActiveLasersCount++;
    }

    private void DeactivateLaser(int laserIndex)
    {
        _Lasers[laserIndex].Deactivate();
        _ActiveLasersCount--;
    }

    private void DeactivateAllLasers()
    {
        return;
        Debug.Log("DEACTIVATE ALL!");
        for (int i = 0; i < _Lasers.Count; i++)
        {
            DeactivateLaser(i);
        }
    }

    private Vector3 CalculateLaserEndPoint(int index)
    {
        CatBase targetCat = _Lasers[index].TargetInfo.TargetCat;
        Vector3 targetPoint = targetCat.transform.position;


        // Get the forward vector for the target and multiply by the distance we want to aim in front of the target.
        Vector3 forward = targetCat.transform.forward.normalized;
        forward *= _DistanceInFrontOfTargetToAimFor;

        // Calculate the right/left vectors from the perspective of direction.
        Vector3 right = new Vector3(forward.z, 0, -forward.x).normalized;
        //Vector3 left = -right;

        // Multiply by the laser's SweepWidth so we get the desired sweep width.
        right *= _Lasers[index].SweepWidth;

        // Update the sweep timer for this laser.
        _Lasers[index].SweepTimer += Time.deltaTime;

        // Make the laser move back and forth.
        // First, calculate the angle to pass into the Sin function.
        float angle = 360 * (_Lasers[index].SweepTimer / _LaserSweepTime);
        if (angle >= 360)
            _Lasers[index].SweepTimer = 0f; // Reset this laser's sweep timer.

        // Convert the angle from degrees to radians, and then calculate the sine value.
        angle *= Mathf.Deg2Rad;
        float sinValue = Mathf.Sin(Mathf.Clamp(angle, 0f, 360f));

        // Calculate the position of the laser pointer relative to the center of the path.
        // This function is creating a sine wave that is aligned to the path basically.
        right *= sinValue;

        
        // Add the vectors we calculated to the target point to get the final target point in front of the cat.
        targetPoint += forward;
        targetPoint += right;

        return targetPoint;
    }


    // Spawns the laser and sets its position to the top of the tower
    IEnumerator SpawnLasers()
    {
        for (int i = _Lasers.Count; i < MaxLasers; i++)
        {
            LaserInfo newLaser = new LaserInfo();

            newLaser.Laser = Instantiate(laserPrefab, laserSpawn);
            newLaser.Laser.gameObject.GetComponent<AudioSource>().Stop();

            newLaser.LaserEndPoint = Instantiate(laserEndPointPrefab, LaserEndPointsParent);
            newLaser.LaserEndPoint.gameObject.SetActive(false);

            newLaser.SweepTimer = _LaserSweepTime;
            newLaser.SweepWidth = _LaserSweepWidth;

            _Lasers.Add(newLaser);

            yield return new WaitForSeconds(1f);
        }
    }


    /// <summary>
    /// Sets the rotation of the arrow that shows the selected path branch.
    /// </summary>
    /// <remarks>
    /// The rotation determines the direction the arrow points.
    /// </remarks>
    /// <param name="rotation">The rotation in degrees.</param>
    public void SetArrowRotation(float rotation)
    {
        Quaternion q = _Arrow.transform.rotation;
        q.eulerAngles = new Vector3(0f, -rotation, 0f);
        _Arrow.transform.rotation = q;
    }

    public override void EnableTargetDetection()
    {
        base.EnableTargetDetection();


        // Clear all current laser targets.
        DeactivateAllLasers();
    }

    public override void DisableTargetDetection()
    {
        base.DisableTargetDetection();

        // Clear all current laser targets.
        DeactivateAllLasers();
    }

    protected override void OnRallyPointChanged()
    {
        
    }


    /// <summary>
    /// This property controls whether or not the arrow that shows the selected path branch is visible or not.
    /// </summary>
    public bool ArrowIsVisible 
    {
        get { return _Arrow.activeSelf; }
        set 
        { 
            if (_Arrow != null)
                _Arrow.SetActive(value); 
        }
    }

    /// <summary>
    /// This specifies the index of the selected path. Specifically, it is the index of the first node on that
    /// path after _PathJunction. So basically, it is an index into _PathJunction.NextNodes to get the right next node.
    /// </summary>
    public int SelectedPathIndex
    {
        get; set;
    }

    public WayPoint SelectedPathNextNode { get { return _PathJunction.NextWayPoints[SelectedPathIndex]; } }
    

    public WayPoint PathJunction { get { return _PathJunction; } }



    private struct ExtraTargetInfo
    {
        public bool HasReachedJunction;
    }



    private class TargetInfo
    {
        public CatBase TargetCat;
        public bool IsApproachingJunction; // This will be true if the cat's next waypoint is set to the junction associated with this tower.
    }


    /// <summary>
    /// Stores info related to a given laser.
    /// </summary>
    private class LaserInfo
    {
        public GameObject Laser; // The instantiated laser GameObject
        public GameObject LaserEndPoint; // The instantiated end point effects GameObject for the laser.
        public float SweepTimer; // Holds the elapsed time for the laser. This is used to make the laser sweep back and forth.
        public float SweepWidth; // How far back and forth the laser sweeps.
        public TargetInfo TargetInfo = null;



        public void Activate(TargetInfo targetInfo)
        {
            Debug.Log("ACTIVATE");
            Laser.SetActive(true);
            Laser.gameObject.GetComponent<AudioSource>().Play();
            LaserEndPoint.SetActive(true);
            SweepTimer = 2f;
            TargetInfo = targetInfo;           
        }

        public void Deactivate()
        {
            Debug.Log("DEACTIVATE");
            Laser.gameObject.GetComponent<AudioSource>().Stop();
            Laser.SetActive(false);
            LaserEndPoint.SetActive(false);
            SweepTimer = 0f;
            TargetInfo = null;
        }
    }
}


