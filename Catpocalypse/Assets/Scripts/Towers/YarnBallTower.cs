using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnBallTower : Tower
{
    [SerializeField] private GameObject yarnBallPrefab;
    [SerializeField] private GameObject throwPointPrefab;
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float throwRange = 5f;
    private bool canThrow = true;

    void Start()
    {
        // Start the projectile throwing coroutine
        StartCoroutine(ThrowProjectiles());
    }

    IEnumerator DistractCat(GameObject cat)
    {

        if (targets.Contains(cat) && cat != null)
        {

            cat.GetComponent<CatBase>().DistractCat(distractValue, this.gameObject.GetComponent<Tower>());
            yield return new WaitForSeconds(5f);
            StartCoroutine(DistractCat(cat));
        }

    }

    IEnumerator ThrowProjectiles()
    {
        while (true)
        {
            // Check if canThrow is true before throwing the projectile
            if (canThrow)
            {
                if (IsTargetInRange())
                {
                    ThrowProjectile();
                    canThrow = false; // Set to false after throwing, prevent further throws until reset

                    // Wait for a specified time before allowing another throw
                    yield return new WaitForSeconds(4f); // Adjust the delay as needed

                    canThrow = true; // Set back to true to allow another throw
                }
            }

            yield return null;
        }
    }

    bool IsTargetInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, throwRange);

        foreach (Collider collider in colliders)
        {
            // Check if the collider belongs to the "cat" layer
            if (collider.gameObject.layer == LayerMask.NameToLayer("Cat"))
            {
                return true;
            }
        }

        return false;
    }

    void ThrowProjectile()
    {
        if (throwPointPrefab != null)
        {
            // Instantiate the throwPoint prefab
            GameObject throwPointObject = Instantiate(throwPointPrefab, transform.position, transform.rotation);
            Transform throwPoint = throwPointObject.transform;

            GameObject projectile = Instantiate(yarnBallPrefab, throwPoint.position, throwPoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Get the target GameObject with the "cat" layer
                GameObject target = FindTargetByLayer("Cat");

                if (target != null)
                {
                    // Calculate the direction to the target
                    Vector3 direction = (target.transform.position - throwPoint.position).normalized;

                    // Apply force in the direction of the target
                    rb.AddForce(direction * throwForce, ForceMode.Impulse);
                }
            }
        }
            
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cat")
        {
            targets.Add(other.gameObject);
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cat")
        {
            targets.Remove(other.gameObject);
            
        }
    }

    GameObject FindTargetByLayer(string Cat)
    {
        // Find the target by layer name
        GameObject[] targets = GameObject.FindGameObjectsWithTag(Cat);

        if (targets.Length > 0)
        {
            return targets[0]; // Assuming there is only one target
        }

        return null;
    }
}