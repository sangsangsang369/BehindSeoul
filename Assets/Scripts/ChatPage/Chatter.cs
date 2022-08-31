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
    int chatNum;


    private void Start() 
    {
        chatMng = FindObjectOfType<ChatManager>();

        chatNum = GetChatNum();
        SetChatterText(ChatData.chatDatasId);
    }
    
    int GetChatNum()
    {
        for(int i = 0; i < chatMng.chatterList.Count; i++)
        {
            if (chatMng.chatterList[i] == this.gameObject)
            {
                return i;
            }
        }
        return 11;
    }

    public void ChatRoomOn()
    {
        chatMng.chatRoomList[chatNum].SetActive(true);
    }

    void SetChatterText(int id)
    {
        chatRoom = chatMng.chatRoomList[chatNum].transform.GetComponent<ChatRoom>();
        nameText.text = chatRoom.goblinNames[ChatData.goblinNamesIndex];
        chatText.text = chatRoom.GetChatDialogue(id,0).Substring(0,17).Replace("/n", " ").Replace("name", ChasaData.chasaName) + " ..."; 
    }
}
