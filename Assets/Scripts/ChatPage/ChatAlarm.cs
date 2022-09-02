using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatAlarm : ChatData
{
    public TMP_Text nameText, chatText;
    ChatManager chatMng;


    void Start() 
    {
        chatMng = FindObjectOfType<ChatManager>();
        if(chatDatasId < 100)
        {
            this.gameObject.GetComponent<Button>().onClick.AddListener(ChatAlarmBtnFunc_NewChatter);
        }
        else
        {
            this.gameObject.GetComponent<Button>().onClick.AddListener(ChatAlarmBtnFunc_PrevChatter);
        }
        nameText.text = goblinNames[goblinNamesIndex];
        SetChatAlarmText(chatDatasId);
    }

    void SetChatAlarmText(int id)
    { 
        chatText.text = GetChatDialogue(id,0).Substring(0,15).Replace("/n", " ").Replace("name", ChasaData.chasaName) + " ..."; 
    }

    void ChatAlarmBtnFunc_NewChatter()
    {
        chatMng.StartChatwithChatter();
        chatMng.chatPage.SetActive(true);
        Destroy(this.gameObject);
    }

    void ChatAlarmBtnFunc_PrevChatter()
    {
        chatMng.chatPage.SetActive(true);
        chatMng.chatRoomList[ChatData.goblinNamesIndex].GetComponent<ChatRoom>().GetBubbleText();
        Destroy(this.gameObject);
    }
}
