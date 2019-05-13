using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UISystem
{
    public class UIManager
    {

        PopUpController popUp;
        // Start is called before the first frame update
        public UIManager(PopUpController popUp)
        {
            this.popUp = popUp;
            GameManager.Instance.onGameReset += Reset;
            popUp.Hide();
            popUp.SetManager(this);
        }
        public void GameOver(string message)
        {
            popUp.ShowPopUp(message);
        }
        public void Reset()
        {
            popUp.Hide();
        }
        public void NotifyRestart()
        {
            GameManager.Instance.Reset();
        }

    }
}