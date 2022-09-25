using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveDataClass 
{
    public int pagesIndex, pageChildIndex;  
    public List<string> bagItems;
    public List<string> bagCoupons;
    public List<string> spotCollection;
    public List<int> endedSixSpots;
    public List<int> endedChatDataId_one;
    public bool ischatOneHavePrev;
    public List<int> endedChatDataId_two;
    public bool ischatTwoHavePrev;
    public List<int> endedChatDataId_three;
    public bool ischatThreeHavePrev;



    public SaveDataClass()
    {
        pagesIndex = 0;
        pageChildIndex = 0;
        
        bagItems = new List<string>();
        bagCoupons = new List<string>();
        spotCollection = new List<string>();
        endedSixSpots = new List<int>();
        endedChatDataId_one = new List<int>();
        ischatOneHavePrev = false;
        endedChatDataId_two = new List<int>();
        ischatTwoHavePrev = false;
        endedChatDataId_three = new List<int>();
        ischatThreeHavePrev = false;

    }
}
