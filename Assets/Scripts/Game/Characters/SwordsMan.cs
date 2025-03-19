using System;
using Game.Characters.Attacks;
using Game.Characters.Movements;
using Game.Characters.States;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters
{
    public class SwordsMan : MonoBehaviour, ICharacter
    {
        [SerializeField]
        NavMeshAgent agent;
        
        IAttack attack;
        IMovement movement;
        
        IState currentState;

        public IState State => currentState;
        
        // void Init()
        void Awake()
        {           
            // currentState = new MoveState(new WalkMovement(aget));
            movement = new WalkMovement(agent);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                movement.MoveToTarget(new Vector3(target.x, target.y, transform.position.z));
            }
        }
    }
}