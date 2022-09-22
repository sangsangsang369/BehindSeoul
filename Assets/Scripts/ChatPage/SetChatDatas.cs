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
        if(!isChatAlarmPop)
        {
            chatMng.chatAlarmParent.GetComponent<ChatAlarmParent>().chatterName = chatterNameInSetChat;
            chatMng.nextBtnInChatPage = NextBtnInThisPage;
            GameObject chatAlarm = Instantiate(chatMng.chatAlarmPrefab);
            chatAlarm.transform.SetParent(chatMng.chatAlarmParent.transform, false);
            isChatAlarmPop = true; 
            
            saveData.endedChatDataId.Add(chatId);
            data.Save();
        }
        
    }
}
