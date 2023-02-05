using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPotins : MonoBehaviour
{
    [SerializeField] GameObject Kret;
    [SerializeField] GameObject prefabGlizda;
    [SerializeField] GameObject[] pointsTab = new GameObject[5] ;
    [SerializeField] int[] sectionSizeX = new int[2] ;
    [SerializeField] int[] sectionSizeY = new int[2] ;
    [SerializeField] int[] sectionSizeZ = new int[2] ;
    public int Score = 0 ;

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
        axisX = Random.Range(sectionSizeX[0], sectionSizeX[1]);
        axisY = Random.Range(sectionSizeY[0], sectionSizeY[1]);
        axisZ = Random.Range(sectionSizeZ[0], sectionSizeZ[1]);
        Debug.Log(new Vector3(axisX, axisY, axisZ));
        return new Vector3(axisX, axisY, axisZ);
    }

}
   

