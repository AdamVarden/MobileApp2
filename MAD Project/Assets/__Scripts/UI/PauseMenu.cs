using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject Pausemenu, Pausebutton;

    //When the pause button is pressed
    public void Pause()
    {
        // Actives the paused menu that is inactive in the hierarchy
        Pausemenu.SetActive (true);
        // Disables the pause button
        Pausebutton.SetActive (false);
        //Sets the game time to zero which pauses
        Time.timeScale = 0;
    }

    //When the resume button is pressed
    public void Resume()
    {
        //The menu is then disabled
        Pausemenu.SetActive (false);
        //The pause button is activated again
        Pausebutton.SetActive (true);
        //Time is resumed
        Time.timeScale = 1;
    }
}
