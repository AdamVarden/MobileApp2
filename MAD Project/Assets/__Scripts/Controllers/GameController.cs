using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
     // == public fields ==
    //Returning the starting lives 
    public int StartingLives { get { return startingLives; } }
    // Returining the lives remaining
    public int RemainingLives { get { return remainingLives; } }

    // == private fields ==
    private int playerScore = 0;
    //To display the score
    [SerializeField] private TextMeshProUGUI scoreText;
    //Starting lives
    [SerializeField] private int startingLives = 3;
    private int remainingLives = 0;

    // for the grunt waves
    [SerializeField] private int gruntCountPerWave = 20;
    [SerializeField] private TextMeshProUGUI remainingGruntText;
    private int remainingGruntCount;
    private int waveNumber = 1;
    // audio sound to indicate "between wave" moment
    [SerializeField] private AudioClip waveReadySound;
    private SoundController sc;

    // == private methods ==
    private void Awake()
    {
        SetupSingleton();
    }
    private void Start()
    {
        UpdateScore();
        remainingLives = startingLives;
        // set up for grunt waves
        gruntCountPerWave = 10;
        remainingGruntCount = gruntCountPerWave;
        sc = SoundController.FindSoundController();
    }

    private void SetupSingleton()
    {
        // this method gets called on creation
        // check for any other objects of the same type
        // if there is one, then use that one.
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);    // destroy the current object
        }
        else
        {
            // keep the new one
            DontDestroyOnLoad(gameObject);  // persist across scenes
        }
    }

    private void OnEnable()
    {
        Grunt.GruntKilledEvent += OnGruntKilledEvent;
        PointSpawners.GruntSpawnedEvent += PointSpawners_OnGruntSpawnedEvent;
    }

    private void OnDisable()
    {
        Grunt.GruntKilledEvent -= OnGruntKilledEvent;
        PointSpawners.GruntSpawnedEvent -= PointSpawners_OnGruntSpawnedEvent;
    }

    private void PointSpawners_OnGruntSpawnedEvent()
    {
        remainingGruntCount--;
        UpdateGruntRemainingText();
        if(remainingGruntCount == 0)
        {
            // stop the point spawner from spawning
            DisableSpawning();
            // start the next wave using a coroutine (wait for 5 seconds)
            StartCoroutine(SetupNextWave());
        }
    }

    private IEnumerator SetupNextWave()
    {
        yield return new WaitForSeconds(5.0f);
        sc.PlayOneShot(waveReadySound);
        waveNumber++;   // not displayed
        gruntCountPerWave = gruntCountPerWave + 5;
        remainingGruntCount = gruntCountPerWave;
        EnableSpawning();
    }

    private void DisableSpawning()
    {
        // find each PointSpawner, call a public method to disable spawning
        foreach(var spawner in FindObjectsOfType<PointSpawners>())
        {
            spawner.DisableSpawning();
        }
    }

    private void EnableSpawning()
    {
        // find each PointSpawner, call a public method to disable spawning
        foreach (var spawner in FindObjectsOfType<PointSpawners>())
        {
            spawner.EnableSpawning();
        }
    }

    private void OnGruntKilledEvent(Grunt grunt)
    {
        // add the score value for the grunt to the player score
        playerScore += grunt.ScoreValue;
        UpdateScore();
    }

    private void UpdateGruntRemainingText()
    {
        //remainingGruntText.text = remainingGruntCount.ToString("000");
    }

    private void UpdateScore()
    {
        //Debug.Log("Score: " + playerScore);
        // display on screen
        scoreText.text = playerScore.ToString();
    }

    public void LoseOneLife()
    {
        remainingLives--;
    }

}
