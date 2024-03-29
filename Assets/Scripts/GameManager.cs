﻿using BombSystem;
using CharacterSystem.Enemy;
using CharacterSystem.Player;
using GridSystem;
using InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UISystem;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Common
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] FloatingJoystick joyStick;
        [SerializeField] Button fireBomb;
        UIManager uiManager;
        MapManager mapManager;
        EnemyManager enemyManager;
        PlayerControllerView player;
        [SerializeField] Button playButon;
        [SerializeField] GameUIController gameUIController;
        [SerializeField]EnemyControllerView enemyPrefab;
        [SerializeField]PopUpController popUp;
        [SerializeField]PlayerControllerView playerPrefab;
        [SerializeField]InputControler inputController;
        [SerializeField]BombControllerView bomb;
        [SerializeField] TileBase destructableTile;
        [SerializeField]Tilemap gameGridMap;
        [SerializeField] GameObject explosion;
        BombManager bombManager;
        public event Action onGameReset;
        int score = 0;

        // Start is called before the first frame update
        public override void OnInitialize()
        {
            base.OnInitialize();
            mapManager = new MapManager(gameGridMap, destructableTile, explosion);
            bombManager = new BombManager(bomb, mapManager);
            enemyManager = new EnemyManager(enemyPrefab, mapManager);
            uiManager = new UIManager(gameUIController,popUp, playButon,joyStick,fireBomb);
        }
        public void Reset()
        {
            score = 0;
            UpdateScore(0);
            onGameReset.Invoke();
            StartPlaying();    
        }
        public void StartPlaying()
        {
            StartGame();
            SpawnPlayer();
        }

        void SpawnPlayer()
        {
            player = Instantiate(playerPrefab.gameObject, mapManager.GetSpawnPoint(), Quaternion.identity).GetComponent<PlayerControllerView>();
            player.SetBombManager(bombManager);
            inputController.SetUpInputController(player,joyStick,fireBomb);
        }
        public void StartGame()
        {
            uiManager.StartGameUI();
            mapManager.GenerateMap();
            enemyManager.SpawnEnemies(5);
        }
        public void GameOver(string message)
        {
            Debug.Log("<color=red>" + message + "</color>");
            uiManager.GameOver(message, score);
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
        public void UpdateScore(int points)
        {
             this.score+= points;
             uiManager.UpdateScore(score);
        }
        public MapManager GetMapManager()
        {
            return mapManager; 
        }

       
    }
}