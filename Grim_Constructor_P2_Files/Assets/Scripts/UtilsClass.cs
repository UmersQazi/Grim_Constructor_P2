using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public static class UtilsClass
{
    public const int sortingOrderDefault = 5000;


    //TextMesh methods
    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), 
        int fontSize = 40, UnityEngine.Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, 
        TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault)
    {
        if(color == null) color = UnityEngine.Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (UnityEngine.Color)color, textAnchor, textAlignment, sortingOrder);
    }


    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, UnityEngine.Color color, 
        TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        //Set parent to false to keep text at its local orientation instead of world orientation
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.color = color;
        textMesh.fontSize = fontSize;
        textMesh.anchor = textAnchor;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

        return textMesh;
    }

    //Sprite Renderer methods
    public static SpriteRenderer CreateNewSprite(Sprite square, Transform parent = null, Vector3 localPosition = default(Vector3),
        UnityEngine.Color ? color = null, int sortingOrder = sortingOrderDefault, float cellSize = default)
    {
        if (color == null) color = UnityEngine.Color.white;
        return CreateNewSprite(parent, square, localPosition, (UnityEngine.Color)color, sortingOrder, cellSize);
    }

    public static SpriteRenderer CreateNewSprite(Transform parent, Sprite square, Vector3 localPosition, 
        UnityEngine.Color color, int sortingOrder, float cellSize)
    {
        GameObject sprite = new GameObject("World_Green", typeof(SpriteRenderer));
        Transform transform = sprite.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        transform.localScale *= cellSize;
        SpriteRenderer spritePart = sprite.GetComponent<SpriteRenderer>();
        spritePart.sprite = square;
        spritePart.color = color;
        spritePart.drawMode = SpriteDrawMode.Simple;
        spritePart.maskInteraction = SpriteMaskInteraction.None;
        spritePart.spriteSortPoint = SpriteSortPoint.Center;
        spritePart.sortingOrder = sortingOrder;
        return spritePart;
    }


    //Line Renderer methods
    public static LineRenderer CreateNewLineRenderer(float cellSize, Transform parent = null, Vector3 localPosition = default(Vector3), UnityEngine.Color? color = null, 
         int sortingOrder = sortingOrderDefault)
    {
        if (color == null) color = UnityEngine.Color.white;
        return CreateNewLineRenderer(cellSize, parent, localPosition, (UnityEngine.Color)color, sortingOrder);
    }

    public static LineRenderer CreateNewLineRenderer(float cellSize, Transform parent, Vector3 localPosition, UnityEngine.Color color,
         int sortingOrder)
    {
        
        GameObject line = new GameObject("Line", typeof(LineRenderer));
        LineRenderer linePart = line.GetComponent<LineRenderer>();
        Material material = linePart.material;

        //Setting color of line's material
        material.color = color;
        material.EnableKeyword("Emission");
        material.SetColor("_EmissionColor", color);
        linePart.material = material;

        //Setting the width and positions of the line
        linePart.startWidth = .05f;
        linePart.endWidth = .05f;
        linePart.positionCount = 2;
        linePart.SetPosition(0, localPosition);
        linePart.SetPosition(1, localPosition + (new Vector3(1,0,0) * cellSize));
        
        /*
        Transform transform = line.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        transform.localScale *= cellSize;
        */

        //Setting line sorting order
        linePart.sortingOrder = sortingOrder;

        //Second, vertical line (NEED TO FIGURE OUT A WAY TO ADD THIS TO LINE ARRAY)
        GameObject line2 = GameObject.Instantiate(line);
        LineRenderer linePart2 = line2.GetComponent<LineRenderer>();
        linePart2.SetPosition(0, localPosition);
        linePart2.SetPosition(1, localPosition + (new Vector3(0, 1, 0)* cellSize));

        return linePart;
    }


    //Mouse position methods
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vector = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vector.z = 0f;
        return vector;
    }

    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }


}
