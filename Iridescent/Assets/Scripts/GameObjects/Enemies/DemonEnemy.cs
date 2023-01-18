using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Enemies
{
    public class DemonEnemy : Enemy
    {  
        private void Start()
        {

            base.Start();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hp = 0;
            }
        }

    }
}
