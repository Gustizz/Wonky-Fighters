using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "New GameManager", menuName = "GameManager")]

public class GameManager : ScriptableObject
{


    public List<enemyStats> enemies;

    public int fightNum;
    public int numOfFights;

    public void ClearData()
    {
        fightNum = 0;
        numOfFights = 0;
    }

}
