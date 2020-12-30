using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicElementSpell : AbstractSpell
{
    public List<GameObject> elementProjectiles;
    public ElementProjectile.ElementType element;

    [SerializeField] List<GameObject> elementIndicators;
    [SerializeField] float cooldown;

    private bool attackOnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        attackOnCooldown = false;
        SetElement(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetElement(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetElement(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetElement(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetElement(3);
        }


        if (Input.GetMouseButton(0) && !attackOnCooldown)
        {
            StartCoroutine(ShootProjectile());
        }
    }

    private void SetElement(int index)
    {
        projectile = elementProjectiles[index];
        foreach (GameObject ind in elementIndicators) 
        {
            ind.SetActive(false);
        }
        elementIndicators[index].SetActive(true);
        if (index == 0)
        {
            element = ElementProjectile.ElementType.Air;
        }
        else if (index == 1)
        {
            element = ElementProjectile.ElementType.Earth;
        }
        else if (index == 2)
        {
            element = ElementProjectile.ElementType.Water;
        }
        else if (index == 3)
        {
            element = ElementProjectile.ElementType.Fire;
        }
        else
        {
            throw new ArgumentException("Parameter must be from 0-3", nameof(index));
        }
    }

    IEnumerator ShootProjectile()
    {
        attackOnCooldown = true;
        GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        Vector2 direction = (mousePos - playerPos).normalized;
        spell.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        ElementProjectile proj = spell.GetComponent<ElementProjectile>();
        proj.damage = Random.Range(minDamage, maxDamage);
        proj.element = element;

        yield return new WaitForSeconds(cooldown);
        attackOnCooldown = false;
    }
}
