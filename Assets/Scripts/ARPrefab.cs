using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPrefab : MonoBehaviour
{
    private Vector3 originPosition; 
    private Quaternion originRotation; 

    private float shake_decay = 0.01f; 
    private float shake_intensity; 
    private float coef_shake_intensity = 0.1f; 
    public bool playAura = false; //파티클 제어 bool
    public ParticleSystem particleObject; //파티클시스템
    GameManager gameMng;

    private void Awake() 
    {
        gameMng = FindObjectOfType<GameManager>();
        gameMng.whenImgTracked.SetActive(true);
    }

    void Update()
    { 
        
        if (shake_intensity > 0) 
        { 
            this.gameObject.transform.position = originPosition + Random.insideUnitSphere * shake_intensity; 
            this.gameObject.transform.transform.rotation = new Quaternion( 
                                originRotation.x + Random.Range(-shake_intensity, shake_intensity) * 0.02f, 
                                originRotation.y + Random.Range(-shake_intensity, shake_intensity) * 0.02f, 
                                originRotation.z + Random.Range(-shake_intensity, shake_intensity) * 0.02f, 
                                originRotation.w + Random.Range(-shake_intensity, shake_intensity) * 0.02f); 
            shake_intensity -= shake_decay; 
        } 

        if(particleObject.gameObject.activeSelf && !particleObject.isPlaying) 
        {
            particleObject.gameObject.SetActive(false);
        }
    }

    public void Shake() 
    { 
        originPosition = this.gameObject.transform.position; 
        originRotation = this.gameObject.transform.rotation; 
        shake_intensity = coef_shake_intensity;  
        playAura = true;
    }

    public void ParticleOn()
    {                   
        if(particleObject.gameObject.activeSelf == false)
        {
            particleObject.gameObject.SetActive(true);
        }
    }
}
