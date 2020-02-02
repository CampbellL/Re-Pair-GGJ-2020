using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAnimal : MonoBehaviour
{
    public Sprite[] normalSprites;
    public Sprite[] happySprites;
    public Sprite[] sadSprites;

    public int season;
    // Start is called before the first frame update
    void Start()
    {
        SetSpritesForSeason();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpritesForSeason()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = normalSprites[season];
    }
}
