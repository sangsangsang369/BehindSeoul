using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintData : MonoBehaviour
{
    public List<string> hintDatas;

    void Awake() 
    {
        GenerateData();    
    }

    void GenerateData()
    {
        hintDatas.Add("숭례문 현판의 특징을 생각해서 퍼즐을 다시 봐볼까요?");  
    }
}