using CharacterSystem.Player;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InputSystem
{
    public class InputControler : MonoBehaviour
    {
        FloatingJoystick JoyStick;
        Button fire_Bomb=null;
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
        public void SetUpInputController(PlayerControllerView player, FloatingJoystick JoyStick,Button fire_Bomb)
        {
            if (this.fire_Bomb == null)
            {
                this.fire_Bomb = fire_Bomb;
                this.fire_Bomb.onClick.AddListener(FireBomb);
            }
            this.JoyStick = JoyStick;
            this.player = player;
        }

        public void FireBomb()
        {
            Debug.Log("bomb firing");
            player.FireBomb();
        }

        // Update is called once per frame
        void Update()
        {

            if (player != null)
            {
                if (JoyStick.Horizontal > 0.5)
                {
                    speed.x = 1;
                }
                else if (JoyStick.Horizontal < -0.5)
                {
                    speed.x = -1;
                }
                else
                {
                    speed.x = 0;
                }
                if (JoyStick.Vertical > 0.5)
                {
                    speed.y = 1;
                }
                else if (JoyStick.Vertical < -0.5)
                {
                    speed.y = -1;
                }
                else
                {
                    speed.y = 0;
                }
                player.SetVelocity(speed);
            }

        }
    }
}