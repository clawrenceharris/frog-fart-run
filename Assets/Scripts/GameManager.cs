/***
 * GameManager.cs
 * Version 1
 * By Nathan Boles
 * 
 * This script's job is to run all other side functions that keeps the game functioning. At this time, this includes
 * - Keeping track of the players current score
 * - Keeping track of the players number of deaths
 * - Restarting the level
 * 
 * This list will likely be expanded on as the game is iterated upon. 
 * 
 * 
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    int score; //How many points did the player earn
    int deaths; //How many times has the player died

    /// <summary>
    /// When called, updates how many points the player has earned, and displays that number on the UI.
    /// </summary>
    /// <param name="s">The number of points the player earns (or loses if negative)</param>
    public void UpdateScore(int s)
    {
        score += s;

    }

    /// <summary>
    /// When called, update just how many times the player has died. This may not actually be used, but I figured it would be more interesting 
    /// to have something keep track of this statistic. 
    /// </summary>
    /// <param name="d">Generally should be 1 when called, but keeping the door open to mess with this in the future.</param>
    public void UpdateDeaths(int d)
    {
        deaths += d;
    }

    /// <summary>
    /// Resets the current scene
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        //Does something
    }
}
