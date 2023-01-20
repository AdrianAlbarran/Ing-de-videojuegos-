using Assets.Scripts.GameObjects.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.GameObjects
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefabDemon;
        public GameObject enemyPrefabJacko;
        public GameObject enemyPrefabSkele;
        public GameObject enemyPrefabZombie;
        public int enemiesAlive;
        public float spawnRate = 5f;
        private Vector3 spawnPoint;
        private Vector3 spawnPoint1 = new Vector3(-1, -24, 0);
        private Vector3 spawnPoint2 = new Vector3(20, -24, 0);
        private bool checkSpawn;
        // Start is called before the first frame update
        void Start()
        {
            enemiesAlive = 0;
            enemyPrefabDemon.transform.position = spawnPoint;
        }

        // Update is called once per frame
        void Update()
        {
            
            if (enemiesAlive < 10 && !checkSpawn)
            {
                int random = Random.Range(0, 2);
                if (random == 0) spawnPoint = spawnPoint2;
                else spawnPoint = spawnPoint1;
                StartCoroutine(spawnEnemy());
                spawnRate *= 0.95f;
            }
        }

        private IEnumerator spawnEnemy()
        {
            checkSpawn = true;
            int random = Random.Range(0, 4);
            switch (random)
            {
                case 0:
                    Instantiate(enemyPrefabDemon, transform).transform.position = (spawnPoint);
                    enemiesAlive++;
                    break;
                case 1:
                    Instantiate(enemyPrefabJacko, transform).transform.position = (spawnPoint);
                    enemiesAlive++;
                    break;
                case 2:
                    Instantiate(enemyPrefabSkele, transform).transform.position = (spawnPoint);
                    enemiesAlive++;
                    break;
                case 3:
                    Instantiate(enemyPrefabZombie, transform).transform.position = (spawnPoint);
                    enemiesAlive++;
                    break;
            }
            
            yield return new WaitForSeconds(spawnRate);
            checkSpawn = false;
        }
    }
}