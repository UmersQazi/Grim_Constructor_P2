using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBar : MonoBehaviour
{
    public bool placed, placing;

    public void EndAnim()
    {
        placed = true;
        placing = false;
    }
    public void StartAnim()
    {
        placing = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        placed = false;
    }
    private void OnEnable()
    {
        placed = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
