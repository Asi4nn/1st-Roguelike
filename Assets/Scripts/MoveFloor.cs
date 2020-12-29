using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveFloor : MonoBehaviour
{
    public int floorLevelToLoad;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check when the object collides with the player
        if (collision.GetComponent<PlayerController>())
        {
            // load the next level
            SceneManager.LoadScene(floorLevelToLoad);
        }
    }
}
