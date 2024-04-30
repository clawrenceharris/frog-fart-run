/***
 * MushroomBounce.cs
 * Version 1
 * By Nathan Boles
 * 
 * This script is for environmental objects where, when the player contacts the collider, they will be sent upwards.
 * In hindsight this script probably should be named something far more versatile as this doesn't necessarily need to plugged onto a mushroom
 * 
 * 
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBounce : MonoBehaviour
{
    [Tooltip("This variable determines what the Y Velocity of the player object should be upon landing on this.")]
    [SerializeField] float jumpVelocity = 10;

    /// <summary>
    /// This collider checks to see if the player object came into contact with it. If they did, send the player upwards with a velocity decided
    /// by the variable "jumpVelocity" while not changing the x Velocity of the player at all.
    /// </summary>
    /// <param name="collision">An object that comes into contact with this collider</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.rigidbody.velocity = new Vector2(collision.rigidbody.velocity.x, jumpVelocity);
        }
    }
}
