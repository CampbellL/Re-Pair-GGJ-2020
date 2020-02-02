using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractionLevelselection : MonoBehaviour
{
    public GameObject SeasonLevelSelection;
    public GameObject SeasonLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        SeasonLevelSelection.SetActive(false);
        SeasonLevel.SetActive(true);
    }
}
