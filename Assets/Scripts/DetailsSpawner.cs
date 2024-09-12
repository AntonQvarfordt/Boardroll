using UnityEngine;
using Sirenix.OdinInspector;
[System.Serializable]
public struct DetailProp
{
    public GameObject Prefab;
    public Transform SpawnPosition;
    public Transform SpawnRotation;

    public bool RandomPosition;

    [ShowIfGroup("RandomPosition")]
    [BoxGroup("RandomPosition/Shown Box")]
    public Transform[] RandomPos;
}

public class DetailsSpawner : MonoBehaviour
{
    public DetailProp[] Details;
    public DetailProp Det;
}
