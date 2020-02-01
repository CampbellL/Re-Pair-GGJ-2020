using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractionResume : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SeasonLevelScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resume()
    {
        MainMenu.SetActive(false);
        SeasonLevelScreen.SetActive(true);
    }
}
