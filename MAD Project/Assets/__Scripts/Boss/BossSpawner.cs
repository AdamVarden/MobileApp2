using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private BossBehaviour princePrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Calls the method
        StartCoroutine(BossSpawn());
    }

    // For Spawinin the bosses
    IEnumerator BossSpawn()
    {
        while(true)
        {
            // Waits a period of time before and after the boss spawn
            yield return new WaitForSeconds(10f);
            Instantiate(princePrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(20f);

        }
    }
}
