using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Glizda : MonoBehaviour
{
    [SerializeField] RespawnPotins respawnPotins;
    [SerializeField] TextMeshProUGUI Score; 
    private void Start()
    {
        respawnPotins = GameObject.FindGameObjectWithTag("RespawnPotins").GetComponent<RespawnPotins>();
        Score = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.CompareTag("Kret"))
        {
            Debug.LogError("TAK £OO");
            respawnPotins.Score += 1;
            Score.text = "Score " + respawnPotins.Score;
            transform.position = respawnPotins.drawPosition();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        transform.position = respawnPotins.drawPosition();
    }
}
