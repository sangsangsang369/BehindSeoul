using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatAlarm : ChatData
{
    //챗 알람 프리펩에 들어가는 스크립트 
    public TMP_Text nameText, chatText;
    ChatManager chatMng;

    void Start() 
    {
        chatMng = FindObjectOfType<ChatManager>();
        //챗데이터가 100보다 작으면
        //새 대화방 생성하는 함수 넣기
        if(chatDatasId < 100)
        {
            this.gameObject.GetComponent<Button>().onClick.AddListener(ChatAlarmBtnFunc_NewChatter);
        }
        //기존 대화방 함수 넣기
        else
        {
            this.gameObject.GetComponent<Button>().onClick.AddListener(ChatAlarmBtnFunc_PrevChatter);
        }
        nameText.text = chatMng.chatAlarmParent.GetComponent<ChatAlarmParent>().chatterName;
        SetChatAlarmText(chatDatasId);
    }

    //챗 알람 미리보기 텍스트 설정
    void SetChatAlarmText(int id)
    { 
        chatText.text = GetChatDialogue(id,0).Substring(0,15).Replace("/n", " ").Replace("name", ChasaData.chasaName) + " ..."; 
    }


    void ChatAlarmBtnFunc_NewChatter()
    {
        //챗 알람 눌렀을 때 실행되는 함수
        //새로운 대화방 생성 
        //챗알람 부모한테 걸려있는 스크립트의 chatterName 가져와서 생성
        chatMng.StartChatwithChatter(chatMng.chatAlarmParent.GetComponent<ChatAlarmParent>().chatterName);
        chatMng.chatPage.SetActive(true);
        chatMng.nextBtnInChatPage.SetActive(true);
        Destroy(this.gameObject);
    }

    void ChatAlarmBtnFunc_PrevChatter()
    {
        //챗 알람 눌렀을 때 실행되는 함수
        //기존에 존재하던 대화방에 이어서 대화 나오도록
        chatMng.chatterList[chatMng.chatAlarmParent.GetComponent<ChatAlarmParent>().chatterName].transform.SetAsFirstSibling();
        chatMng.chatPage.SetActive(true);
        chatMng.chatRoomList[this.gameObject.transform.parent.GetComponent<ChatAlarmParent>().chatterName].GetComponent<ChatRoom>().GetBubble();
        chatMng.nextBtnInChatPage.SetActive(true);
        Destroy(this.gameObject);
    }
}
