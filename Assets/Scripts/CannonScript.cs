using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> bombPrefabs;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject target;
    [SerializeField] private Vector2 timeInterval;
    [SerializeField] private Vector2 bombForce;
    [SerializeField] private float rangeInDegrees;
    [SerializeField] private float arcInDegrees;
    private float cooldown;
    void Start()
    {
        cooldown = Random.Range(timeInterval.x, timeInterval.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
        {
            cooldown = Random.Range(timeInterval.x, timeInterval.y);

            Fire();
        }

    }

    private void Fire()
    {
        GameObject bombPrefab = bombPrefabs[Random.Range(0, bombPrefabs.Count)];

        GameObject bomb = Instantiate(bombPrefab, spawnPoint.transform.position, bombPrefab.transform.rotation);

        Rigidbody bombRigidBody = bomb.GetComponent<Rigidbody>();
        Vector3 impulseVector = target.transform.position - spawnPoint.transform.position;
        impulseVector.Scale(new Vector3(1, 0, 1));
        impulseVector.Normalize();
        impulseVector += new Vector3(0f, arcInDegrees / 45f, 0f);
        impulseVector.Normalize();
        impulseVector = Quaternion.AngleAxis(rangeInDegrees * Random.Range(-1f, 1f), Vector3.up) * impulseVector;

        impulseVector *= Random.Range(bombForce.x, bombForce.y);
        bombRigidBody.AddForce(impulseVector, ForceMode.Impulse);
    }
}
