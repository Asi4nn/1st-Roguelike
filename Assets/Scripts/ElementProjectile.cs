using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementProjectile : MonoBehaviour
{
    public float damage;

    public enum ElementType
    {
        Air,
        Earth,
        Water,
        Fire
    }
    public ElementType element;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 1000f) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().DealDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Solid"))
        {
            Destroy(gameObject);
        }
    }
}
