using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
    BagManager bagMng;
    void Start()
    {
        bagMng = FindObjectOfType<BagManager>();
    }

    public void itemInfoPageOn()
    {
        bagMng.itemInfoPage.SetActive(true);
    }
    
}
