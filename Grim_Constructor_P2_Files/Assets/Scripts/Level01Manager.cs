using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Level01Manager : MonoBehaviour
{
    public int goodDeeds = 105;
    [SerializeField] Text goodDeedsText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goodDeedsText.text = goodDeeds.ToString();
    }

    public void ClickAndDrag(SpriteRenderer spriteToDrag)
    {

    }


}
