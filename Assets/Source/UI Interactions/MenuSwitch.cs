using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSwitch : MonoBehaviour
{
    private Button ButtonMenu;
    private int SpriteVersion;
    public Sprite[] sprites;
    public GameObject Home;
    public GameObject Sound;
    public GameObject Pet;

    // Start is called before the first frame update
    void Start()
    {
        ButtonMenu= gameObject.GetComponent<Button>();
        Home.SetActive(false);
        Sound.SetActive(false);
        Pet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchMenu()
    {
        
        if (SpriteVersion == 0)
        {
            SpriteVersion = 1;
            Home.SetActive(true);
            Sound.SetActive(true);
            Pet.SetActive(true);

            
        }
         else if(SpriteVersion == 1)
        {
           SpriteVersion = 0;
            Home.SetActive(false);
            Sound.SetActive(false);
            Pet.SetActive(false);
        }

        ButtonMenu.image.sprite = sprites[SpriteVersion];
    }
}
