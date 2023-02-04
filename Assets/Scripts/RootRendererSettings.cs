using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RootRendererSettings : ScriptableObject
{
    [field: SerializeField]
    private Mesh[] straightMeshVariants;
    public Mesh StraightMesh => GetRandomFromList(straightMeshVariants); 
    [field: SerializeField]
    private Mesh[] turnMeshVariants;
    public Mesh TurnMesh => GetRandomFromList(turnMeshVariants);
    [field: SerializeField]
    private Mesh[] endingMeshVariants;
    public Mesh EndingMesh => GetRandomFromList(endingMeshVariants);


    private T GetRandomFromList<T> (IReadOnlyList<T> list)
    {
        if (list.Count == 0)
            return default;
        if (list.Count == 1)
            return list[0];
        return list[Random.Range(0, list.Count)];
    }



}
