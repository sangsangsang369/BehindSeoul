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
    public List<int> endedChatDataId;



    public SaveDataClass()
    {
        pagesIndex = 0;
        pageChildIndex = 0;
        
        bagItems = new List<string>();
        bagCoupons = new List<string>();
        spotCollection = new List<string>();
        endedSixSpots = new List<int>();
        endedChatDataId = new List<int>();
    }
}
