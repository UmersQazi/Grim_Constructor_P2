using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private int width, height, fontSize;
    private int[,] gridArray;
    private float cellSize;
    private Vector3 originPosition;

    private TextMesh[,] debugTextArray;
    private SpriteRenderer[,] greenSpriteArray;
    private Sprite square;

    private LineRenderer[,] lineArray;

    private int squareSpriteSortingOrder;
    private int toolSpriteSortingOrder;

    private Color standardColor;
    private Color occupiedColor;
    private Color availableColor;

    private Level01Manager levelManager;


    public Grid(int width, int height, int fontSize, float cellSize, Vector3 originPosition, Sprite square, UnityEngine.Color color
        , int squareSpriteSortingOrder, int toolSpriteSortingOrder, Color standardColor, Color occupiedColor, Color availableColor, Level01Manager levelManager)
    {
        this.width = width;
        this.height = height;
        this.fontSize = fontSize;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        this.square = square;
        this.squareSpriteSortingOrder = squareSpriteSortingOrder;
        this.toolSpriteSortingOrder = toolSpriteSortingOrder;

        this.standardColor = standardColor;
        this.occupiedColor = occupiedColor;
        this.availableColor = availableColor;
        
        gridArray = new int[width, height];
        lineArray = new LineRenderer[width, height];
        //Array that holds all the text objects seen within the cells of the grid
        debugTextArray = new TextMesh[width, height];

        //Array that holds the green square sprites
        greenSpriteArray = new SpriteRenderer[width, height];


        for (int x = 0; x < gridArray.GetLength(0); x++)
            for(int y = 0; y < gridArray.GetLength(1); y++)
            {
                //Creates and adds a new text object to the text array
                debugTextArray[x,y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 5, 
                Color.white, TextAnchor.MiddleCenter);
                //Debug.Log($"{x}, {y}");
                //Debug.Log("Drawing Line\n");

                //Draws out the grid
                lineArray[x, y] = UtilsClass.CreateNewLineRenderer(cellSize, null, GetWorldPosition(x,y), color, 25);
                //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y+1), Color.white,100f);
                //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);

                //Create and add a new green square sprite at the cell's location
                greenSpriteArray[x, y] = UtilsClass.CreateNewSprite(square, null, 
                    GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, standardColor, 25, cellSize);


            }
        //Draws grid edges
        UtilsClass.CreateGridEdges(GetWorldPosition(width, 0), GetWorldPosition(0, height), GetWorldPosition(width, height), color, 25);
        //Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        //Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);


    }

    private void Update()
    {
        
    }

    //Gets the position of the given grid box
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    //Sets the value of the given square
    public void SetValue(int x, int y, int value)
    {
        //Changes the value
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            gridArray[x, y-1] = value;
            gridArray[x-1, y] = value;
            gridArray[x-1, y-1] = value;
        }

        //Changes the text of the cell to the new value
        debugTextArray[x, y].text = gridArray[x,y].ToString();
        debugTextArray[x, y-1].text = gridArray[x, y-1].ToString();
        debugTextArray[x-1, y].text = gridArray[x-1, y].ToString();
        debugTextArray[x-1, y-1].text = gridArray[x-1, y-1].ToString();
    }

    //Returns the position clicked on the mouse as the index on the gridArray
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x/cellSize);
        y = Mathf.FloorToInt((worldPosition- originPosition).y/cellSize);
    }

    //Takes position clicked on by mouse and the new value, changes the position into a gridArray index and
    //calls the original SetValue method that changes the value of that index and the TextMesh
    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y)
    {
        //This if statement accounts for the edges and corners of the grid
        if (x == width - 1 || y == height - 1 || x == 0 || y == 0)
        {
            return -1;
        }

        if (x >= 0 && y >= 0 && x < width && y < height)
            if(gridArray[x, y] == 0 && gridArray[x, y - 1] == 0 && gridArray[x - 1, y] == 0 && gridArray[x - 1, y - 1] == 0)
                return gridArray[x, y];
        //else
            return -1;

        
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
        
    }

    public void ClearGreen()
    {
        foreach(SpriteRenderer g in greenSpriteArray)
        {
            g.color = standardColor;
        }
    }

    public void ChangeColorAlt(int x, int y, Level01Manager levelManager)
    {
        if (levelManager.toolToBePlaced.tileIncrementsX.Length == levelManager.toolToBePlaced.tileIncrementsY.Length) { 
            bool canPlace = true;
            for (int i = 0; i < levelManager.toolToBePlaced.tileIncrementsX.Length; i++)
            {
                if (gridArray[x - levelManager.toolToBePlaced.tileIncrementsX[i],
                    y - levelManager.toolToBePlaced.tileIncrementsY[i]] != 0)
                {
                    canPlace = false;
                }
            }

            if (canPlace)
            {
                for (int i = 0; i < levelManager.toolToBePlaced.tileIncrementsX.Length; i++)
                {
                    greenSpriteArray[x - levelManager.toolToBePlaced.tileIncrementsX[i],
                        y - levelManager.toolToBePlaced.tileIncrementsY[i]].color = availableColor;
                }
            }

            else if (!canPlace)
            {
                for (int i = 0; i < levelManager.toolToBePlaced.tileIncrementsX.Length; i++)
                {
                    greenSpriteArray[x - levelManager.toolToBePlaced.tileIncrementsX[i],
                        y - levelManager.toolToBePlaced.tileIncrementsY[i]].color = occupiedColor;
                }
            }
            //Tool toolInstance = ScriptableObject.CreateInstance("Tool") as Tool;
            //toolInstance.tileIncrementsCoordinates = toolToBePlaced.tileIncrementsCoordinates;


            TileClearAlt(x, y, levelManager.toolToBePlaced);
        }

    }


    //For some reason requires an instance of the tool??????
    public void TileClearAlt(int x, int y, Tool toolToBePlaced)
    {
        bool noMatch = false;
        for (int z = 0; z < greenSpriteArray.GetLength(0); z++)
        {
            for (int w = 0; w < greenSpriteArray.GetLength(1); w++)
            {
                noMatch = TileClearAlt2(x, y, z, w, toolToBePlaced);

                if (noMatch)
                {
                    Debug.Log("Clearing Tile");
                    greenSpriteArray[z, w].color = standardColor;
                }
                

            }
        }
        //ScriptableObject.Destroy(toolToBePlaced);
    }

    public bool TileClearAlt2(int x, int y, int z, int w, Tool toolToBePlaced)
    {
        Dictionary<int, int> tileCoor = toolToBePlaced.tileIncrementsCoordinates; 

        if(toolToBePlaced != null)
        {
            Debug.Log("Tool detected");
            Debug.Log("Number of coordinates:  " + toolToBePlaced.tileIncrementsX.Length);
        }
        else
        {
            Debug.Log("Tool NOT detected");
        }

        

        bool noMatch = false;





        for(int i = 0; i < toolToBePlaced.tileIncrementsX.Length; i++)
        {
            int xCoor = toolToBePlaced.tileIncrementsX[i];
            int yCoor = toolToBePlaced.tileIncrementsY[i];
            Debug.Log("Increments: " + xCoor + "," + yCoor);
            //tileCoor.TryGetValue(tileCoor[i], out yCoor);

            //Debug.Log(tileCoor[i] + ", " + yCoor);

            if (x - xCoor != z && y - yCoor == w)
            {
                noMatch = true;
            }else if(x - xCoor == z && y - yCoor != w)
            {
                noMatch = true;
            }else if(x - xCoor != z && y - yCoor != w)
            {
                noMatch = true;
            }else if(x - xCoor == z && y - yCoor == w)
            {
                Debug.Log("Its equal");
            }

        }

        if (noMatch)
        {
            Debug.Log("Clearing");
        }

        return noMatch;



        
        /*
        if(toolToBePlaced.tileIncrementsCoordinates.ContainsKey(x) && toolToBePlaced.tileIncrementsCoordinates[x] != y)
        {
            greenSpriteArray[x, y].color = standardColor;
        }else if (!toolToBePlaced.tileIncrementsCoordinates.ContainsKey(x))
        {
            greenSpriteArray[x, y].color = standardColor;
        }
        */
    }


    public void ChangeColor(int x, int y, GameObject mouseSprite)
    {
        
        if (gridArray[x,y] == 0 && gridArray[x,y-1] == 0 && gridArray[x-1,y] == 0 && gridArray[x-1, y-1] == 0)
        {
            greenSpriteArray[x, y].color = availableColor;
            greenSpriteArray[x, y-1].color = availableColor;
            greenSpriteArray[x-1, y].color = availableColor;
            greenSpriteArray[x-1, y-1].color = availableColor;
        }
        else if(gridArray[x, y] == 1 || gridArray[x, y - 1] == 1 || gridArray[x - 1, y] == 1 || gridArray[x - 1, y - 1] == 1)
        {
            greenSpriteArray[x, y].color = occupiedColor;
            greenSpriteArray[x, y - 1].color = occupiedColor;
            greenSpriteArray[x - 1, y].color = occupiedColor;
            greenSpriteArray[x - 1, y - 1].color = occupiedColor;
        }
        else
        {
            /*
            greenSpriteArray[x, y].color = standardColor;
            greenSpriteArray[x, y - 1].color = standardColor;
            greenSpriteArray[x - 1, y].color = standardColor;
            greenSpriteArray[x - 1, y - 1].color = standardColor;
            */
        }


        TileClear(x, y, mouseSprite);
        
        

    }

    public void TileClear(int x, int y, GameObject mouseSprite)
    {
        
        //if(mouseSprite == null)
        //Goes through every square that is not selected and keeps its color to yellow
        for (int z = 0; z < greenSpriteArray.GetLength(0); z++)
        {
            for (int w = 0; w < greenSpriteArray.GetLength(1); w++)
            {

                if (z != x && z != x - 1 && w != y && w != y - 1)
                {
                    greenSpriteArray[z, w].color = standardColor;
                }

                if (z == x && w != y && w != y - 1)
                {
                    greenSpriteArray[z, w].color = standardColor;
                }
                if (w == y && z != x - 1 && z != x)
                {
                    greenSpriteArray[z, w].color = standardColor;
                }
                if (z == x - 1 && w != y && w != y - 1)
                {
                    greenSpriteArray[z, w].color = standardColor;
                }
                if (w == y - 1 && z != x - 1 && z != x)
                {
                    greenSpriteArray[z, w].color = standardColor;
                }


            }
        }
    }

    //This makes sure none of the tiles on the grid are still highlighted after tool placement
    public void ManualTileClear()
    {
        
        for (int z = 0; z < greenSpriteArray.GetLength(0); z++)
        {
            for (int w = 0; w < greenSpriteArray.GetLength(1); w++)
            {
                greenSpriteArray[z, w].color = standardColor;
            }
        }
    }


}
