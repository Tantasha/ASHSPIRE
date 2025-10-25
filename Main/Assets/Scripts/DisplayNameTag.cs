using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DisplayNameTag : MonoBehaviour
{
    public string NameTag;
    
    public GameObject inputField;
    public GameObject textDisplay;
    
    public void StoreName()
    {
        NameTag = inputField.GetComponent<Text>().text;
        textDisplay.GetComponent<Text>().text = NameTag;
    }
}
