using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bubble : MonoBehaviour
{
    //말풍선 프리펩에 들어가는 스크립트
    public TMP_Text nameText, chatText;
    public Image UserImage;

    private void Start() 
    {
        this.gameObject.transform.localScale = new Vector3(1f,1f,1f);    
    }
}
