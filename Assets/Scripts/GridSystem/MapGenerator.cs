using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GridSystem
{
    public class MapGenerator
    {

        MapManager mapManager;
        public MapGenerator(MapManager mapManager)
        {
            this.mapManager = mapManager;
        }
        public void GenerateMap()
        {
            int tileCount = 0;
            int noOfDestructableTiles = Random.Range(80, 110)+3;
            float probabilityOfTile = 70f;
            Debug.Log("[MapGenerator]Generateing map ");
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    Vector3Int tilePos = new Vector3Int(i - 10, j - 4, 0);
                    float r = Random.Range(0, 100);
                    if (r < probabilityOfTile && tileCount < noOfDestructableTiles && mapManager.isCellEmpty(tilePos))
                    {
                        tileCount++;
                        mapManager.CreateDestructableTile(tilePos);
                    }
                }
            }
        }
    }
}