using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    public int maxHealth;
    [HideInInspector] public int healthPoints;
    void Start()
    {
        healthPoints = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
