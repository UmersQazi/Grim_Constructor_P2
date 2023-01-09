using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolDisplay : MonoBehaviour
{
    public Tool toolOfButton;
    [SerializeField] Text costText, amountText;
    [SerializeField] Level01Manager levelManager;
    int toolIndex;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Tool t in levelManager.toolsOfLevel)
        {
            toolIndex++;
            if (t == toolOfButton) {
                break;
            }
        }
        //nameText.text = toolOfButton.name.ToString();
    }

    private void Update()
    {
        costText.text = toolOfButton.cost.ToString();
        amountText.text = levelManager.toolAmountsInLevel[toolIndex-1].ToString();
        
    }


}
