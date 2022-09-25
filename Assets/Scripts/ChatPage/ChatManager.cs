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
    public string[] chatterNameList = {"설명 도깨비", "지원 도깨비", "정보 도깨비"};

    DataManager data;
    SaveDataClass saveData;

    public bool clickedBychatAlarm = false;


    private void Awake() 
    {
        data = DataManager.singleTon;
        saveData = data.saveData;

        if(saveData.endedChatDataId_one.Count > 0)
        {
            StartChatwithChatter("설명 도깨비");      
        }
        if(saveData.endedChatDataId_two.Count > 0)
        {
            StartChatwithChatter("지원 도깨비"); 
        }
        if(saveData.endedChatDataId_three.Count > 0)
        {
            StartChatwithChatter("정보 도깨비"); 
        }
        if(saveData.endedChatDataId_one.Count > 0)
        {
            for(int i = 0; i < saveData.endedChatDataId_one.Count; i++)
            {
                int _chatDataId = saveData.endedChatDataId_one[i]; 
                chatRoomList[chatterNameList[0]].GetComponent<ChatRoom>().GetbubbleAuto(_chatDataId);
            }
            chatterList[chatterNameList[0]].GetComponent<Chatter>().SetChatterText(saveData.endedChatDataId_one[saveData.endedChatDataId_one.Count -1]);
        }
        if(saveData.endedChatDataId_two.Count > 0)
        {
            for(int i = 0; i < saveData.endedChatDataId_two.Count; i++)
            {
                int _chatDataId = saveData.endedChatDataId_two[i]; 
                chatRoomList[chatterNameList[1]].GetComponent<ChatRoom>().GetbubbleAuto(_chatDataId);
            }
            chatterList[chatterNameList[1]].GetComponent<Chatter>().SetChatterText(saveData.endedChatDataId_two[saveData.endedChatDataId_two.Count -1]);
        }
        if(saveData.endedChatDataId_three.Count > 0)
        {
            for(int i = 0; i < saveData.endedChatDataId_three.Count; i++)
            {
                int _chatDataId = saveData.endedChatDataId_three[i]; 
                chatRoomList[chatterNameList[2]].GetComponent<ChatRoom>().GetbubbleAuto(_chatDataId);
            }
            chatterList[chatterNameList[2]].GetComponent<Chatter>().SetChatterText(saveData.endedChatDataId_three[saveData.endedChatDataId_three.Count -1]);
        } 
    }

    //새 대화방 생성
    //chatterList에 넘겨받은 스트링을 key로, 겜오브젝트를 value로 add하기
    public void StartChatwithChatter(string _chatterName)
    {
        GameObject chatter = Instantiate(ChatterPrefab);
        chatterList.Add(_chatterName, chatter);
        chatter.GetComponent<Chatter>().nameText.text = _chatterName;
        MakeChatRoom(_chatterName);
        chatter.transform.SetParent(chattersContent.transform, false); 
        //새로 생성된 대화방은 맨 위로 올리기
        chatter.transform.SetAsFirstSibling();
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
