using Assets.Scripts.GameObjects.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.GameObjects
{
    public class EnemySpawner : MonoBehaviour
    {
        private DemonEnemy enemigo;
        private int enemiesAlive;
        private int spawnRate = 5;
        // Start is called before the first frame update
        void Start()
        {
            enemiesAlive = 4;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator spawnEnemy()
        {
            enemigo = new DemonEnemy();
            yield return new WaitForSeconds(spawnRate);
        }
    }
}