using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BombSystem
{
    public class BombControllerView : MonoBehaviour
    {
        // Start is called before the first frame update
        BombManager bombManager;
        public void SetBombManager(BombManager bombManager)
        {
            this.bombManager = bombManager;
        }
        public void ActivateBomb()
        {
            ShowBomb();
            Invoke("Explode", 3f);
        }
        public void HideBomb()
        {
            gameObject.SetActive(false);
        }
        public void ShowBomb()
        {
            gameObject.SetActive(true);
        }
        void Explode()
        {
            if (gameObject.activeSelf)
            {
                bombManager.BombExploded();
                HideBomb();
            }
        }
        public void SetCell(Vector3 bombPos)
        {
            this.transform.position = bombPos;
        }

    }
}