using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Character", menuName = "Character", order = 0)]
public class CharacterSO : ScriptableObject
{
    public string Name;
    [Header("Movement")]
    [Range(50f, 300f)] public float MovementAcceleration;
    [Range(1, 10)] public float MovementSpeed;
    [Range(10, 20)] public float Mass;
    [Header("Misc")]
    public GameObject prefab;
}