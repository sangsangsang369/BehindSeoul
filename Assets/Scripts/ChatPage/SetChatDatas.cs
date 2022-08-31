using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChatDatas : MonoBehaviour
{
    public int  chatId,
                nameIndex;
    ChatManager chatMng;

    
    void Awake() 
    {
        ChatData.chatDatasId = chatId; 
        ChatData.goblinNamesIndex = nameIndex;

        chatMng = FindObjectOfType<ChatManager>();

        PopupChatAlarm();
    }

    void PopupChatAlarm()
    {
        GameObject chatAlarm = Instantiate(chatMng.chatAlarmPrefab);
        chatAlarm.transform.SetParent(chatMng.chatAlarmParent.transform, false); 
    }
}
