using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JungManager : MonoBehaviour
{
    [SerializeField]
    GameObject  jungPage,
                jungWrongText,
                nPrefab;

    [SerializeField]
    TMP_InputField  jungAnswerInput;
    GameManager gameMng;
    DataManager data;
    SaveDataClass saveData;
    int jungCurrPage = 0;

    void Start() 
    {     
        data = DataManager.singleTon;
        saveData = data.saveData;
        gameMng = FindObjectOfType<GameManager>();  
        jungPage.transform.GetChild(jungCurrPage).gameObject.SetActive(true);
    }

    public void GetOInBag()
    {
        GameObject l = Instantiate(nPrefab);
        l.transform.SetParent(gameMng.bagOnlineContent.transform, false); 
        saveData.bagItems.Add("Letter_N");
        data.Save();
    }

    public void GoToNextJungPage()
    {
        jungPage.transform.GetChild(jungCurrPage).gameObject.SetActive(false);
        jungPage.transform.GetChild(++jungCurrPage).gameObject.SetActive(true);
    }

    public void GoToPrevJungPage()
    {
        jungPage.transform.GetChild(jungCurrPage).gameObject.SetActive(false);
        jungPage.transform.GetChild(--jungCurrPage).gameObject.SetActive(true);
    }

    public void JungAnswerSubmitBtnFunc()
    {
        jungWrongText.SetActive(false);
        if(jungAnswerInput.text != "드므")
        {
            jungWrongText.SetActive(true);
        }
        else
        {
            gameMng.hintBtn.SetActive(false);
            GoToNextJungPage();
        }
    }
}
