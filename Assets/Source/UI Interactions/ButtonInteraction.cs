using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SeasonScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //PressStart();
    }

    public void PressStart()
    {
        MainMenu.SetActive(false);
        SeasonScreen.SetActive(true);
    }
}
