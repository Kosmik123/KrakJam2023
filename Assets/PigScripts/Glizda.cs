using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Glizda : MonoBehaviour
{
    private const string emisionColorName = "_EmissionColor";
    private RespawnPoints respawnPoints;
    private TextMeshProUGUI Score;

    [SerializeField]
    private Renderer meshRenderer;
    private Transform player;

    [SerializeField]
    private float emissionStartDistance;
    [SerializeField]
    private float emissionForce;
    [ColorUsage(true, true)]
    private Color emissionColor;

    private void Awake()
    {
        respawnPoints = GameObject.FindGameObjectWithTag("RespawnPotins").GetComponent<RespawnPoints>();
        Score = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Kret").transform;
        emissionColor = meshRenderer.material.GetColor(emisionColorName);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform == player)
        {
            respawnPoints.Score += 1;
            Score.text = "Score " + respawnPoints.Score;
            transform.position = respawnPoints.drawPosition();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        transform.position = respawnPoints.drawPosition();
    }

    private void Update()
    {
        float distanceFromBorder = Vector3.Distance(transform.position, player.position) - emissionStartDistance;
        if (distanceFromBorder > 0)
        {
            float emissionValue = distanceFromBorder * emissionForce;
            meshRenderer.material.SetColor(emisionColorName, emissionValue * emissionColor);
        }
    }


}
