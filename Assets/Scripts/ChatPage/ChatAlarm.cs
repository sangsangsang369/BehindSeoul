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
    DataManager data;
    SaveDataClass saveData;

    void Start() 
    {
        chatMng = FindObjectOfType<ChatManager>();
        data = DataManager.singleTon;
        saveData = data.saveData;

        //챗데이터가 100보다 작으면 새 대화방 생성하는 함수 넣기
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
        chatMng.StartChatwithChatter(chatMng.chatAlarmParent.GetComponent<ChatAlarmParent>().chatterName);
        chatMng.chatPage.SetActive(true);

        chatMng.nextBtnInChatPage.SetActive(true);
        Destroy(this.gameObject);
    }

    void ChatAlarmBtnFunc_PrevChatter()
    {
        chatMng.chatterList[chatMng.chatAlarmParent.GetComponent<ChatAlarmParent>().chatterName].transform.SetAsFirstSibling();
        chatMng.chatPage.SetActive(true);
        SetChatterTextFromAlarm();

        chatMng.clickedBychatAlarm = true;

        chatMng.nextBtnInChatPage.SetActive(true);
        Destroy(this.gameObject);
    }

    public void SetChatterTextFromAlarm()
    {
        chatMng.chatterList[chatMng.chatAlarmParent.GetComponent<ChatAlarmParent>().chatterName].GetComponent<Chatter>().chatText.text
        = GetChatDialogue(chatDatasId,0).Substring(0,17).Replace("/n", " ").Replace("name", ChasaData.chasaName) + " ..."; 
    }
}
