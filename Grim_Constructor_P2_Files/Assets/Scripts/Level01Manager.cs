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
        switch (name)
        {
            case "Bridge A":
                break;
            case "Bridge B":
                break;
            case "Bridge C":
                break;
            case "Solider":
                break;
            default:
                break;
        }
    }


}
