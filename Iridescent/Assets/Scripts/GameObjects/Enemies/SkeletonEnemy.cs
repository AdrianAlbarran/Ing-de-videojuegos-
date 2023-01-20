using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Enemies
{
    public class SkeletonEnemy : Enemy
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
