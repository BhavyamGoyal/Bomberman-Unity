using GridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BombSystem
{
    public class BombManager
    {
        BombControllerView bombInGame;
        MapManager mapManager;
        public BombManager(BombControllerView bomb, MapManager mapManager)
        {
            this.mapManager = mapManager;
            bombInGame = GameObject.Instantiate(bomb.gameObject).GetComponent<BombControllerView>();
            bombInGame.SetBombManager(this);
            bombInGame.gameObject.SetActive(false);
        }
        public bool isBombPresent()
        {
            return bombInGame.gameObject.activeSelf;
        }
        public void LaunchBomb(Vector3 bombpos)
        {
            Vector3 bombPos = mapManager.GetCellPos(bombpos);
            if (!isBombPresent())
            {
                Debug.Log("[BombManager] bomb Launched");
                bombInGame.gameObject.SetActive(true);
                bombInGame.SetCell(bombPos);
                bombInGame.ActivateBomb();
            }
        }
        public void BombExploded()
        {
            mapManager.BombExploded(bombInGame.gameObject.transform.position);
        }
    }
}