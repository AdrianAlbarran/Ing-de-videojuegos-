using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public interface IEnemy
    {
        public bool IsAlive();
        public string GetArmy();
        public void Create();
        public void Move(float deltaTime);
        public void ProcessCollissions(IEnemy other);
        public void Render();
  
}
