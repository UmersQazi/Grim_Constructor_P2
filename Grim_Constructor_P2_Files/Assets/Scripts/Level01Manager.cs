using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class Level01Manager : MonoBehaviour
{
    public int goodDeeds = 105;
    [SerializeField] Text goodDeedsText;
    public GameObject toolSprite;
    [SerializeField] float spriteDivider;
    [SerializeField] TestUse gridAccess;

    [Header("Grid Presets")]
    [SerializeField] GameObject[] presetSprites;

    [Header("Available Tools")]
    public Tool[] toolsOfLevel;
    public int[] toolAmountsInLevel;
    public Tool toolToBePlaced;
    public int toolAmountIndex;
    //[SerializeField] string[] spriteNames;


    // Start is called before the first frame update
    void Start()
    {
        //Finds the number of tools the player can use in the current level
        toolAmountsInLevel = new int[toolsOfLevel.Length];

        //Collects the amounts of each tool available in the level for the player in an array
        for(int i = 0; i < toolsOfLevel.Length; i++)
        {
            toolAmountsInLevel[i] = toolsOfLevel[i].amount;
        }

        //Sets the sprite positions of sprites that are already in the level at the beginning on the grid
        foreach(GameObject g in presetSprites)
        {
            CallSetSpritePos(g);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Sets text for player to see the number of GD they can spend
        goodDeedsText.text = "Good Deeds:" + goodDeeds.ToString();
    }

    
    void CallSetSpritePos(GameObject spriteObj)
    {
        gridAccess.SetSpritePos(spriteObj);
    }

    //A Button event that sets the sprite the user wants to drag
    public void ClickAndDrag(Sprite spriteToDrag)
    {
        if (goodDeeds > 0)
        {
            if (toolSprite != null)
            {
                Destroy(toolSprite);
                //gridAccess.CallManualTileClear();
            }
            string name = spriteToDrag.name;
            
            switch (name)
            {
                case "BridgeA1_0":
                    ToolSpriteCreator(name, 0, spriteToDrag);
                    break;
                case "BridgeB1_0":
                    ToolSpriteCreator(name, 1, spriteToDrag);
                    break;
                case "BridgeC1_0":
                    ToolSpriteCreator(name, 2, spriteToDrag);
                    break;
                case "Soldier2_0":
                    ToolSpriteCreator(name, 3, spriteToDrag);
                    break;
                default:
                    break;
            }
        }
    }

    //
    public void ToolSpriteCreator(string name, int index, Sprite spriteToDrag)
    {
        //As long as the amount of a tool is more than 0 and the player can purchase it, the player can use
        //the sprite of that tool to move it around the grid and place it 
        if (toolAmountsInLevel[index] > 0 && (goodDeeds - toolsOfLevel[index].cost) >= 0)
        {
            Debug.Log(toolAmountsInLevel[index]);
            toolToBePlaced = toolsOfLevel[index];
            toolSprite = new GameObject(name, typeof(SpriteRenderer));
            SpriteRenderer spriteRenderer = toolSprite.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = spriteToDrag;
            spriteRenderer.sortingOrder = 26;
            toolSprite.transform.localScale /= spriteDivider;
        }
        //Otherwise, the player cannot use that tool
        else if (toolAmountsInLevel[index] < 0 || toolAmountsInLevel[index] == 0)
        {
            Debug.Log("Not enough!");
            toolToBePlaced = null;
            toolSprite = null;
        }
    }


    //Deducts the tool amount and the amount of GD from the player
    public void ToolDeduction(Tool toolToPlace)
    {
        goodDeeds -= toolToPlace.cost;

        int index = 0;

        foreach (Tool t in toolsOfLevel)
        {
            index++;
            if(t == toolToPlace)
            {
                break;
            }
        }
        toolAmountIndex = index-1;
        toolAmountsInLevel[index-1]--;
    }

    


}
