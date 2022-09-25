using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chatter : ChatData
{
    //chatter 리스트에 들어가는 미리보기 프리펩 스크립트
    public TMP_Text nameText, chatText;
    ChatManager chatMng;
    ChatRoom chatRoom;
    string chatterNameinChatter;

    DataManager data;
    SaveDataClass saveData;


    private void Start() 
    {
        chatMng = FindObjectOfType<ChatManager>();
        data = DataManager.singleTon;
        saveData = data.saveData;

        chatterNameinChatter = GetChatterName();
        
        int chatDataFirstNum = 0;
        for(int i = 0; i < chatMng.chatterNameList.Length; i++)
        {
            if(chatMng.chatterNameList[i] == chatterNameinChatter)
            {
                chatDataFirstNum = i+1;
                break;
            }
        }

        if(chatDataFirstNum == 1 && saveData.endedChatDataId_one.Count == 0)
        {
            SetChatterText(ChatData.chatDatasId);
        }
        else if(chatDataFirstNum == 2 && saveData.endedChatDataId_two.Count == 0)
        {
            SetChatterText(ChatData.chatDatasId);
        }
        else if(chatDataFirstNum == 3 && saveData.endedChatDataId_three.Count == 0)
        {
            SetChatterText(ChatData.chatDatasId);
        }
    }
    
    string GetChatterName()
    {
        chatMng = FindObjectOfType<ChatManager>();
        foreach(string cn in chatMng.chatterList.Keys)
        {
            if (chatMng.chatterList[cn] == this.gameObject)
            {
                return cn;
            }
        }
        return "null";
    }

    public void ChatRoomOn()
    {
        chatRoom = chatMng.chatRoomList[chatterNameinChatter].transform.GetComponent<ChatRoom>();
        chatMng.chatRoomList[chatterNameinChatter].SetActive(true);
        if(chatMng.clickedBychatAlarm)
        {
            chatMng.chatRoomList[chatterNameinChatter].GetComponent<ChatRoom>().GetBubble_Save();
            chatMng.clickedBychatAlarm = false;
        }
        chatRoom.ScrollDown();
    }

    public void SetChatterText(int id)
    {
        chatterNameinChatter = GetChatterName();
        nameText.text = chatterNameinChatter;
        chatText.text = GetChatDialogue(id,0).Substring(0,17).Replace("/n", " ").Replace("name", ChasaData.chasaName) + " ..."; 
    } 
}
