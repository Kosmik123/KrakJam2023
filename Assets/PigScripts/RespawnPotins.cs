using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPotins : MonoBehaviour
{
    [SerializeField] GameObject Kret;
    [SerializeField] GameObject prefabGlizda;
    [SerializeField] GameObject[] pointsTab = new GameObject[5] ;
    private int axisX;
    private int axisY;
    private int axisZ;

    private void Start()
    {
        Instantiate(Kret).transform.position = drawPosition();
        RespawnObejct();
    }


    public void RespawnObejct()
    {
        for (int i = 0; i < pointsTab.Length; ++i)
        {

            GameObject buforGlizda = Instantiate(prefabGlizda);
            buforGlizda.transform.position = drawPosition();

        }
    }
    public Vector3 drawPosition()
    {

        axisX = Random.Range(1, 5);
        axisY = Random.Range(1, 5);
        axisZ = Random.Range(1, 5);
        Debug.Log(new Vector3(axisX, axisY, axisZ));
        return new Vector3(axisX, axisY, axisZ);
    }

}
   

