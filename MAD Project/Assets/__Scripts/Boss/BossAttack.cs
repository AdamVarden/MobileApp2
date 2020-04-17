using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    // This is referring to the Orb prefab for the boss to attack it aims at the direction of the player
    float moveSpeed = 7f;

    Rigidbody2D rb;

    //Targets the player
    PlayerMovement target;  
    Vector2 moveDirection;
    // It damage value
    [SerializeField] private int damageValue = 15;
    [SerializeField] private AudioClip crashSound;

    // used in Player Health
    public int DamageValue { get { return damageValue; } }
    private SoundController sc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerMovement>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2 (moveDirection.x,moveDirection.y);
        Destroy (gameObject,3f);
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        // To recognize the player when colliding
        if (col.gameObject.name.Equals("Player"))
        {
            Debug.Log("Hit");
            PlaySound(crashSound);
            Destroy(gameObject);
        }
    }

    //Music
    private void PlaySound(AudioClip clip)
    {
        if (sc)
        {
            sc.PlayOneShot(clip);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
