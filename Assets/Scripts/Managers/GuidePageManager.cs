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
    int currPage = 0;
    int knockingCount = 0;
    

    void Start() 
    {
        nameInput.characterLimit = 14;    
    }

    public void NameSubmitBtnFunc()
    {
        ChasaData.chasaName = nameInput.text;
        chasaName = nameInput.text;
        textOne.text = textOne.text.Replace("name", chasaName);
        textThree.text = textThree.text.Replace("name", chasaName);
    }

    public void GoToNextGuidePage()
    {
        guidePagesParent.transform.GetChild(currPage).gameObject.SetActive(false);
        guidePagesParent.transform.GetChild(++currPage).gameObject.SetActive(true);
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
}
