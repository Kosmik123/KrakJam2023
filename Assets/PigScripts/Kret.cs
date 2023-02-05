using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kret : MonoBehaviour
{
    [SerializeField] RespawnPoints RespawnPotins;
    [SerializeField] TransformSmoother transformSmoother;
    [SerializeField] GridPosition gridPosition;
    [SerializeField] Rigidbody rigidbodydd;
    private void Start()
    {
        RespawnPotins = GameObject.FindGameObjectWithTag("RespawnPotins").GetComponent<RespawnPoints>();
    }

    private void OnCollisionStay(Collision collision)
    {
      Vector3 Tak�oo =  RespawnPotins.drawPosition();
        transformSmoother.Position = Tak�oo;
        gridPosition.Position = Tak�oo.ToVector3Int();
    }


}
