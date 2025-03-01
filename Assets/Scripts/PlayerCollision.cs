using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManeger gameManeger;
    private void Awake()
    {
        gameManeger=FindAnyObjectByType<GameManeger>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            gameManeger.AddScore(1);
            Debug.Log("Cham roi day");
        }
    }
}
