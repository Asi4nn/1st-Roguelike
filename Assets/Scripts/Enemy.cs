using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public GameObject healthBar;
    public Slider healthSlider;

    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // MoveToPlayer();
    }

    public void DealDamage(float damage)
    {
        healthBar.SetActive(true);
        health -= damage;
        healthSlider.value = CalculateHealthPercent();
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

    private float CalculateHealthPercent()
    {
        return health / maxHealth;
    }
}
