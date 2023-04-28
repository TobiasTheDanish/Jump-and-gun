using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterWall : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            gameManager.RespawnPlayer(collision.gameObject);
        }
    }
}
