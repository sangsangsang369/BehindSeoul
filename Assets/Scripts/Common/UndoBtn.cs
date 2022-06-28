using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoBtn : MonoBehaviour
{
    [SerializeField]
    GameObject prePage, thisPage;

    public void UndoBtnFunc()
    {
        thisPage.SetActive(false);
        prePage.SetActive(true);
    }
}
