using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public GameObject  chatPage, 
                chattersContent,
                chatRoomParent,
                ChatterPrefab,
                ChatRoomPrefab,
                chatterBubblePrefab,
                meBubblePrefab,
                replyBubblePrefab,
                chatAlarmPrefab,
                chatAlarmParent;
    public List<GameObject> chatterList,
                            chatRoomList;


    public void StartChatwithChatter()
    {
        GameObject chatter = Instantiate(ChatterPrefab);
        chatterList.Add(chatter);
        chatter.transform.SetParent(chattersContent.transform, false); 
        chatter.transform.SetAsFirstSibling();
        MakeChatRoom();
    }

    void MakeChatRoom()
    {
        GameObject chatRoom = Instantiate(ChatRoomPrefab);
        chatRoomList.Add(chatRoom);
        chatRoom.transform.SetParent(chatRoomParent.transform, false); 
        chatRoom.SetActive(false);
    }
}
