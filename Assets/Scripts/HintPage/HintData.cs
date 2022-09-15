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
        hintDatas.Add("사진과 같은 독을 찾고 설명서를 찾아보세요!"); 
        hintDatas.Add("정보 도깨비는 한명이 아니랍니다.");
        hintDatas.Add("불을 꺼서 나온 숫자는?");

    }
}
