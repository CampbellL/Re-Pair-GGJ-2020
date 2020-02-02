﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Source.Animals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source
{
    [System.Serializable]
    public struct Rule
    {
        public string origin;
        public string[] targets;
    }
    
    [System.Serializable]
    public struct FailCombinations
    {
        public string type1;
        public string type2;
        public Sprite combinationSprite;
    }

    public class GameMode : MonoBehaviour
    {
        public static GameMode instance;
        public Rule[] rules;
        public FailCombinations[] failCombinations;
        public int Tries { get; private set; }

        private List<Animal> _animals = new List<Animal>();

        // Start is called before the first frame update
        void Start()
        {
            instance = this;

            Tries = 0;
            StartCoroutine(Test());
        }

        public void Play()
        {
            _animals.Clear();
            _animals = FindObjectsOfType<Animal>().ToList().FindAll(animal => !animal.isStatic);

            foreach (var animal in _animals)
                animal.StartMoving();
        }

        public void Evaluate()
        {
            _animals = FindObjectsOfType<Animal>().ToList().FindAll(animal => !animal.isMatched);

            if (_animals.Count > 0)
            {
                //failed
                Tries++;
                Debug.Log("Level Failed!");
            }
            else
            {
                //level completed
                Debug.Log("Level Complete!");
            }
        }

        public void ResetPuzzle()
        {
            Animal[] allAnimals = FindObjectsOfType<Animal>();

            foreach (var animal in allAnimals)
            {
                animal.ResetState();
            }
        }

        IEnumerator Test()
        {
            yield return new WaitForSecondsRealtime(10f);
            Evaluate();
        }
        
    }
}