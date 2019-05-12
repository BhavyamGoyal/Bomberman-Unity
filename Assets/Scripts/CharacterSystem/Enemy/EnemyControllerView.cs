using GridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CharacterSystem.Enemy
{
    public class EnemyControllerView : MonoBehaviour
    {
        EnemyManager enemyManager;
        MapManager mapManager;
        public List<Vector3> freeCells;
        Vector3 destination, startPosition;
        public float speed = 2f;
        private float startTime;
        private float journeyLength;
        private void Start()
        {
            startPosition=destination = transform.position;
            startTime = Time.time;
            journeyLength = Vector3.Distance(startPosition, destination);
        }
        void Update()
        {
            float distCovered = (Time.time - startTime)*speed;
            float fracJourney = distCovered / journeyLength;
            Vector3 newPos = Vector3.Lerp(startPosition, destination, fracJourney);
            if (!float.IsNaN(newPos.x))
            {
                transform.position = newPos;
            }
            //this.transform.position = Vector3.Lerp(this.transform.position, destination, .05f);
            if (this.transform.position == destination)
            {
                GetNextCell();
            }
        }
        public void SetManager(EnemyManager manager, MapManager mapManager)
        {
            this.mapManager = mapManager;
            enemyManager = manager;
        }
        void GetNextCell()
        {
            startPosition = destination;
            freeCells = mapManager.GetNearbyEmptyCells(this.transform.position);
            if (freeCells.Count > 0)
            {
                destination=freeCells[Random.Range(0, freeCells.Count)];
                //Debug.Log("[EnemuControllerView] next position " + destination );
            }
            else { destination = this.transform.position; }
            startTime = Time.time;
            journeyLength = Vector3.Distance(startPosition, destination);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "explosion")
            {
                enemyManager.DestroyEnemy(this);
                Destroy(this.gameObject);
            }
        }
    }
}