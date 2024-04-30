/***
 * FrogJump.cs
 * Version 1
 * By Nathan Boles
 * 
 * This script is to be attached to the collectables in the game. It watches for when the appropriate tag touches it
 * adds it's points to the players score, before disappearing from the game forever
 * 
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [Tooltip("How many points does the player earn for collecting this. Please keep in mind that this can be set in prefabs so that you don't need to change each object individually")]
    [SerializeField] int scoreValue;
    [Tooltip("What is the tag of the collider this should be looking out for. Mostly here for in case we decide to give the tongue it's own hitbox")]
    [SerializeField] string tagNameToWatchFor = "Player";
    GameManager gm; //The GameManager script in this scene. There should only be one!

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// When the specified tagged object touches this, tell GameManager to update the score, then destroy this object. 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagNameToWatchFor))
        {
            gm.UpdateScore(scoreValue);
            Destroy(this);
        }
    }
}
