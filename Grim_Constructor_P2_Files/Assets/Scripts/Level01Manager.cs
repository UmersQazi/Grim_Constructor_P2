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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goodDeedsText.text = goodDeeds.ToString();
    }

    public void ClickAndDrag(Sprite spriteToDrag)
    {
        string name = spriteToDrag.name;
        toolToDrag = new GameObject(name, typeof(SpriteRenderer));
        SpriteRenderer spriteRenderer = toolToDrag.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteToDrag;
        toolToDrag.transform.localScale /= 5;
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
