using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BoundaryCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        //For destroying grunts
        var grunt = collision.GetComponent<Grunt>();
        if(grunt)
        {
            Destroy(grunt.gameObject);
        }
        //For destroying weapons
        var weapon = collision.GetComponent<Weapon>();
        if(weapon)
        {
            Destroy(weapon.gameObject);
        }
    }
}
