using BombSystem;
using CharacterSystem.Enemy;
using CharacterSystem.Player;
using GridSystem;
using InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace Common
{
    public class GameManager : Singleton<GameManager>
    {
        MapManager mapManager;
        EnemyManager enemyManager;
        PlayerControllerView player;
        [SerializeField]EnemyControllerView enemyPrefab;
        [SerializeField]PlayerControllerView playerPrefab;
        [SerializeField]InputControler inputController;
        [SerializeField]BombControllerView bomb;
        [SerializeField] TileBase destructableTile;
        [SerializeField]Tilemap gameGridMap;
        [SerializeField] GameObject explosion;
        BombManager bombManager;

        // Start is called before the first frame update
        public override void OnInitialize()
        {
            base.OnInitialize();
            mapManager = new MapManager(gameGridMap, destructableTile,explosion);
            bombManager = new BombManager(bomb,mapManager);
            enemyManager = new EnemyManager(enemyPrefab,mapManager);
            mapManager.GenerateMap();
            enemyManager.SpawnEnemies(5);
        }
        public void GameOver(string message)
        {
            Debug.Log("<color=red>" + message + "</color>");
        }
        public void Start()
        {
            player = Instantiate(playerPrefab.gameObject, mapManager.GetSpawnPoint(), Quaternion.identity).GetComponent<PlayerControllerView>();
            player.SetBombManager(bombManager);
            inputController.SetPlayer(player);
            
        }
        public PlayerControllerView GetPlayer()
        {
            return player;
        }
        public BombManager GetBombManager()
        {
            Debug.Log("Getting bomb manager");
            return bombManager;
        }
        public MapManager GetMapManager()
        {
            return mapManager; 
        }

       
    }
}