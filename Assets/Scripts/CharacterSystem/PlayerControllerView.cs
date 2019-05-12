using BombSystem;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CharacterSystem.Player
{
    public class PlayerControllerView : MonoBehaviour
    {
        BombManager bombManager;
        Rigidbody2D rb;
        int speed = 10;
        Vector3 velocity = new Vector3(0, 0, 0);
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        public void SetBombManager(BombManager bombManager)
        {
            this.bombManager = bombManager;
        }
        void FixedUpdate()
        {
            rb.velocity = velocity * speed;
        }
        public void SetVelocity(Vector3 velocity)
        {
            this.velocity = velocity;
        }

        public void FireBomb()
        {
            bombManager.LaunchBomb(this.transform.position);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "explosion" || collision.gameObject.tag == "enemy")
            {
                this.gameObject.SetActive(false);
                GameManager.Instance.GameOver("Player Died");
            }
        }

    }
}