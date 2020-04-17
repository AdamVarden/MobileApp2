using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities;

//        InvokeRepeating(SPAWN_GRUNT_METHOD, spawnDelay, spawnInterval);

public class PointSpawners : MonoBehaviour
{
    // == private fields ==
    //For spawining grunts
    [SerializeField] private Grunt gruntPrefab;
    [SerializeField] private float spawnDelay = 0.25f;
    [SerializeField] private float spawnInterval = 0.35f;

    private const string SPAWN_GRUNT_METHOD = "SpawnOneGrunt";
    private IList<SpawnPoint> spawnPoints;
    private Stack<SpawnPoint> spawnStack;
    private GameObject gruntParent;

    // events for telling the system grunt spawned
    public delegate void GruntSpawned();
    public static event GruntSpawned GruntSpawnedEvent;

    //private ListUtils listUtils = new ListUtils();

    // == private methods ==
    private void Start()
    {
        gruntParent = GameObject.Find("GruntParent");
        if(!gruntParent)
        {
            gruntParent = new GameObject("GruntParent");
        }
        // get the spawn points here
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        //SpawnGruntWaves();
        // create the stack of points
        spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        EnableSpawning();

    }

    private void SpawnGruntWaves()
    {
        // create the stack of points
        spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        InvokeRepeating(SPAWN_GRUNT_METHOD, spawnDelay, spawnInterval);
    }

    // stack version
    private void SpawnOneGrunt()
    {
        if(spawnStack.Count == 0)
        {
            spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        }
        var grunt = Instantiate(gruntPrefab, gruntParent.transform);
        var sp = spawnStack.Pop();
        grunt.transform.position = sp.transform.position;
        PublishOnGruntSpawnedEvent();   // tell the system
    }

    // create my event to publish the fact that enemt spawned
    public void PublishOnGruntSpawnedEvent()
    {
        GruntSpawnedEvent?.Invoke();
    }

    // == public methods ==

    public void EnableSpawning()
    {
        InvokeRepeating(SPAWN_GRUNT_METHOD, spawnDelay, spawnInterval);
    }

    public void DisableSpawning()
    {
        CancelInvoke(SPAWN_GRUNT_METHOD);
    }




}
