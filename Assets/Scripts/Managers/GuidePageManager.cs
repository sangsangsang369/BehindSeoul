using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuidePageManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField  nameInput,
                    answer1Input,
                    answer2Input;
    
    [SerializeField]
    TMP_Text    textOne,
                textTwo,
                textThree;

    [SerializeField]
    GameObject  guidePagesParent,
                doorNextBtn,
                horiPrefab,
                lockerPrefab,
                bagOnlineContent,
                wrong1Text,
                wrong2Text;
    public string chasaName;
    int knockingCount = 0;
    GameManager gameMng;   
    [SerializeField]
    List<TMP_Text>  nameContainTexts; 
    public int guideCurrPage = 0;
    public int nextBtnIndex = 0;

    DataManager data;
    SaveDataClass saveData;
    
    private void Awake() 
    {
        gameMng = FindObjectOfType<GameManager>(); 
        data = DataManager.singleTon;
        saveData = data.saveData;
    }

    void Start() 
    {
        guidePagesParent.transform.GetChild(guideCurrPage).gameObject.SetActive(true); 
        nameInput.characterLimit = 14;  
    }

    public void NameSubmitBtnFunc()
    {
        ChasaData.chasaName = nameInput.text;
        chasaName = nameInput.text;
        foreach (TMP_Text t in nameContainTexts)
        {
            t.text = t.text.Replace("name", nameInput.text);
        }
    }

    public void GoToNextGuidePage()
    {
        guidePagesParent.transform.GetChild(guideCurrPage).gameObject.SetActive(false);
        guidePagesParent.transform.GetChild(++guideCurrPage).gameObject.SetActive(true);
    }

    public void GoToPrevGuidePage()
    {
        guidePagesParent.transform.GetChild(guideCurrPage).gameObject.SetActive(false);
        guidePagesParent.transform.GetChild(--guideCurrPage).gameObject.SetActive(true);
    }

    public void Answer1SubmitBtnFunc()
    {
        wrong1Text.SetActive(false);
        if(answer1Input.text != "괴물의 영혼")
        {
            wrong1Text.SetActive(true);
        }
        else
        {
            GoToNextGuidePage();
        }
    }

    public void Answer2SubmitBtnFunc()
    {
        wrong2Text.SetActive(false);
        if(answer2Input.text != "숭례문")
        {
            wrong2Text.SetActive(true);
        }
        else
        {
            GoToNextGuidePage();
        }
    }

    public void DoorImgFunc()
    {
        if(++knockingCount >= 5)
        {
            doorNextBtn.SetActive(true);
        }
    }

    public void GetHorirInBag()
    {
        GameObject locker = Instantiate(horiPrefab);
        locker.transform.SetParent(bagOnlineContent.transform, false); 
        
    }

    public void GetLockerInBag()
    {
        GameObject locker = Instantiate(lockerPrefab);
        locker.transform.SetParent(bagOnlineContent.transform, false);
        
    }

    public void GuidePageSaveData()
    {
        saveData.pagesIndex = 1;
        saveData.bagItems.Add("Hori");
        saveData.bagItems.Add("Locker");
        data.Save();
    }
}
