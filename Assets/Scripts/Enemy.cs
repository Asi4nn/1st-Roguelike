using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float maxHealth;

    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void MoveToPlayer()
    {
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        transform.Translate((player.transform.position - transform.position).normalized * speed * Time.deltaTime);
    }
}
