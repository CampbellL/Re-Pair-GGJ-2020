using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitch : MonoBehaviour
{
    private SpriteRenderer WinterButtonMenu;
    private int SpriteVersion = 1;
    private Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        WinterButtonMenu= gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("Sprites/winterMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchMenu()
    {
        SpriteVersion += 1; 
        if(SpriteVersion == 1)
        {
            SpriteVersion = 0;
        }
        else if(SpriteVersion == 0)
        {
            SpriteVersion = 1;
        }

        WinterButtonMenu.sprite = sprites[SpriteVersion];
    }
}
