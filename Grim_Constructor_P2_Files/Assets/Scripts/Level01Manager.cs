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
    public GameObject toolToDrag;
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
            if (toolToDrag != null)
            {
                Destroy(toolToDrag);
                //gridAccess.CallManualTileClear();
            }
            string name = spriteToDrag.name;
            toolToDrag = new GameObject(name, typeof(SpriteRenderer));
            SpriteRenderer spriteRenderer = toolToDrag.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = spriteToDrag;
            spriteRenderer.sortingOrder = 26;
            toolToDrag.transform.localScale /= spriteDivider;
            string toolName;
            switch (name)
            {
                case "BridgeA1_0":
                    if (toolAmountsInLevel[0] > 0 )//&& (toolsOfLevel[0].cost-goodDeeds) > 0)
                        toolToBePlaced = toolsOfLevel[0];
                    break;
                case "BridgeB1_0":
                    if (toolAmountsInLevel[1] > 0 )//&& (toolsOfLevel[1].cost - goodDeeds) > 0)
                        toolToBePlaced = toolsOfLevel[1];
                    break;
                case "BridgeC1_0":
                    if (toolAmountsInLevel[2] > 0)//&& (toolsOfLevel[2].cost - goodDeeds) > 0)
                    {
                        Debug.Log(toolAmountsInLevel[2]);
                        toolToBePlaced = toolsOfLevel[2];
                    }else
                        toolToBePlaced = null;
                    break;
                case "Soldier2_0":
                    if (toolAmountsInLevel[3] > 0 )//&& (toolsOfLevel[3].cost - goodDeeds) > 0)
                        toolToBePlaced = toolsOfLevel[3];
                    break;
                default:
                    break;
            }
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
