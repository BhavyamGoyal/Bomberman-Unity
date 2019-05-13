using CharacterSystem.Player;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InputSystem
{
    public class InputControler : MonoBehaviour
    {
        // Start is called before the first frame update
        PlayerControllerView player;
        Vector3 speed = new Vector3(0, 0, 0);
        private void OnEnable()
        {
            GameManager.Instance.onGameReset += Reset;   
        }
        private void Reset()
        {
            player = null;  
        }
        public void SetPlayer(PlayerControllerView player)
        {
            this.player = player;
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                    player.FireBomb();
            }
            if (Input.GetAxis("Horizontal") > 0.2)
            {
                speed.x = 1;
            }else if(Input.GetAxis("Horizontal") < -0.2)
            {
                speed.x = -1;
            }
            else
            {
                speed.x = 0;
            }
            if (Input.GetAxis("Vertical") > 0.2)
            {
                speed.y = 1;
            }
            else if (Input.GetAxis("Vertical") < -0.2)
            {
                speed.y = -1;
            }
            else
            {
                speed.y = 0;
            }
            if (player != null)
            {
                player.SetVelocity(speed);
            }

        }
    }
}