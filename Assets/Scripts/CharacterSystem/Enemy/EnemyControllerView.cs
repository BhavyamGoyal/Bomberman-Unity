using GridSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
namespace CharacterSystem.Enemy
{
    public class EnemyControllerView : MonoBehaviour
    {
        EnemyManager enemyManager;
        MapManager mapManager;
        public List<Vector3> freeCells;
        Vector3 destination, startPosition;
        public float speed = 1f;
        private float startTime;
        private float journeyLength;
        bool islocked = true;
        CancellationTokenSource cts;
        Task waitForFree = null;
        private void Start()
        {
            startPosition = destination = transform.position;
            startTime = Time.time;
            cts = new CancellationTokenSource();
            journeyLength = Vector3.Distance(startPosition, destination);
            WaitingToBeFree();
        }

        void Update()
        {
            if (!islocked)
            {
                float distCovered = (Time.time - startTime) * speed;
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
        }
        public void DestroyEnemy()
        {
            Destroy(gameObject);
        }
        public void SetManager(EnemyManager manager, MapManager mapManager)
        {
            this.mapManager = mapManager;
            enemyManager = manager;
        }
        void WaitingToBeFree()
        {
            Task.Factory.StartNew(async() =>
            {
                while (islocked)
                {
                    await new WaitForSeconds(1f);
                    GetNextCell();
                }
            },cts.Token);
        }
        private void OnDestroy()
        {
            Debug.Log(gameObject.name);
            if (waitForFree != null)
            {
                cts.Cancel();
            }
        }

        void GetNextCell()
        {
            startPosition = destination;
            freeCells = mapManager.GetNearbyEmptyCells(this.transform.position);
            if (freeCells.Count > 0)
            {
                waitForFree = null;
                islocked = false;
                destination = freeCells[UnityEngine.Random.Range(0, freeCells.Count)];
                //Debug.Log("[EnemuControllerView] next position " + destination );
            }
            else { destination = this.transform.position; }
            startTime = Time.time;
            LookAt(this.transform.position, destination);
            journeyLength = Vector3.Distance(startPosition, destination);
        }
            void LookAt(Vector2 start, Vector2 end)
            {
                Vector2 diff = end - start;
                float zRot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, zRot-90);
            }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "explosion")
            {
                enemyManager.DestroyEnemy(this);
                Destroy(this.gameObject);
            }
            else if (collision.gameObject.tag == "bomb")
            {
                freeCells.Remove(destination);
                if (freeCells.Count > 0)
                {
                    destination = freeCells[UnityEngine.Random.Range(0, freeCells.Count)];
                    destination = mapManager.GetCellPos(transform.position);
                }
            }

        }
    }
}