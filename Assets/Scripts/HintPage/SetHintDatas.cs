using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHintDatas : HintData
{
    public int  hintId;
    GameManager gameMng;

    void Start() 
    {
        gameMng = FindObjectOfType<GameManager>();

        gameMng.hintText.text = hintDatas[hintId];
        gameMng.hintBtn.SetActive(true);
    }
}
