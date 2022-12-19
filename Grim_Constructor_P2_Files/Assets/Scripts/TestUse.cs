using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestUse : MonoBehaviour
{
    private Grid grid;

    public int width, height, cellSize;
    public Vector3 origin = new Vector3(0,0,0);

    public int roundedMousePos;
    public GameObject mouseSprite, mouseSprite2, orignalSprite; 
    public Vector3 mousePosition;
    public bool clicked;
    public Sprite square;
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
        loadBarScript = loadBar.GetComponent<LoadBar>();
        grid = new Grid(width, height, cellSize, origin, square);
        clicked = false;
        orignalSprite = mouseSprite;
        originalMousePos = mousePos;
    }

    // Update is called once per frame
    void Update()
    {
        if(mousePos != originalMousePos)
        {
            
            moveSound.Play();
            originalMousePos = mousePos;
        }
        mousePosition = UtilsClass.GetMouseWorldPosition();
        roundedMousePos = grid.GetValue(mousePosition);
        if(!clicked)
            MoveSprite();
        if (Input.GetMouseButtonDown(0) && !loadBarScript.placing)
        {
            if (grid.GetValue(mousePosition) == 0 && mouseSprite == orignalSprite)
            {
                if (!builtSoundPlayed)
                {
                    buildingSound.Play();
                    builtSoundPlayed = true;
                }
                 spriteToBuild = mouseSprite;
                grid.SetValue(mousePosition, 1);
                clicked = true;
                grid.ClearGreen();
                
                mouseSprite = mouseSprite2;
                spriteToBuild.SetActive(false);
                mouseSprite.SetActive(true);
                clicked = false;
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
        if (Input.GetMouseButton(1))
        {
            Debug.Log(grid.GetValue(mousePosition));
        }

        TurnOffLoad();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }



    }

    public void TurnOffLoad()
    {
        if (loadBarScript.placed)
        {
            loadBar.SetActive(false);
            Debug.Log("Turned off loading!");
            spriteToBuild.SetActive(true);
            crateToEnd.SetActive(false);
            if (!completeSoundPlayed)
            {
                buildingCompleteSound.Play();
                completeSoundPlayed = true;
            }
            if (!particlePlayed)
            {
                particleSystemUsed.Play();
                particlePlayed = true;
            }
        }
    }


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

    public void MoveSprite()
    {
        int x, y;
        //moveSound.Play();
        grid.GetXY(mousePosition, out x, out y);
        //If i need it off center, remove .5 increments
        mouseSprite.transform.position = new Vector3(x, y, 0)*cellSize + origin;
        grid.ChangeColor(x, y);
    }

}
