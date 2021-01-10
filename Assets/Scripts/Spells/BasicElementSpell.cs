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
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();

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
        // Vector2 direction = (mousePos - playerPos).normalized;
        PlayerController.Facing direction = CalculateFireDirection(mousePos, playerPos);
        spell.GetComponent<Rigidbody2D>().velocity = FacingDirectionToVector(direction) * projectileSpeed;
        ElementProjectile proj = spell.GetComponent<ElementProjectile>();
        proj.damage = Random.Range(minDamage, maxDamage);
        proj.element = element;

        // update direction in player controller (this may not be the most efficient)
        playerController.firingDirection = direction;

        yield return new WaitForSeconds(cooldown);
        attackOnCooldown = false;
    }

    private PlayerController.Facing CalculateFireDirection(Vector2 mousePos, Vector2 playerPos)
    {
        Vector2 relativeMousePos = mousePos - playerPos;
        float sqrt2Over2 = Mathf.Sqrt(2)/2;

        /*
         * Rotate relativeMousePos by rotation matrix:
         * [[sqrt(2)/2 , sqrt(2)/2],
         *  [-sqrt(2)/2, sqrt(2)/2]]
         *  Rotation 45 degrees clockwise
         */

        //relativeMousePos = new Vector2(relativeMousePos.x * sqrt2Over2 + relativeMousePos.y * -sqrt2Over2,
        //                               relativeMousePos.x * sqrt2Over2 + relativeMousePos.y * sqrt2Over2);
        float relativeX = relativeMousePos.x * sqrt2Over2 + relativeMousePos.y * sqrt2Over2;
        float relativeY = relativeMousePos.x * -sqrt2Over2 + relativeMousePos.y * sqrt2Over2;

        if (relativeX >= 0 && relativeY >= 0)
        {
            return PlayerController.Facing.UP;
        }
        else if (relativeX >= 0 && relativeY < 0)
        {
            return PlayerController.Facing.RIGHT;
        }
        else if (relativeX < 0 && relativeY >= 0)
        {
            return PlayerController.Facing.LEFT;
        }
        else
        {
            return PlayerController.Facing.DOWN;
        }
    }

    private Vector2 FacingDirectionToVector(PlayerController.Facing facing)
    {
        if (facing == PlayerController.Facing.UP)
        {
            return new Vector2(0, 1);
        }
        else if (facing == PlayerController.Facing.RIGHT)
        {
            return new Vector2(1, 0);
        }
        else if (facing == PlayerController.Facing.DOWN)
        {
            return new Vector2(0, -1);
        }
        else
        {
            return new Vector2(-1, 0);
        }
    }
}
