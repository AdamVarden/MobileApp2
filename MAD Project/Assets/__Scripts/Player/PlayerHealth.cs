using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    // set up an initial health value,
    // set an amount of damage per grunt - start with 5
    [SerializeField] private int playerStartHealth = 100;
    [SerializeField] private TextMeshProUGUI livesText;

    private int playerCurrentHealth;

    private GameController gc;
    private Vector3 startPosition;

    private SpriteRenderer sr;
    private PolygonCollider2D pc2d;
    private WeaponsController wc;
    private PlayerMovement pm;
    private ParticleSystem ps;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        pc2d = GetComponent<PolygonCollider2D>();
        wc = GetComponent<WeaponsController>();
        pm = GetComponent<PlayerMovement>();
        ps = GetComponentInChildren<ParticleSystem>();

        playerCurrentHealth = playerStartHealth;
        gc = FindObjectOfType<GameController>();
        startPosition = new Vector3(transform.position.x,
                                     transform.position.y,
                                     transform.position.z);
    }

    // use the triggerEnter method to see if it gets hit by grunt
    [Obsolete]
    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        //When hit by the grunt it reduces health
        var grunt = whatHitMe.GetComponent<Grunt>();
        if(grunt)
        {
            playerCurrentHealth -= grunt.DamageValue;
            Debug.Log($"Player Health From Grunt: = {playerCurrentHealth}");
            livesText.text = playerCurrentHealth.ToString();

        }

        //When hit by a the orb the boss uses to attack it reduces the health
        var boss = whatHitMe.GetComponent<BossAttack>();
        if(boss)
        {
            playerCurrentHealth -= boss.DamageValue;
            Debug.Log($"Player Health From Boss: = {playerCurrentHealth}");
            livesText.text = playerCurrentHealth.ToString();

        }
        if(playerCurrentHealth <= 0)
        {
            // player should die
            Die();
            //Once dead the user is brought back to the main menu
            Application.LoadLevelAdditive(1);
        }
    }

    private void Die()
    {
        // need to stop the player interacting - disable weapons
        // make the ship disappear
        // need to play an explosion
        // hide the object, then make it reappear at the start
        // going to take a little time
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        // disable components - makes the player disappear
        DisableComponents();

        // tell the game controller lost one life
        gc.LoseOneLife();

        yield return new WaitForSeconds(1.5f);
        if(gc.RemainingLives > 0)
        {
            Respawn();
        }
    }

    private void DisableComponents()
    {
        // SpriteREnderer, PolygonCollider2d, Weaspons, Movement
        SetComponentsEnabled(false);
    }

    private void EnableComponents()
    {
        SetComponentsEnabled(true);
    }

    private void SetComponentsEnabled(bool status)
    {
        sr.enabled = status;
        pc2d.enabled = status;
        wc.enabled = status;
        pm.enabled = status;
        if(status == true)
        {
            ps.Play();
        }
        else
        {
            ps.Stop();
        }
        
    }

    private void Respawn()
    {
        // set the player back to the start position
        // reset the player health
        // re-enable all the components to make the player visible.
        transform.position = startPosition;
        playerCurrentHealth = playerStartHealth;
        EnableComponents();
    }
}
