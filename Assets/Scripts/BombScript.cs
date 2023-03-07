using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] private GameObject woodBreakingPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float timeToDetonate;
    [SerializeField] private float blastRadius;
    [SerializeField] private int blastDamage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplosionCoroutine(timeToDetonate));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ExplosionCoroutine(float ExplosionDelay)
    {
        yield return new WaitForSeconds(ExplosionDelay);

        Explode();
    }

    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider collider in colliders)
        {
            if(collider.CompareTag("Platform"))
            {
                if (!collider.TryGetComponent<LifeScript>(out LifeScript lifeScript)) continue;

                float distance = ( collider.transform.position - transform.position ).magnitude;
                float distanceRate = Mathf.Clamp(distance / blastRadius, 0, 1);
                float damageRate = 1f - Mathf.Pow(distanceRate, 5);
                int damage = (int)Mathf.Ceil(damageRate * blastDamage);

                lifeScript.healthPoints -= damage;
                
                if(lifeScript.healthPoints <= 0)
                {
                    Instantiate(woodBreakingPrefab, collider.transform.position, woodBreakingPrefab.transform.rotation);
                    Destroy(collider.gameObject);
                }
                 
            }
                
        }

        Destroy(gameObject);
        
    }
}
