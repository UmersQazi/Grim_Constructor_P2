using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New tool")]
public class Tool : ScriptableObject
{
    public new string name;
    public int cost;
    public int amount;
    public int gridPlacementValue;
}
