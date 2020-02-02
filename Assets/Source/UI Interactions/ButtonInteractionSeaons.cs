using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractionSeaons : MonoBehaviour
{
    public GameObject SeasonSelectionScreen;
    public GameObject LevelSelectionWinter;
    public GameObject LevelSelectionSummer;
    public GameObject LevelSelectionSpring;
    public GameObject LevelSelectionAutumn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeAutumn()
    {
        SeasonSelectionScreen.SetActive(false);
        LevelSelectionAutumn.SetActive(true);

    }

    public void changeSummer()
    {
        SeasonSelectionScreen.SetActive(false);
        LevelSelectionSummer.SetActive(true);
    }

    public void changeSpring()
    {
        SeasonSelectionScreen.SetActive(false);
        LevelSelectionSpring.SetActive(true);
    }

    public void changeWinter()
    {
        SeasonSelectionScreen.SetActive(false);
        LevelSelectionWinter.SetActive(true);
    }
    
}
