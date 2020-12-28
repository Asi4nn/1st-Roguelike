using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 100f) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player")
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                collision.GetComponent<Enemy>().DealDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
