using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kret : MonoBehaviour
{
    [SerializeField] RespawnPotins RespawnPotins;
    [SerializeField] TransformSmoother transformSmoother;
    [SerializeField] GridPosition gridPosition;
    private void Start()
    {
        RespawnPotins = GameObject.FindGameObjectWithTag("RespawnPotins").GetComponent<RespawnPotins>();
    }

    private void OnCollisionStay(Collision collision)
    {
      Vector3 Tak³oo =  RespawnPotins.drawPosition();
        transformSmoother.Position = Tak³oo;
        gridPosition.Position = Tak³oo.ToVector3Int();
    }

}
