using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Enemies
{
    public class JackoEnemy : Enemy
    {
       
        private void Start()
        {

            base.Start();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                hp = 0;
            }
        }
    }
}
