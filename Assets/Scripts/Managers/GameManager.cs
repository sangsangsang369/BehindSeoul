using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject   sungPage,
                        deokPage,
                        gwangPage,
                        hamPage,
                        seokPage,
                        zukPage,
                        junPage,
                        jungPage,
                        isangPage,
                        tongInPage,
                        parkNoSuPage,
                        canvasCam,
                        mapScene,
                        upperBarBtns,
                        hintBtn;
    public TMP_Text     hintText;
    public List<GameObject> nextBtnsAfterCheckedChat;
    public int nextBtnIndex = 0;

}