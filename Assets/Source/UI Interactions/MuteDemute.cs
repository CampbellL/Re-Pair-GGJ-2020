using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteDemute : MonoBehaviour
{
    public GameObject MuteButton;
    public GameObject OnButton;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeSoundStatus()
    {
        if(MuteButton.active == false)
        {
            MuteButton.SetActive(true);
            OnButton.SetActive(false);
        }
        else if(MuteButton.active == true)
        {
            MuteButton.SetActive(false);
            OnButton.SetActive(true);
        }
       
    }
}
