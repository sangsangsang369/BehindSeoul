using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZukManager : MonoBehaviour
{
    [SerializeField]
    GameObject  zukPage,
                seokWrongText,
                o_zPrefab;

    [SerializeField]
    TMP_InputField  seokAnswerInput;
    int zukCurrPage = 0;
    GameManager gameMng;
    DataManager data;
    SaveDataClass saveData;

    void Start() 
    {  
        data = DataManager.singleTon;
        saveData = data.saveData;
        gameMng = FindObjectOfType<GameManager>();  
        zukPage.transform.GetChild(zukCurrPage).gameObject.SetActive(true);
    }

    public void GetOzInBag()
    {
        GameObject l = Instantiate(o_zPrefab);
        l.transform.SetParent(gameMng.bagOnlineContent.transform, false); 
        saveData.bagItems.Add("Letter_O_z");
        data.Save();
    }

    public void GoToNextZukPage()
    {
        zukPage.transform.GetChild(zukCurrPage).gameObject.SetActive(false);
        zukPage.transform.GetChild(++zukCurrPage).gameObject.SetActive(true);
    }
}
