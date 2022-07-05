using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoBtn : MonoBehaviour
{
    //뒤로가기 버튼

    [SerializeField]
    GameObject prePage, thisPage;

    public void UndoBtnFunc()
    {
        thisPage.SetActive(false);
        prePage.SetActive(true);
    }
}
