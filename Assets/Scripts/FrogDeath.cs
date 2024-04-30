/***
 * FrogDeath.cs
 * Version 1
 * By Nathan Boles
 * 
 * This script should be attached to a collider that is below the camera's view.
 * It watches when the player reaches the threshold, if they do, the frog character dies, and the game resets. 
 * May add checkpoints to the level at a later point, but given how limited my access to stuff is right now, 
 * 
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogDeath : MonoBehaviour
{
    GameManager gm; //The GameManager script in this scene. There should only be one!

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }
    
    /// <summary>
    /// If the player falls into this collider, they are considered to have died. 
    /// This script thus tells GameManager to increment the death count while also 
    /// restarting the level. This may be changed later to reset things to a checkpoint instead.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gm.UpdateDeaths(1);
            gm.Restart();
        }
    }
}

