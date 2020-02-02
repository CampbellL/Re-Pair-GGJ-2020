using System;
using UnityEngine;

namespace Source.Animals
{
    public class Animal : MonoBehaviour
    {
        [HideInInspector] public bool isStatic;
        [HideInInspector] public GameObject connectedObject;

        public float moveSpeed;

        private bool _move = false;

        private void Update()
        {
            if (_move && connectedObject)
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
            _move = true;
        }

        public void ResetAnimal()
        {
            
        }
    }
}
