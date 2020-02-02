using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public int tries;
    private Animal[] animals;
    
    // Start is called before the first frame update
    void Start()
    {
        animals = FindObjectsOfType<Animal>().Where();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Play()
    {
        
    }
}
