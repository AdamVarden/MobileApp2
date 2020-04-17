using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// add this for scene management library
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // handle button events
    // Start button, Options button
    // use SceneManager to load and unload scenes
    // all methods are static, subscribe to events
    // like SceneLoaded, SceneUnloaded, SceneChanged for custom logic,
    // use LoadSceneAsync to load a new scene

    // SceneManager is in UnityEngine.SceneManagement library

    // == OnClick Event Handlers ==
    public void Start_OnClick()
    {
        SceneManager.LoadSceneAsync("Game Scene");
    }


}
