using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

// This fixes the ambiguity between System.Random and UnityEngine.Random by
// telling it to use the Unity one.
using Random = UnityEngine.Random;
using UnityEngine.UIElements;

public class CatBase : MonoBehaviour
{
    public static bool ShowDistractednessBar = true;



    // This event is static so we don't need to subscribe the money manager to every cat instance's OnCatDied event.
    public static event EventHandler OnCatDied;
    public static event EventHandler OnCatReachGoal;


    [Tooltip("The cuteness value is how much this type of cat increases the cuteness meter.")]
    [Min(0)]
    [SerializeField] protected int _CutenessValue = 5;

    [Min(0)]
    [SerializeField] protected float distractionThreshold = 50; //The amount of distraction it takes to fully distract the cat
    [Min(0f)]
    [SerializeField] protected float damageToPlayer = 2f; //How much health the cat takes from the player

    [Min(0f)]
    [Tooltip("This sets how close the cat must get to the next WayPoint to consider itself to have arrived there. This causes it to then target the next WayPoint (or a randomly selected one if the current WayPoint has multiple next points set in the Inspector.")]
    [SerializeField] protected float _WayPointArrivedDistance = 2f;

    [Tooltip("How much money to player gets for distracting this type of cat.")]
    [SerializeField] protected float distractReward = 50;

    [Header("Distractedness Meter")]
    [SerializeField] protected float _DistractednessMeterHeightAboveCat = 2f;
    [SerializeField] protected GameObject _DistractednessMeterPrefab;



    protected float distraction = 0; //How distracted the cat is currently
    protected bool isDistracted = false; // If the cat has been defeated or not.
    //Rigidbody rb;//The RigidBody component
    private NavMeshAgent agent;

    protected PlayerHealthManager healthManager;

    protected float _DistanceFromNextWayPoint = 0f;
    protected WayPoint _NextWayPoint;

    protected GameObject _DistractednessMeterGO;
    protected UnityEngine.UI.Image _DistractednessMeterBarImage;
    protected TextMeshPro _DistractednessMeterLabel;

    public List<AudioClip> sounds = new List<AudioClip>();
    private AudioSource catAudio;

    public List<AudioClip> purrs = new List<AudioClip>();

    public bool isSlowed;


    // Start is called before the first frame update
    void Start()    
    {
        IsDead = false;

        catAudio = GetComponent<AudioSource>();
        InitDistractednessMeter();
        int index = Random.Range(0, sounds.Count - 1);

        catAudio.clip = sounds[index];
        catAudio.Play();
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


        _DistractednessMeterGO.SetActive(ShowDistractednessBar);
    }

    private void InitDistractednessMeter()
    {
        Transform distractednessMeter = Instantiate(_DistractednessMeterPrefab).transform;
        _DistractednessMeterGO = distractednessMeter.gameObject;

        distractednessMeter.SetParent(transform, true); // I'm parenting it this way rather than using the Instantiate() function above, because I need it to not inherit scale from the cat.
        distractednessMeter.localPosition = new Vector3(0, _DistractednessMeterHeightAboveCat, 0);

        _DistractednessMeterBarImage = distractednessMeter.Find("DistractednessBar").GetComponent<UnityEngine.UI.Image>();
        _DistractednessMeterLabel = distractednessMeter.Find("DistractednessLabel").GetComponent<TextMeshPro>();

        UpdateDistractednessMeter();
    }

    private void UpdateDistractednessMeter()
    {        
        _DistractednessMeterBarImage.fillAmount = (float) distraction / distractionThreshold;
        _DistractednessMeterLabel.text = $"Distractedness: {distraction} of {distractionThreshold}";
    }

    protected void Distracted()
    {
        isDistracted = true;
    }
    //I am intending this function to be called from either the tower or the projectile that the tower fires
    public void DistractCat(float distractionValue, Tower targetingTower)
    {

        if (PlayerCutenessManager.Instance.CurrentCutenessChallenge == PlayerCutenessManager.CutenessChallenges.CatsGetHarderToDistract)
        {
            float debuffPercent = PlayerCutenessManager.Instance.CuteChallenge_CatsGetHarderToDistract_DebuffPercent;
            distractionValue = distractionValue * debuffPercent;
        }



        distraction += distractionValue;
        UpdateDistractednessMeter();

        if (distraction >= distractionThreshold)
        {
            StartCoroutine(Sound());

            targetingTower.targets.Remove(this.gameObject);           
            
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            healthManager.TakeDamage(damageToPlayer);
            OnCatReachGoal?.Invoke(this, EventArgs.Empty);

            KillCat(2);
        }
    }


    protected void KillCat(int type)
    {
        if (IsDead)
            return;


        // Prevents this function from running twice in rare cases, causing this cat's death to count as more than one.
        IsDead = true;
        if(type == 1)
        {
            // Fire the OnCatDied event.
            OnCatDied?.Invoke(this, EventArgs.Empty);
        }
        else if (type == 2) 
        {
            OnCatReachGoal?.Invoke(this, EventArgs.Empty);
        }

        
        Destroy(_DistractednessMeterBarImage.transform.parent.gameObject);

        // Destroy the cat.
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



    public int Cuteness { get { return _CutenessValue; } }
    IEnumerator Sound()
    {
        agent.speed = 0;
        int index = Random.Range(0, purrs.Count - 1);

        catAudio.clip = purrs[index];
        catAudio.Play();
        yield return new WaitForSeconds(0.5f);

        KillCat(1);
    }


    public float DistractionReward { get { return distractReward; } }
    public bool IsDead { get; private set; }
}