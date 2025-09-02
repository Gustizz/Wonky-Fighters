using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerInTown : MonoBehaviour
{
    public GameManager GameManager;

    public GameObject player;

    public Vector3 arenaSpawn;
    public Vector3 tutSpawn;

    private void Start()
    {
        if (GameManager.numOfFights > 0)
        {
            player.transform.position = arenaSpawn;
        }
        else
        {
            player.transform.position = tutSpawn;
        }
    }
}
