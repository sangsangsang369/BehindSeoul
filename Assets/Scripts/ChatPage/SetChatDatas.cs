using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChatDatas : MonoBehaviour
{
    //챗 알람 띄우는 페이지에 들어가는 스크립트
    public int  chatId;
    public string chatterNameInSetChat;
    ChatManager chatMng;
    bool isChatAlarmPop = false;
    public bool isThisChatIdInHistory = false;

    public GameObject NextBtnInThisPage;
    
    DataManager data;
    SaveDataClass saveData;
    
    void Awake() 
    {
        ChatData.chatDatasId = chatId; 

        chatMng = FindObjectOfType<ChatManager>();
        data = DataManager.singleTon;
        saveData = data.saveData;
        
        PopupChatAlarm();
    }

    void PopupChatAlarm()
    {
        foreach(int i in saveData.endedChatDataId_one)
        {
            if(i == chatId)
            {
                isThisChatIdInHistory = true;
                break;
            }
        }
        foreach(int i in saveData.endedChatDataId_two)
        {
            if(i == chatId)
            {
                isThisChatIdInHistory = true;
                break;
            }
        }
        foreach(int i in saveData.endedChatDataId_three)
        {
            if(i == chatId)
            {
                isThisChatIdInHistory = true;
                break;
            }
        }
        if(!isChatAlarmPop && !isThisChatIdInHistory)
        {
            chatMng.chatAlarmParent.GetComponent<ChatAlarmParent>().chatterName = chatterNameInSetChat;
            chatMng.nextBtnInChatPage = NextBtnInThisPage;
            GameObject chatAlarm = Instantiate(chatMng.chatAlarmPrefab);
            chatAlarm.transform.SetParent(chatMng.chatAlarmParent.transform, false);
            isChatAlarmPop = true; 
        } 
        else
        {
            NextBtnInThisPage.SetActive(true);
        }
    }
}
