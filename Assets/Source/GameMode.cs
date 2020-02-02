using System;
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

    [System.Serializable]
    public struct FailCombinationsSeason
    {
        public int season;
        public FailCombinations[] failCombinations;
    }

    public class GameMode : MonoBehaviour
    {
        public static GameMode instance;
        public int season;
        public Rule[] rules;
        public FailCombinationsSeason[] failCombinationsSeason;
        public int Tries { get; private set; }

        private List<Animal> _animals = new List<Animal>();
        private bool _playStarted;

        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            Tries = 0;
        }

        private void Update()
        {
            if (_playStarted)
            {
                _animals.Clear();
                _animals = FindObjectsOfType<Animal>().ToList();

                bool playFinished = true;

                foreach (var animal in _animals)
                {
                    if (!animal.playFinished)
                    {
                        playFinished = false;
                        break;
                    }
                }

                if (playFinished)
                {
                    _playStarted = false;
                    Evaluate();
                }
            }
        }

        public void Play()
        {
            _animals.Clear();
            _animals = FindObjectsOfType<Animal>().ToList().FindAll(animal => !animal.isStatic);

            foreach (var animal in _animals)
                animal.StartMoving();

            _playStarted = true;
        }

        public void Evaluate()
        {
            _animals.Clear();
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
                SceneManager.LoadScene("MainScene");
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
    }
}