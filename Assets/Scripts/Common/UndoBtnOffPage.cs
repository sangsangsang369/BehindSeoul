using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoBtnOffPage : MonoBehaviour
{
    [SerializeField]
    GameObject undoBtn;
    [SerializeField]
    int setActiveBool = 0;

    private void Start() 
    {
        if(setActiveBool == 1)
        {
            undoBtn.SetActive(false);
        }
        else
        {
            undoBtn.SetActive(true);
        }
    }
}
