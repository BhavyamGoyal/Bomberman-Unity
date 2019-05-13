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
        GameUIController gameUI;
        // Start is called before the first frame update
        public UIManager(GameUIController gameUI,PopUpController popUp, Button playButton)
        {
            this.gameUI=gameUI;
            this.playButton = playButton;
            this.popUp = popUp;
            this.playButton.onClick.AddListener(StartGame);
            GameManager.Instance.onGameReset += Reset;
            popUp.Hide();
            gameUI.HideText();
            popUp.SetManager(this);
        }
        public void UpdateScore(int score)
        {
            gameUI.UpdateScoreText(score);
        }
        ~UIManager()
        {
            this.playButton.onClick.RemoveListener(StartGame);
        }
        private void StartGame()
        {
            GameManager.Instance.StartPlaying();
            gameUI.ShowText();
            playButton.gameObject.SetActive(false);
        }
        public void GameOver(string message,int score)
        {
            popUp.ShowPopUp(message, score);
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