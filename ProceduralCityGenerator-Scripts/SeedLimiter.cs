using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SeedLimiter : MonoBehaviour
{
    //This method is a simple Try Parse that checks if the input is within the range of an int32. If not then the input is set to default
    public void CheckValidSeed()
    {
        TMP_InputField TInput = GetComponent<TMP_InputField>();
        int input;
        if(!int.TryParse(TInput.text, out input))
        {
            TInput.text = "314159265";
        }
    }
    public void IterationLimit()
    {
        TMP_InputField TInput = GetComponent<TMP_InputField>();
        int input = int.Parse(TInput.text);
        if(input > 10 || input < 1)
        {
            TInput.text = "1";
        }
    }
    public void RoadLimit()
    {
        TMP_InputField TInput = GetComponent<TMP_InputField>();
        int input = int.Parse(TInput.text);
        if(input > 150 || input < 1)
        {
            TInput.text = "1";
        }
    }
    public void SizeLimit()
    {
        TMP_InputField TInput = GetComponent<TMP_InputField>();
        int input = int.Parse(TInput.text);
        if(input > 100 || input < 1)
        {
            TInput.text = "5";
        }
    }

    public void Quit(){
        Application.Quit();
    }
}
