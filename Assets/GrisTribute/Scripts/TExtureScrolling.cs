using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TExtureScrolling : MonoBehaviour
{
    public float scrollSpeedX;
    public float scrollSpeedY;
    float offsetX;
    float offsetY;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        offsetX = Time.time * scrollSpeedX;
        offsetY = Time.time * scrollSpeedY;
        rend.material.mainTextureOffset = new Vector2 (offsetX, offsetY);
    }
}
