using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHome : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject actualScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickHome()
    {
        MainMenu.SetActive(true);
        actualScene.SetActive(false);
    }
}
