using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Animals
{
    public class Animal : MonoBehaviour
    {
        [HideInInspector] public bool isStatic;
        [HideInInspector] public bool isMatched;
        [HideInInspector] public bool isImmovable;
        [HideInInspector] public bool playFinished;
        [HideInInspector] public GameObject connectedObject;
        public string animalType;

        public Sprite[] normalSprites;
        public Sprite[] happySprites;
        public Sprite[] sadSprites;

        protected float moveSpeed = 10f;
        protected SpriteRenderer sprRndr;
        protected Collider2D colliderRef;

        private List<string> _matchableAnimalTypes = new List<string>();
        private FailCombinations[] _failCombinations;
        private Vector2 _initialPosition;
        private bool _move = false;

        protected virtual void Start()
        {
            foreach (var rule in GameMode.instance.rules)
            {
                if (rule.origin == animalType)
                {
                    if (rule.targets.Length == 0)
                    {
                        isImmovable = true;
                        break;
                    }
                    
                    foreach (var type in rule.targets)
                    {
                        _matchableAnimalTypes.Add(type);
                    }

                    break;
                }
            }

            _failCombinations = GameMode.instance.failCombinationsSeason[GameMode.instance.season].failCombinations;
            _initialPosition = transform.position;

            sprRndr = GetComponent<SpriteRenderer>();
            colliderRef = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (_move && connectedObject && !isStatic)
            {
                var tarPos = connectedObject.transform.position;
                var position = transform.position;

                position = Vector2.MoveTowards(position, tarPos, moveSpeed * Time.deltaTime);

                var transform1 = transform;
                transform1.position = position;

                //orient towards target
                transform1.up = tarPos - transform1.position;
            }
        }

        public void StartMoving()
        {
            if(!isImmovable)
                _move = true;
            else
            {
                Debug.Log("Cannot move");
            }
        }

        public void ResetState()
        {
            //reset to values
            _move = false;
            isMatched = false;
            isStatic = false;
            playFinished = false;
            connectedObject = null;
            transform.position = _initialPosition;
            sprRndr.enabled = true;
            colliderRef.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Animal"))
            {
                if (!isStatic)
                {
                    Animal otherScript = other.gameObject.GetComponent<Animal>();
                    bool matchSuccess = false;

                    foreach (var matchableType in _matchableAnimalTypes)
                    {
                        if (otherScript.animalType == matchableType)
                        {
                            //success
                            matchSuccess = true;
                            Debug.Log("successfully matched!");
                            _move = false;
                            break;
                        }
                    }

                    sprRndr.enabled = false;
                    colliderRef.enabled = false;
                    otherScript.colliderRef.enabled = false;
                    playFinished = true;
                    otherScript.playFinished = true;

                    if (matchSuccess)
                    {
                        otherScript.sprRndr.sprite = happySprites[GameMode.instance.season];
                        otherScript.sprRndr.color = new Color(0, 0, 0, 0.3f);
                        other.transform.localScale *= 0.8f;
                        isMatched = true;
                        otherScript.isMatched = true;
                    }
                    else
                    {
                        foreach (var combination in _failCombinations)
                        {
                            if ((combination.type1 == otherScript.animalType || combination.type2 == otherScript.animalType)
                                && (combination.type1 == animalType || combination.type2 == animalType))
                            {
                                otherScript.sprRndr.sprite = combination.combinationSprite;
                                other.transform.localScale = new Vector3(2.1f, 2.1f, 2.1f);
                            }
                            else
                            {
                                //fail state
                                otherScript.sprRndr.sprite = sadSprites[GameMode.instance.season];
                            }
                        }
                    }
                }
            } 
            else if (other.CompareTag("Block"))
            {
                _move = false;
                playFinished = true;
                
                //fail state
                Animal otherScript = other.gameObject.GetComponent<Animal>();
                sprRndr.enabled = false;
                colliderRef.enabled = false;
                otherScript.colliderRef.enabled = false;
                
                otherScript.sprRndr.sprite = sadSprites[GameMode.instance.season];
            }
        }
    }
}