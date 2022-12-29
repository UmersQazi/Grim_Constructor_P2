using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestUse : MonoBehaviour
{
    private Grid grid;

    public int width, height, cellSize;
    public Vector3 origin = new Vector3(0,0,0);
    public Sprite square;
    [SerializeField] Color colorOfLines;
    public Level01Manager level01Manager;
    GameObject toolToDrag;
    



    public int roundedMousePos;
    public GameObject mouseSprite, mouseSprite2, orignalSprite; 
    public Vector3 mousePosition;
    public bool clicked;
    
    public GameObject loadBar;
    LoadBar loadBarScript;

    public GameObject crateSprite;
    GameObject spriteToBuild;
    GameObject crateToEnd;
    public Villager villager;

    public AudioSource moveSound, buildingSound, buildingCompleteSound;
    Vector3 mousePos, originalMousePos;
    bool builtSoundPlayed, completeSoundPlayed, particlePlayed;

    public ParticleSystem particleSystem, particleSystemUsed;
    
    // Start is called before the first frame update
    void Start()
    {
        //loadBarScript = loadBar.GetComponent<LoadBar>();
        grid = new Grid(width, height, cellSize, origin, square, colorOfLines);
        //clicked = false;
        //orignalSprite = mouseSprite;
        originalMousePos = mousePos;
    }

    // Update is called once per frame
    void Update()
    {
        mouseSprite = level01Manager.toolToDrag;
        if(mousePos != originalMousePos)
        {
            
            //moveSound.Play();
            originalMousePos = mousePos;
        }
        mousePosition = UtilsClass.GetMouseWorldPosition();
        roundedMousePos = grid.GetValue(mousePosition);

        //If object has not been placed, move the sprite with mouse
        if(!clicked && mouseSprite != null)
            MoveSprite();





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

    //Moves mouse sprite
    public void MoveSprite()
    {
        int x, y;
        //moveSound.Play(); CHANGE

        //Rounds the current mouse position
        grid.GetXY(mousePosition, out x, out y);

        //This rounded position is what lets the mouse sprite move across the scene in a rigid manner
        //If i need it off center, remove .5 increments
        mouseSprite.transform.position = new Vector3(x, y, 0)*cellSize + origin;

        //This changes the colors of the tiles the mouse sprite is hovering over
        //grid.ChangeColor(x, y);
    }
    
}
