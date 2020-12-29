using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // default at 5
    public float timeBetweenDoorTeleports = 5;
    // other door this door is connected to
    public Door otherDoor;
    // can initially move other player
    protected bool canMove = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if the player is colliding with the door
        if (collision.GetComponent<PlayerController>() && canMove)
        {
            collision.transform.position = otherDoor.transform.position;
            canMove = false;
            otherDoor.canMove = false;
            otherDoor.StartCoroutine(ResetCanMove(timeBetweenDoorTeleports));
            StartCoroutine(ResetCanMove(timeBetweenDoorTeleports));
        }
    }

    // reset the door's ability to move the player after a designated amount of time
    public IEnumerator ResetCanMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        // make the player able to move between the two doors again after 5 seconds
        canMove = true;
    }
}
