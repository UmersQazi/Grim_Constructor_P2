using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Grid Shape")]
[Serializable]
public class Shape : ScriptableObject
{
    [SerializeField] private List<Vector2Int> tileIncrements;

    public List<Vector2Int> TileIncrements { get { return tileIncrements; } }


}
