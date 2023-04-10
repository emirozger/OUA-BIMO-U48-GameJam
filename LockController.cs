using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : MonoBehaviour
{
    private int[] result, correctCombination;
    public bool canTakeGreenButton = false;


    private void Start()
    {
        Cursor.visible=false;
        Cursor.lockState=CursorLockMode.Locked;
        result = new int[] { 5, 5, 5 };
        correctCombination = new int[] { 7, 7, 7 };
        Rotate.Rotated += CheckResults;
    }
    private void Update()
    {
        Debug.Log(result[0]);
        Debug.Log(result[1]);
        Debug.Log(result[2]);
        CheckResults("wheel1", result[0]);
        CheckResults("wheel2", result[1]);
        CheckResults("wheel3", result[2]);

    }

    private void CheckResults(string wheelName, int number)
    {
        switch (wheelName)
        {
            case "wheel1":        
                result[0] = number;
                Debug.Log(number);
                break;
            case "wheel2":
                result[1] = number;
                Debug.Log(number);
                break;
            case "wheel3":
                result[2] = number;
                Debug.Log(number);
                break;
        }

        if (result[0] == correctCombination[0] && result[1] == correctCombination[1] && result[2] == correctCombination[2])
        {
            canTakeGreenButton=true;
            Debug.Log("Opened!");
        }
    }

    private void OnDestroy()
    {
        Rotate.Rotated -= CheckResults;
    }
}