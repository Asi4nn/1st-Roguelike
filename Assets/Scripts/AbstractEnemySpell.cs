using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractEnemySpell : MonoBehaviour
{
    public GameObject projectile;
    public Transform player;
    public float minDamage;
    public float maxDamage;
    public float projectileSpeed;
    public float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShootPlayer()
    {
        yield return new WaitForSeconds(cooldown);
        if (player != null)
        {
            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 thisPos = transform.position;
            Vector2 targetPos = player.position;
            Vector2 direction = (targetPos - thisPos).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            spell.GetComponent<TestEnemyProjectile>().damage = Random.Range(minDamage, maxDamage);
            StartCoroutine(ShootPlayer());
        }
    }
}
