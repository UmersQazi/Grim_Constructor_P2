using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolDisplay : MonoBehaviour
{
    public Tool toolOfButton;
    [SerializeField] Text costText, amountText;
    // Start is called before the first frame update
    void Start()
    {
        costText.text = toolOfButton.cost.ToString();
        amountText.text = toolOfButton.amount.ToString();
        //nameText.text = toolOfButton.name.ToString();
    }

}
