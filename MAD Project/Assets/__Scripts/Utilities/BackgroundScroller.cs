using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    // == private fields ==
    [SerializeField] private float scrollSpeed = 0.5f;
    private Vector2 offset;
    private Material myMaterial;

    void Start()
    {
        // get the material, set the offset vector
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(scrollSpeed, 0f);  // move to left
    }

    void Update()
    {
        // increment the offset for the material
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
