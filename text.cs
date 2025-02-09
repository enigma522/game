using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Required for UI Image
using TMPro;

public class TextWithBackground : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public Image backgroundImage; 

    private string[] lines;

    void Start()
    {
        lines = textComponent.text.Split('\n'); 
        textComponent.text = "";
        StartCoroutine(DisplayTexts());
    }

    IEnumerator DisplayTexts()
    {
        foreach (string message in lines)
        {
            textComponent.text = message;
            backgroundImage.enabled = true; 
            yield return new WaitForSeconds(3);
        }
        textComponent.text = "";
        backgroundImage.enabled = false; 

    }
}
