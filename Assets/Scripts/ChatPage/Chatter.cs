using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chatter : MonoBehaviour
{
    //chatter 리스트에 들어가는 미리보기 프리펩 스크립트
    public TMP_Text nameText, chatText;
    ChatManager chatMng;
    ChatRoom chatRoom;
    string chatterNameinChatter;


    private void Start() 
    {
        chatMng = FindObjectOfType<ChatManager>();

        chatterNameinChatter = GetChatterName();
        SetChatterText(ChatData.chatDatasId);
    }
    
    string GetChatterName()
    {
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
        chatMng.chatRoomList[chatterNameinChatter].SetActive(true);
        chatRoom.ScrollDown();
    }

    void SetChatterText(int id)
    {
        chatRoom = chatMng.chatRoomList[chatterNameinChatter].transform.GetComponent<ChatRoom>();
        nameText.text = chatterNameinChatter;
        chatText.text = chatRoom.GetChatDialogue(id,0).Substring(0,17).Replace("/n", " ").Replace("name", ChasaData.chasaName) + " ..."; 
    }
}
