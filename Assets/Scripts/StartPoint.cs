using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        // make a new player on scene load
        Instantiate(player);

        // find the player
        PlayerController thePlayer = FindObjectOfType<PlayerController>();
        // move the player to this position
        thePlayer.transform.position = transform.position;

        Camera theCamera = FindObjectOfType<Camera>();
        // add -10 on z axis so everything can be viewed
        theCamera.transform.position = transform.position + new Vector3(0f, 0f, -10f);
    }
}
