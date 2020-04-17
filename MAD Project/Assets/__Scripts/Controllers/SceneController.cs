using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities;    // scene names

// sceneManageent library - load, unload scenes
using UnityEngine.SceneManagement;



public class SceneController : MonoBehaviour
{
    // == onclick Events ==
    public void Start_OnClick()
    {
        SceneManager.LoadSceneAsync(SceneNames.GAME_LEVEL);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync(SceneNames.MAIN_MENU);
    }


}
