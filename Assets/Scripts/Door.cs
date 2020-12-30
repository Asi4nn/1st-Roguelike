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
    private bool canMove = true;
    // centre of room, used to transport camera to other room
    public Transform centre;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if the player is colliding with the door
        if (collision.transform.GetComponent<PlayerController>() && canMove)
        {
            // move the camera to the other room
            Camera camera = FindObjectOfType<Camera>();
            camera.transform.position = otherDoor.centre.position + new Vector3(0f, 0f, -10f);
            //camera.transform.position += new Vector3(0, 20f);

            collision.transform.position = otherDoor.transform.position;
            canMove = false;
            otherDoor.canMove = false;
            StartCoroutine(ResetCanMove(timeBetweenDoorTeleports));
        }
    }

    // reset the door's ability to move the player after a designated amount of time
    public IEnumerator ResetCanMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        // make the player able to move between the two doors again after 5 seconds
        canMove = true;
        otherDoor.canMove = true;
    }
}
