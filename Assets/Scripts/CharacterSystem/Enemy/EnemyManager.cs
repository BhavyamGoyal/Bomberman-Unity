using Common;
using GridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CharacterSystem.Enemy
{
    public class EnemyManager
    {
        EnemyControllerView enemyPrefab;
        MapManager mapManager;
        List<EnemyControllerView> enemies = new List<EnemyControllerView>();
        public EnemyManager(EnemyControllerView enemyPrefab,MapManager mapManager)
        {
            this.mapManager = mapManager;
            this.enemyPrefab = enemyPrefab;
        }
        public void SpawnEnemies(int numberOfEnemies)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                SpawnEnemy();
            }
        }
        public void SpawnEnemy()
        {
            EnemyControllerView enemy = GameObject.Instantiate(enemyPrefab.gameObject, mapManager.GetRandomSpawnPosition(), Quaternion.identity).GetComponent<EnemyControllerView>();
            enemy.SetManager(this,mapManager);
            enemies.Add(enemy);
        }
        public void DestroyEnemy(EnemyControllerView enemy)
        {
            enemies.Remove(enemy);
            if (enemies.Count == 0)
            {
                GameManager.Instance.GameOver("You Won");
            }
        }

    }
}