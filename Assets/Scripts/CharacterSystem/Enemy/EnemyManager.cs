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
        GameManager gameManager;
        List<EnemyControllerView> enemies = new List<EnemyControllerView>();
        public EnemyManager(EnemyControllerView enemyPrefab,MapManager mapManager)
        {
            this.mapManager = mapManager;
            this.enemyPrefab = enemyPrefab;
            gameManager=GameManager.Instance;
            gameManager.onGameReset += Reset;
        }
        public void SpawnEnemies(int numberOfEnemies)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                SpawnEnemy();
            }
        }
        public void Reset()
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                enemies[i].DestroyEnemy();
            }
            enemies.Clear();
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
            gameManager.UpdateScore(5);
            if (enemies.Count == 0)
            {
                gameManager.GameOver("You Won");
            }
        }

    }
}