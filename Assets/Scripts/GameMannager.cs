using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMannager : MonoBehaviour
{
    GameObject player;
    GameObject enemy;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    public void DesactivarGameObjectPlayer()
    {
        player.SetActive(false);
    }
    public void DesactivarGameObjectEnemigo()
    {
        enemy.SetActive(false);
    }
}
