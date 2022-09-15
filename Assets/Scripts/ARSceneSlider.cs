using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ARSceneSlider : MonoBehaviour
{
    Slider slTimer;
    float fSliderBarTime;
    bool minus = false;
    ARPrefab arPrefab;
    int successNum = 0;
    int heartIndex = 0;
    public List<GameObject> hearts = new List<GameObject>();

    GameManager gameMng;

    void Start()
    {
        slTimer = GetComponent<Slider>();
    }
 
    void Update()
    {
        if (slTimer.value < 1.0f && !minus)
        {
            slTimer.value += Time.deltaTime;
        }
        else if(slTimer.value > 0.0f && minus)
        {
            slTimer.value -= Time.deltaTime;
        }
        
        else if(slTimer.value >= 1.0f)
        {
            minus = true;
            slTimer.value -= Time.deltaTime;
        }
        else if(slTimer.value <= 0.0f)
        {
            minus = false;
            slTimer.value += Time.deltaTime;
        }
    }

    public void GetSliderValue()
    {
        if(slTimer.value > 0.3f && slTimer.value < 0.7f)
        {
            arPrefab = FindObjectOfType<ARPrefab>();
            arPrefab.Shake();
            arPrefab.ParticleOn();
            hearts[heartIndex++].SetActive(false);
            successNum++;
            if(successNum == 5)
            {
                gameMng = FindObjectOfType<GameManager>();
                gameMng.SuccessPopup.SetActive(true);
                gameMng.whenImgTracked.SetActive(false);
            }
        }
        else
        {
            
        }
    }
}
