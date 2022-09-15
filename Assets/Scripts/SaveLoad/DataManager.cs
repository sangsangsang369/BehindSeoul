using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    JsonManager jsonManager;
    public SaveDataClass saveData;
    public static DataManager singleTon;

    void Awake()
    {
        if (singleTon == null)
        {
            singleTon = this;
            DontDestroyOnLoad(gameObject);
            //싱글톤이 지정되어있지 않다면. 이 객체(this)를 지정;
        }
        else
        {
            Destroy(gameObject);
            //싱글톤이 이미 지정되어있다면,(한번이라도 위의 코드가 발동했다는 의미) 게임오브젝트 파괴
        }
        //제이슨매니저 할당.
        jsonManager = new JsonManager();
        //load는 세이브데이터 로드다.
        saveData = new SaveDataClass();
    
        Load();
    }

    //세이브데이터 세이브
    public void Save()
    {
        jsonManager.SaveJson(saveData);
    }

    //데이터 로드
    public void Load()
    {
        saveData = jsonManager.LoadSaveData();
    }

    public void DataInitialize()
    {
        jsonManager.DataInitialize();
    }
}
