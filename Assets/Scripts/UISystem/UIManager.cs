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
        Button playButton;
        // Start is called before the first frame update
        public UIManager(PopUpController popUp, Button playButton)
        {
            this.playButton = playButton;
            this.popUp = popUp;
            this.playButton.onClick.AddListener(StartGame);
            GameManager.Instance.onGameReset += Reset;
            popUp.Hide();
            popUp.SetManager(this);
        }
        ~UIManager()
        {
            this.playButton.onClick.RemoveListener(StartGame);
        }
        private void StartGame()
        {
            GameManager.Instance.StartPlaying();
            playButton.gameObject.SetActive(false);
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