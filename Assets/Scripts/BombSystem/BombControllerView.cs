using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BombSystem{
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
            Invoke("Explode", .4f);
        }
        void Explode()
        {
            bombManager.BombExploded();
            this.gameObject.SetActive(false);
        }
        public void SetCell( Vector3 bombPos)
        {
            this.transform.position = bombPos;
        }
        
    }
}