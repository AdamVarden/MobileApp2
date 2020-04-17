using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// use this to manage collisions

public class Grunt : MonoBehaviour
{
    // == set this up to publish an event to the system
    // == when killed
    // Visuals for a grunt death
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private float explosionDuration = 1.0f;
   
    [SerializeField] private AudioClip crashSound;
    // sounds for getting hit the weapon, spawning
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip spawnSound;
    // == public fields ==
    // used from GameController Grunt.ScoreValue
    public int ScoreValue { get { return gruntValue; } }
    
    // used in PLayerHealth
    public int DamageValue { get { return damageValue; } }

    // delegate type to use for event
    public delegate void GruntKilled(Grunt grunt);

    // create static method to be implemented in the listener
    public static GruntKilled GruntKilledEvent;

    

    // == private fields ==
    // The grunts values
    [SerializeField] public int gruntValue = 5;
    [SerializeField] private int damageValue = 10;
    // The sounds
    private SoundController sc;

    private void Start()
    {
        sc = SoundController.FindSoundController();
        PlaySound(spawnSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (sc)
        {
            sc.PlayOneShot(clip);
        }
    }

    // == private methods == Tracking collisions
    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        // parameter = what ran into me
        // if the player hit, then destroy the player
        // and the current grunt 

        //References Variables
        var player = whatHitMe.GetComponent<PlayerMovement>();
        var weapon = whatHitMe.GetComponent<Weapon>();

        //When colliding with the player
        if(player)  // if (player != null)
        {
            Debug.Log("It was the player");
            PlaySound(crashSound);
            //Destroys the grunt
            Destroy(gameObject);
        }

        //When hit by the weapon
        if(weapon)
        {
            // play die sound
            PlaySound(dieSound);
            // destroy bulllet
            Destroy(weapon.gameObject);
            // publish the event to the system to give the player points
            PublishGruntKilledEvent();
            // show the explosion
            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
            // destroy this game object
            Destroy(gameObject);            
            Destroy(explosion, explosionDuration);

        }
    }
    // publishing the killed grunt
    private void PublishGruntKilledEvent()
    {
        // make sure somebody is listening
        if(GruntKilledEvent != null)   
        {
            GruntKilledEvent(this);
        }
    }
}
