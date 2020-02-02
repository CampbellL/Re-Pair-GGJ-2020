using System.Collections.Generic;
using System.Linq;
using Source.Animals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source
{
    public class GameMode : MonoBehaviour
    {
        public static GameMode instance;
        public int Tries { get; private set; }

        private List<Animal> _animals;

        // Start is called before the first frame update
        void Start()
        {
            instance = this;
            //DontDestroyOnLoad(this);

            Tries = 0;
            _animals = FindObjectsOfType<Animal>().ToList().Where((animal) => !animal.isStatic) as List<Animal>;
        }

        public void Play()
        {
            foreach (var animal in _animals)
                animal.StartMoving();
        }

        public void ResetPuzzle()
        {
            Tries++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}