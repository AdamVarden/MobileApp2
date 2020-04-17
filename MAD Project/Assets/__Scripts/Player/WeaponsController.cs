using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    // == private fields ==
    [SerializeField] private float weaponSpeed = 6.0f;
    [SerializeField] private Weapon weaponPrefab;
    [SerializeField] private float firingInterval = 0.1f;

    private GameObject weaponParent;

    private Coroutine firingCoroutine;  // pointer to the coroutine
                                        // need this to stop firing

    // == private methods
    private void Start()
    {
        weaponParent = GameObject.Find("WeaponParent");
        if (!weaponParent)
        {
            weaponParent = new GameObject("WeaponParent");
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.S))
        {
            // setup a co routine to fire the weapons - start firing
            firingCoroutine = StartCoroutine(FireCoroutine());
            //Fireweapon();
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.S))
        {
            // stop firing
            StopCoroutine(firingCoroutine);
        }
    }

    // coroutine must be of type IEnumerator
    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            // generate weapons
            Weapon weapon = Instantiate(weaponPrefab, weaponParent.transform);
            weapon.transform.position = transform.position;
            Rigidbody2D rbb = weapon.GetComponent<Rigidbody2D>();
            rbb.velocity = Vector2.right * weaponSpeed;

            // the yield return, causes the method to pause
            // sleep()
            yield return new WaitForSeconds(firingInterval);
        }
    }

    private void FireWeapon()
    {
        // instantiate weapon
        Weapon weapon = Instantiate(weaponPrefab, weaponParent.transform);
        // give it the same position as the player
        weapon.transform.position = transform.position;
        // give it velocity and move right
        Rigidbody2D rbb = weapon.GetComponent<Rigidbody2D>();

        //rbb.velocity = new Vector2(1 * speed, 0);
        rbb.velocity = Vector2.right * weaponSpeed;
        rbb.velocity = Vector2.right * weaponSpeed;
        
    }
}
