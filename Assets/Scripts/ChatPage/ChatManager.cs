using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatManager : MonoBehaviour
{
    //채팅 총괄하는 매니저 스크립트

    public GameObject  chatPage, 
                chattersContent,
                chatRoomParent,
                ChatterPrefab,
                ChatRoomPrefab,
                chatterBubblePrefab,
                meBubblePrefab,
                replyBubblePrefab,
                chatAlarmPrefab,
                chatAlarmParent,
                nextBtnInChatPage;
    //딕셔너리<변수 이름, 게임 오브젝트>
    public Dictionary<string, GameObject>  chatterList = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject>  chatRoomList = new Dictionary<string, GameObject>();


    private void Start() 
    {
        //대화방 생성하고 대화방 창 생성하고 그 안에 채팅 생성하기
        
    }

    //새 대화방 생성
    //chatterList에 넘겨받은 스트링을 key로, 겜오브젝트를 value로 add하기
    public void StartChatwithChatter(string _chatterName)
    {
        GameObject chatter = Instantiate(ChatterPrefab);
        chatterList.Add(_chatterName, chatter);
        chatter.transform.SetParent(chattersContent.transform, false); 
        //새로 생성된 대화방은 맨 위로 올리기
        chatter.transform.SetAsFirstSibling();
        MakeChatRoom(_chatterName);
    }

    //새 대화방 창 생성 (생성 후 꺼두기)
    void MakeChatRoom(string chatRoomName)
    {
        GameObject chatRoom = Instantiate(ChatRoomPrefab);
        chatRoomList.Add(chatRoomName, chatRoom);
        chatRoom.transform.SetParent(chatRoomParent.transform, false); 
        chatRoom.SetActive(false);
    }
}
