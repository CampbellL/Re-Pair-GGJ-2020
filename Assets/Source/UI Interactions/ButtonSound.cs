using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
        Button b = GetComponent<Button>();
        AudioSource audio = Camera.main.GetComponent<AudioSource>();        b.onClick.AddListener(delegate () {
            audio.Play();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
