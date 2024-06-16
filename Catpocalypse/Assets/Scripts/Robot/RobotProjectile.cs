using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// This class represents a projectile fired by the robot.
/// </summary>
public class RobotProjectile : MonoBehaviour
{
    [Tooltip("How much this projectile distracts a cat when it hits one.")]
    [SerializeField, Min(0f)]
    protected float _DistractionAmount = 100f;

    [Tooltip("Sets the launch speed of this projectile in meters per seecond.")]
    [SerializeField, Min(0f)]
    public float _LaunchSpeed = 10f;

    [Tooltip("How long the projectile will exist before it disappears.")]
    [SerializeField, Min(1f)]
    protected float _Lifetime = 10f;



    /// <summary>
    /// Tracks how long the projectile has existed for.
    /// </summary>
    private float _Timer;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _Timer += Time.deltaTime;
        if (_Timer >= _Lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cat")
        {
            CatBase target = collision.gameObject.GetComponent<CatBase>();

            if (target != null)
                Distract(target);
        }
    }

    /// <summary>
    /// This function is called to distract the cat the projectile hit.
    /// </summary>
    /// <param name="target"></param>
    private void Distract(CatBase target)
    {
        target.DistractCat(_DistractionAmount, null);

        Destroy(gameObject);
    }



    public float DistractionAmount
    {
        get { return _DistractionAmount; }
        set { _DistractionAmount = value; }
    }

    public float LaunchSpeed
    {
        get { return _LaunchSpeed; }
        set { _LaunchSpeed = value; }
    }

    public float LifeTime
    {
        get { return _Lifetime; }
        set { _Lifetime = value; }
    }

}
