using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveFloor : MonoBehaviour
{
    public int floorLevelToLoad;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        // check when the object collides with the player
        if (collision.transform.GetComponent<PlayerController>())
        {
            Debug.Log("Loading next level");
            // load the next level
            SceneManager.LoadScene(floorLevelToLoad);
        }
    }
}
