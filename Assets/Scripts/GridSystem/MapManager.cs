using Common;
using InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GridSystem
{
    public class MapManager
    {
        Tilemap gameMap;
        TileBase destructableTile;
        MapGenerator generator;
        GameObject explosion;
        public MapManager(Tilemap gameMap, TileBase destructableTile, GameObject explosion)
        {
            this.explosion = explosion;
            this.destructableTile = destructableTile;
            this.gameMap = gameMap;
            generator = new MapGenerator(this);
            GameManager.Instance.onGameReset += Reset;
            Debug.Log("[MapManager]Manager Created");
        }
        public Vector3 GetSpawnPoint()
        {
            gameMap.SetTile(new Vector3Int(-10, 6, 0), null);
            gameMap.SetTile(new Vector3Int(-9, 6, 0), null);
            gameMap.SetTile(new Vector3Int(-10, 5, 0), null);
            Vector3Int cellPos = gameMap.WorldToCell(new Vector3(-10, 6, 0));
            return gameMap.GetCellCenterLocal(cellPos);
        }
        public List<Vector3> GetNearbyEmptyCells(Vector3 pos)
        {
            List<Vector3> freeCells = new List<Vector3>();
            Vector3Int cellPos= gameMap.WorldToCell(pos);
            if(isCellEmpty(cellPos+new Vector3Int(1, 0, 0))){
                freeCells.Add(gameMap.GetCellCenterWorld(cellPos + new Vector3Int(1, 0, 0)));
            }
            if (isCellEmpty(cellPos + new Vector3Int(-1, 0, 0))){
                freeCells.Add(gameMap.GetCellCenterWorld(cellPos + new Vector3Int(-1, 0, 0)));
            }
            if (isCellEmpty(cellPos + new Vector3Int(0, 1, 0))){
                freeCells.Add(gameMap.GetCellCenterWorld(cellPos + new Vector3Int(0, 1, 0)));
            }
            if (isCellEmpty(cellPos + new Vector3Int(0, -1, 0))){
                freeCells.Add(gameMap.GetCellCenterWorld(cellPos + new Vector3Int(0, -1, 0)));
            }
            return freeCells;
        }
        public void Reset()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    Vector3Int cell = new Vector3Int(i - 10, j - 4, 0);
                    if (gameMap.GetTile(cell)==destructableTile)
                    {
                        gameMap.SetTile(cell, null);
                    }
                }
            }
        }
        public void GenerateMap()
        {
            Debug.Log("[MapManager]Generating map");
            generator.GenerateMap();
        }
        public Vector3 GetCellPos(Vector3 worldPos)
        {
            Vector3Int cell = gameMap.WorldToCell(worldPos);
            Vector3 cellCenterPos = gameMap.GetCellCenterWorld(cell);
            return cellCenterPos;
        }
    
        public bool isCellEmpty(Vector3Int cellPos)
        {
            return !gameMap.GetTile(cellPos);
        }

        public void CreateDestructableTile(Vector3Int tilePos)
        {
            ///Debug.Log("[MapManager]CreatingTile ");
            gameMap.SetTile(tilePos, destructableTile);
        }

        public void BombExploded(Vector3 bombPos)
        {
            Vector3Int originCell = gameMap.WorldToCell(bombPos);
            ExplodeCell(originCell);
            ExplodeCell(originCell + new Vector3Int(1, 0, 0));
            ExplodeCell(originCell + new Vector3Int(0, 1, 0));
            ExplodeCell(originCell + new Vector3Int(-1, 0, 0));
            ExplodeCell(originCell + new Vector3Int(0, -1, 0));
        }

        bool ExplodeCell(Vector3Int cell)
        {
            TileBase tile = gameMap.GetTile(cell);
            if (tile == null || tile == destructableTile)
            {
                Vector3 pos = gameMap.GetCellCenterWorld(cell);
                GameObject explode=GameObject.Instantiate(explosion, pos, Quaternion.identity);
                GameObject.Destroy(explode, .2f);
                if (tile == destructableTile)
                {
                    gameMap.SetTile(cell, null);
                }
            }
            return true;
        }
        public Vector3 GetRandomSpawnPosition()
        {
            Vector3Int cellpos = new Vector3Int(Random.Range(-10, 5), Random.Range(-4, 6), 0);
            while(gameMap.GetTile(cellpos)!= destructableTile)
            {
                cellpos = new Vector3Int(Random.Range(-10, 4), Random.Range(-3, 6), 0);
            }
            gameMap.SetTile(cellpos, null);
            return gameMap.GetCellCenterLocal(cellpos);
        }
    }
}