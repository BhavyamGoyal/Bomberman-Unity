using BombSystem;
using Common;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{

    public Tilemap tilemap;
    BombManager bombManager;
    private void Start()
    {
        Debug.Log(GameManager.Instance.ToString());
        bombManager = GameManager.Instance.GetBombManager();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bombManager.LaunchBomb(worldPos);
        }
    }
}
