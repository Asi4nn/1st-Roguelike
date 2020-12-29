using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // find the player
        PlayerController thePlayer = FindObjectOfType<PlayerController>();
        // move the player to this position
        thePlayer.transform.position = transform.position;
    }
}
