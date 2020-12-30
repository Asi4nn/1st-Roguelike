﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyProjectile : MonoBehaviour
{
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats.playerStats.DealDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Solid"))
        {
            Destroy(gameObject);
        }
    }
}
