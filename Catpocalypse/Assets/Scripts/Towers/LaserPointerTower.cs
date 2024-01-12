using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserPointerTower : Tower
{
    [SerializeField]
    private GameObject laserPrefab; // The laser prefab to be copied
    [SerializeField]
    private GameObject laserEndPointPrefab; // The laser end point effect prefab
    [SerializeField]
    private int numOfLasers; // The number of lasers a tower can instantiate
    [SerializeField]
    private GameObject laserPointerTower; //

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


    private List<GameObject> lasers; // The list of instantiated lasers, both active and inactive
    private List<GameObject> laserEndPoints; // The list of instantiated end point effects for the lasers.
    private List<float> laserSweepTimers; // Holds the elapsed time for each laser. This is used to make the laser sweep back and forth.


    public void Awake()
    {
        lasers = new List<GameObject>();
        laserEndPoints = new List<GameObject>();
        laserSweepTimers = new List<float>();

        laserPointerTower = gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if(lasers.Count < numOfLasers)
        {
            StartCoroutine(SpawnLasers());
        }
        StartCoroutine(LaserControl());
    }
    

    IEnumerator LaserControl()
    {
        for( int i = 0; i < numOfLasers; i++)
        {
            //Base case for the tower
            if(targets.Count == 0)
            {
                DeactivateLaser(i);
                yield return new WaitForSeconds(1f);
            }
            else if (i >= targets.Count)
            {
                DeactivateLaser(i);
            }
            else if(i < targets.Count && targets.Count != 0)
            {
                // Checks for a null value in case a cat moves out of range
                if(targets[i] != null)
                {
                    // Activates a laser if it is inactive
                    if (!lasers[i].activeSelf) { 
                        lasers[i].SetActive(true);
                        lasers[i].gameObject.GetComponent<AudioSource>().Play();
                        laserEndPoints[i].SetActive(true);
                    }

                    // Changes the laser's length and targets the cat with it
                    Vector3[] linePositions = new Vector3[2];
                    linePositions[0] = lasers[0].transform.position;

                    Vector3 targetPoint = CalculateLaserEndPoint(i);
                   

                    linePositions[1] = targetPoint;

                    laserEndPoints[i].transform.position = targetPoint;

                    //Debug.Log(linePositions[0] + "    " + targets[i].name + " " + linePositions[1] + "  " + targets[i].transform.position);

                    lasers[i].GetComponent<LineRenderer>().SetPositions(linePositions);
                    targets[i].GetComponent<CatBase>().DistractCat(distractValue, this);
                }
                  
            } 

        }
        yield return new WaitForSeconds(1f);
    }

    private void DeactivateLaser(int laserIndex)
    {
        lasers[laserIndex].gameObject.GetComponent<AudioSource>().Stop();
        lasers[laserIndex].SetActive(false);
        laserEndPoints[laserIndex].SetActive(false);
        laserSweepTimers[laserIndex] = 0f;
    }

    private Vector3 CalculateLaserEndPoint(int index)
    {
        Vector3 targetPoint = targets[index].transform.position;


        // Get the forward vector for the target and multiply by the distance we want to aim in front of the target.
        Vector3 forward = targets[index].transform.forward.normalized;
        forward *= _DistanceInFrontOfTargetToAimFor;

        // Calculate the right/left vectors from the perspective of direction.
        Vector3 right = new Vector3(forward.z, 0, -forward.x).normalized;
        //Vector3 left = -right;

        // Multiply by the laserSweepWidth so we get the desired sweep width.
        right *= _LaserSweepWidth;

        // Update the sweep timer for this laser.
        laserSweepTimers[index] += Time.deltaTime;

        // Make the laser move back and forth.
        // First, calculate the angle to pass into the Sin function.
        float angle = 360 * (laserSweepTimers[index] / _LaserSweepTime);
        if (angle >= 360)
            laserSweepTimers[index] = 0f; // Reset this laser's sweep timer.

        // Convert the angle from degrees to radians, and then calculate the sine value.
        angle *= Mathf.Deg2Rad;
        float sinValue = Mathf.Sin(Mathf.Clamp(angle, 0f, 360f));

        // Calculate the position of the laser pointer relative to the center of the path.
        // This function is creating a sine wave that is aligned to the path basically.
        right *= sinValue;

        

        targetPoint += forward;
        targetPoint += right;

        return targetPoint;
    }


    // Spawns the laser and sets its position to the top of the tower
    IEnumerator SpawnLasers()
    {
        for (int i = lasers.Count; i < numOfLasers; i++)
        {

            lasers.Add(Instantiate(laserPrefab, gameObject.transform));
            lasers[i].gameObject.GetComponent<AudioSource>().Stop();

            laserEndPoints.Add(Instantiate(laserEndPointPrefab));
            laserEndPoints[i].gameObject.SetActive(false);

            laserSweepTimers.Add(0f);

            yield return new WaitForSeconds(1f);
        }
    }
}
