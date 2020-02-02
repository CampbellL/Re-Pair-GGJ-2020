using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractionQuit : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject Autumn;
    public GameObject Summer;
    public GameObject Spring;
    public GameObject Winter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void quitSummer()
    {
        Summer.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void quitAutumn()
    {
        Autumn.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void quitWinter()
    {
        Winter.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void quitSpring()
    {
        Spring.SetActive(false);
        MainMenu.SetActive(true);
    }
}
