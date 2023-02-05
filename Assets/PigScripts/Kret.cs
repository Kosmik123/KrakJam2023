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
      Vector3 Tak³oo =  RespawnPotins.drawPosition();
        transformSmoother.Position = Tak³oo;
        gridPosition.Position = Tak³oo.ToVector3Int();
    }


}
