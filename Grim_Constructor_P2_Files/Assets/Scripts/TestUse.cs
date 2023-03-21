using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestUse : MonoBehaviour
{
    private Grid grid;
    [Header("Grid Specs")]
    public int width, height, fontSize, cellSize;
    public Vector3 origin = new Vector3(0,0,0);
    public Sprite square;
    [SerializeField] Color colorOfLines;

    [Header("Preset Spots")]
    public int[,] setPositions;
    public GameObject[] initialSetSprites;



    [Header("Tile Colors")]
    [SerializeField] Color standardColor;
    [SerializeField] Color occupiedColor;
    [SerializeField] Color availableColor;
   


    [Header("Sorting Orders")]
    public int squareSpriteSortingOrder = 25;
    public int toolSpriteSortingOrder = 26;


    [Header("Level Manager")]
    public Level01Manager level01Manager;



    [Header("Mouse Options")]
    public int roundedMousePos;
    public GameObject mouseSprite, prevMouseSprite;
    public Vector3 mousePosition;
    public bool clicked;

    /*
    public GameObject loadBar;
    LoadBar loadBarScript;

    
    public GameObject crateSprite;
    GameObject spriteToBuild;
    GameObject crateToEnd;
    public Villager villager;
    

    //public AudioSource moveSound, buildingSound, buildingCompleteSound;
    */
    Vector3 mousePos, originalMousePos;
    //bool builtSoundPlayed, completeSoundPlayed, particlePlayed;

    //public ParticleSystem particleSystem, particleSystemUsed;
    
    // Start is called before the first frame update
    void Start()
    {
        //loadBarScript = loadBar.GetComponent<LoadBar>();
        //This line creates the actual grid. The user gives data involving the shape, the text, and colors of the grid.
        grid = new Grid(width, height, fontSize, cellSize, origin, square, colorOfLines, squareSpriteSortingOrder, 
            toolSpriteSortingOrder, standardColor, occupiedColor, availableColor, level01Manager);
        //clicked = false;
        //orignalSprite = mouseSprite;

        //Observes the initial position of the mouse and looks at the original mouse sprite
        originalMousePos = mousePos;
        prevMouseSprite = mouseSprite;

        //Makes sure none of the tiles in the grid have any color that indicates a spot is occupied
        CallManualTileClear();
    }

    // Update is called once per frame
    void Update()
    {
        //The current mouse sprite will always be whatever tool the user clicked on in the tool belt UI
        mouseSprite = level01Manager.toolSprite;
        prevMouseSprite = mouseSprite;

        //Makes sure to keep tiles the same unoccupied color once the user clicks on a tool
        if(prevMouseSprite != mouseSprite)
        {
            CallManualTileClear();
            prevMouseSprite = mouseSprite;
        }

        //Resets the mouse position
        if(mousePos != originalMousePos)
        {
            
            //moveSound.Play();
            originalMousePos = mousePos;
        }

        //Gets the current mouse position
        mousePosition = UtilsClass.GetMouseWorldPosition();

        //Rounds down the current mouse position for the tile the mouse is hovering over in the grid
        roundedMousePos = grid.GetValue(mousePosition);

        //If tool has not been placed, move the sprite with mouse
        if(!clicked && mouseSprite != null)
            MoveSprite();

        //Places tool sprite onto the grid and makes sure the tiles are the same color after
        if (Input.GetMouseButtonDown(0) && mouseSprite != null && grid.GetValue(mousePosition) == 0 && level01Manager.goodDeeds > 0)
        {
            Debug.Log("Clicking");
            grid.SetValue(mousePosition, level01Manager.toolToBePlaced.gridPlacementValue);
            level01Manager.ToolDeduction(level01Manager.toolToBePlaced);
            level01Manager.toolSprite = null;
            CallManualTileClear();
            //mouseSprite = null;
        }


        //If user clicks and load animation not playing
        /*
        if (Input.GetMouseButtonDown(0) && !loadBarScript.placing)
        {
            //If space is not occupied and mouse sprite is still initial sprite (CHANGE)
            if (grid.GetValue(mousePosition) == 0 && mouseSprite == orignalSprite)
            {
                //Plays animation sound effect
                if (!builtSoundPlayed)
                {
                    buildingSound.Play();
                    builtSoundPlayed = true;
                }


                spriteToBuild = mouseSprite;

                //Sets position as occupied
                grid.SetValue(mousePosition, 1);
                clicked = true;

                //Gets rid of green tiles
                grid.ClearGreen();
                
                //Introduces second mouse sprite (CHANGE)
                mouseSprite = mouseSprite2;
                
                //Temporarily makes building invisible for building animation
                spriteToBuild.SetActive(false);

                //Turns on mouse sprite if not on already
                mouseSprite.SetActive(true);

                //User can click again
                clicked = false;

                //This starts the building animation, and stores the crate sprite used in the animation to turn off later
                crateToEnd = Building(mousePosition);
            }
            if(grid.GetValue(mousePosition) == 0 && mouseSprite == mouseSprite2)
            {
                particlePlayed = false;
                if (builtSoundPlayed)
                {
                    buildingSound.Play();
                    builtSoundPlayed = false;
                }
                completeSoundPlayed = false;
                //buildingSound.Play();
                spriteToBuild = mouseSprite;
                spriteToBuild.SetActive(false);
                grid.SetValue(mousePosition, 1);
                clicked = true;
                grid.ClearGreen();
                crateToEnd = Building(mousePosition);
            }
            
        }
        */

        //Right-click to check mouse position
        if (Input.GetMouseButton(1))
        {
            Debug.Log(grid.GetValue(mousePosition));
        }

        //Always looks for a crate to turn off
        //TurnOffLoad();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        

    }

    //Clears tiles of any differing colors
    public void CallManualTileClear()
    {
        grid.ManualTileClear();
    }

    //Sets the tool sprite's position on the grid
    public void SetSpritePos(GameObject spriteObj)
    {
        int x, y;

        grid.GetXY(spriteObj.transform.position, out x, out y);
        spriteObj.transform.position = new Vector3(x, y, 0);
        grid.SetValue(spriteObj.transform.position, 1);

    }


    /*
    public void TurnOffLoad()
    {
        if (loadBarScript.placed)
        {
            //Turns off load bar
            loadBar.SetActive(false);
            Debug.Log("Turned off loading!");

            //Brings back placed building
            spriteToBuild.SetActive(true);
            //Turns off crate
            crateToEnd.SetActive(false);

            //Plays building completed sound effect
            if (!completeSoundPlayed)
            {
                buildingCompleteSound.Play();
                completeSoundPlayed = true;
            }

            //Plays perticle effect
            if (!particlePlayed)
            {
                particleSystemUsed.Play();
                particlePlayed = true;
            }
        }
    }
    */
    /*
    //Plays building animation
    public GameObject Building(Vector3 position)
    {
        GameObject newCrate = Instantiate(crateSprite, position-new Vector3(1.5f,1.5f,0), new Quaternion(0,0,0,0));
        villager.nextSpot = position;
        villager.canGo = true;
        particleSystemUsed = Instantiate(particleSystem, position, Quaternion.identity);
        //particleSystem.transform.position = position;
        loadBar.SetActive(true);
        if (loadBar.activeSelf)
        {
            Debug.Log("Turned on loading!");
        }
        if (!loadBar.activeSelf)
        {
            
        }
        loadBar.transform.position = position + new Vector3(0, 1, 0);
        
        Animator loadBarAnim = loadBar.GetComponent<Animator>();
        loadBarAnim.Play("LoadBarAnim1");
        return newCrate;
    }
    */

    //Moves mouse sprite
    public void MoveSprite()
    {
        int x, y;
        //moveSound.Play(); CHANGE

        //Rounds the current mouse position
        grid.GetXY(mousePosition, out x, out y);

        //This rounded position is what lets the mouse sprite move across the scene in a rigid manner
        //If i need it off center, remove .5 increments
        //Moves the mouse sprite rigidly as long as the position is not at any of the edges of or over the grid
        if (x < width && y < height && x >= 1 && y >= 1)
        {
            mouseSprite.transform.position = new Vector3(x, y, 0) * cellSize + origin;
            if (mouseSprite != null)
            {
                //Debug.Log("Count: " +level01Manager.toolToBePlaced.tileIncrementsX.Length);
                /*
                Tool toolInstance = ScriptableObject.CreateInstance("Tool") as Tool;
                toolInstance.amount = level01Manager.toolToBePlaced.amount;
                toolInstance.cost = level01Manager.toolToBePlaced.cost;
                toolInstance.tileIncrementsX = level01Manager.toolToBePlaced.tileIncrementsX;
                toolInstance.tileIncrementsY = level01Manager.toolToBePlaced.tileIncrementsY;
                */
                
                //Changes the color of the tiles the moue is hovering over
                grid.ChangeColor(x, y, mouseSprite);
            }
            else
                CallManualTileClear();
        }

        if(x >= width || y >= height || x < 1 || y < 1)
        {
            CallManualTileClear();
        }


        //This changes the colors of the tiles the mouse sprite is hovering over
        
    }
    
}
