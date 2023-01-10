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

    [Header("Available Tools")]
    public Tool[] toolsOfLevel;
    public int[] toolAmountsInLevel;
    public Tool toolToBePlaced;
    public int toolAmountIndex;



    // Start is called before the first frame update
    void Start()
    {
        toolAmountsInLevel = new int[toolsOfLevel.Length];
        for(int i = 0; i < toolsOfLevel.Length; i++)
        {
            toolAmountsInLevel[i] = toolsOfLevel[i].amount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        goodDeedsText.text = "Good Deeds:" + goodDeeds.ToString();
    }

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

    public void ToolSpriteCreator(string name, int index, Sprite spriteToDrag)
    {
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
        else if (toolAmountsInLevel[index] < 0 || toolAmountsInLevel[index] == 0)
        {
            Debug.Log("Not enough!");
            toolToBePlaced = null;
            toolSprite = null;
        }
    }


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
