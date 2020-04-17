using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public delegate void BossKilled(BossBehaviour boss);

    // create static method to be implemented in the listener
    public static BossKilled BossKilledEvent;

    //Score value for the killed boss
    public int ScoreValue { get { return bossValue; } }
    
    //The object that will be used to attack the player
    [SerializeField] GameObject attack;
    //The boss value when killed
    [SerializeField] public int bossValue = 10;
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private AudioClip dieSound;
    private float explosionDuration = 1.0f;
    //The bosses health
    int health = 100;

    private SoundController sc;
    //The boss Attacking
    float fireRate;
    float nextfire;

    void Start()
    {
        //The rate of its firing
        fireRate = 0.5f;
        nextfire = Time.time;

    }

  private void PlaySound(AudioClip clip)
    {
        if (sc)
        {
            sc.PlayOneShot(clip);
        }
    }

    void Update()
    {
        CheckIfTimeToFire();
    }

    // To detect when the weapon collides and hurts the boss
    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        //Reference Variable
        var weapon = whatHitMe.GetComponent<Weapon>();

        //When the weapon hits
        if(weapon)
        {
            //Health is reduced
            health = health - 10;

            //When the health hits zero it is killed
            if(health == 0)
            {
                // play die sound
                PlaySound(dieSound);
                // destroy bulllet
                Destroy(weapon.gameObject);
                // publish the event to the system to give the player points
                 PublishBossKilledEvent();
                 // show the explosion
                GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
                // destroy this game object
                Destroy(gameObject);            
                Destroy(explosion, explosionDuration);
            }
            //The boss health
            Debug.Log("Health of boss: " + health);
        }
    }

    //Publishing the boss being killed
    private void PublishBossKilledEvent()
    {
        // make sure somebody is listening
        if(BossKilledEvent != null)   
        {
            BossKilledEvent(this);
        }
    }
    // Firing Method
    void CheckIfTimeToFire()
    {   
        // If its time to fire it will fire the orb at the player
        if(Time.time > nextfire)
        {
            Instantiate(attack,transform.position, Quaternion.identity);
            nextfire = Time.time + fireRate;
        }
    }
}
