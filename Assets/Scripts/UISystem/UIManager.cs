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
        Joystick joyStick;
        Button fireButton;
        GameUIController gameUI;
        GameManager gameManager;
        // Start is called before the first frame update
        public UIManager(GameUIController gameUI, PopUpController popUp, Button playButton,Joystick joyStick,Button fireButton)
        {
            this.fireButton = fireButton;
            this.joyStick = joyStick;
            this.gameUI = gameUI;
            this.playButton = playButton;
            this.popUp = popUp;
            this.playButton.onClick.AddListener(StartGame);
            gameManager = GameManager.Instance;
            gameManager.onGameReset += Reset;
            popUp.Hide();
            gameUI.HideText();
            setGameUI(false);
            popUp.SetManager(this);
        }
        public void setGameUI(bool set)
        {
            joyStick.gameObject.SetActive(set);
            fireButton.gameObject.SetActive(set);
        }
        public void StartGameUI()
        {
            setGameUI(true);
        }
       // public
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
            gameManager.StartPlaying();
            gameUI.ShowText();
            playButton.gameObject.SetActive(false);
            setGameUI(true);
        }
        public void GameOver(string message, int score)
        {
            setGameUI(false);
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