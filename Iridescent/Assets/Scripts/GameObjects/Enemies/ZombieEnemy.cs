using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameObjects;

    namespace Assets.Scripts.GameObjects.Enemies
{
    public class ZombieEnemy : Enemy
    {       
        private void Start()
        {

            base.Start();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            controlMoveAnimation(animator);
        }
    }
}