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
        int speed = 5;
        Vector3 velocity = new Vector3(0, 0, 0);
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        public void DestroyPlayer()
        {
            Destroy(gameObject);
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
            if (velocity.x >0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            }else if (velocity.x < 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            }else if (velocity.y < 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            }else if (velocity.y > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
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
                Destroy(gameObject);
                GameManager.Instance.GameOver("Player Died");
            }
        }

    }
}