using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kret : MonoBehaviour
{
    [SerializeField] RespawnPotins RespawnPotins;
    private void Start()
    {
        RespawnPotins = GameObject.FindGameObjectWithTag("RespawnPotins").GetComponent<RespawnPotins>();
    }

    private void OnCollisionStay(Collision collision)
    {
        transform.localPosition = RespawnPotins.drawPosition();
    }
}
