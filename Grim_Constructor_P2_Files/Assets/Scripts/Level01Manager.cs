using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Level01Manager : MonoBehaviour
{
    public int goodDeeds = 105;
    [SerializeField] Text goodDeedsText;
    public GameObject toolToDrag;
    [SerializeField] float spriteDivider;
    [SerializeField] TestUse gridAccess;

    [Header("Available Tools")]
    [SerializeField] Tool[] toolsOfLevel;
    public Tool toolToBePlaced;

    // Start is called before the first frame update
    void Start()
    {
        
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
                    toolToBePlaced = toolsOfLevel[0];
                    break;
                case "BridgeB1_0":
                    toolToBePlaced = toolsOfLevel[1];
                    break;
                case "BridgeC1_0":
                    toolToBePlaced = toolsOfLevel[2];
                    break;
                case "Solider2_0":
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
        toolToPlace.amount--;
    }


}
