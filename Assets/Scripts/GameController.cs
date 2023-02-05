using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private RespawnPoints respawnPotins;

    [SerializeField]
    private LevelGenerator levelGenerator;


    private void Start()
    {
        levelGenerator.GenerateLevel();
        respawnPotins.StartLevel();
    }

}
