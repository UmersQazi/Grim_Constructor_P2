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
        if(toolToDrag != null)
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
                goodDeeds -= toolsOfLevel[0].cost;
                toolsOfLevel[0].amount--;
                break;
            case "BridgeB1_0":
                goodDeeds -= toolsOfLevel[1].cost;
                toolsOfLevel[1].amount--;
                break;
            case "BridgeC1_0":
                goodDeeds -= toolsOfLevel[2].cost;
                toolsOfLevel[2].amount--;
                break;
            case "Solider2_0":
                 goodDeeds -= toolsOfLevel[3].cost;
                toolsOfLevel[3].amount--;
                break;
            default:
                break;
        }
    }


}
