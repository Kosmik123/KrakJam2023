using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glizda : MonoBehaviour
{
    [SerializeField] RespawnPotins respawnPotins;

    private void Start()
    {
        respawnPotins = GameObject.FindGameObjectWithTag("RespawnPotins").GetComponent<RespawnPotins>();
    }

    private void OnCollisionStay(Collision collision)
    {
        transform.position = respawnPotins.drawPosition();
    } 
}
