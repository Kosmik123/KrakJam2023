using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private RespawnPotins respawnPotins;

    [SerializeField]
    private LevelGenerator levelGenerator;


    private void Start()
    {
        levelGenerator.GenerateLevel();
        respawnPotins.StartLevel();
    }

}
