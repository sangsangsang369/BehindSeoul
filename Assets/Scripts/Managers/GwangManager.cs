using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GwangManager : MonoBehaviour
{
    [SerializeField]
    GameObject  gwangPage,
                gwangWrongText,
                kPrefab;

    [SerializeField]
    TMP_InputField  gwangAnswerInput;
    GameManager gameMng;
    int gwangCurrPage = 0;
    DataManager data;
    SaveDataClass saveData;


    void Start()  
    {   
        data = DataManager.singleTon;
        saveData = data.saveData;
        gameMng = FindObjectOfType<GameManager>();  
        gwangPage.transform.GetChild(gwangCurrPage).gameObject.SetActive(true);
    }

    public void GetKInBag()
    {
        GameObject l = Instantiate(kPrefab);
        l.transform.SetParent(gameMng.bagOnlineContent.transform, false); 
        saveData.bagItems.Add("Letter_K");
        data.Save();
    }

    public void GoToNextGwangPage()
    {
        gwangPage.transform.GetChild(gwangCurrPage).gameObject.SetActive(false);
        gwangPage.transform.GetChild(++gwangCurrPage).gameObject.SetActive(true);
    }

    public void GoToPrevGwangPage()
    {
        gwangPage.transform.GetChild(gwangCurrPage).gameObject.SetActive(false);
        gwangPage.transform.GetChild(--gwangCurrPage).gameObject.SetActive(true);
    }

    public void GwangAnswerSubmitBtnFunc()
    {
        gwangWrongText.SetActive(false);
        if(gwangAnswerInput.text != "1904")
        {
            gwangWrongText.SetActive(true);
        }
        else
        {
            gameMng.hintBtn.SetActive(false);
            GoToNextGwangPage();
        }
    }
}
