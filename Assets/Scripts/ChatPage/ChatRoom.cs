using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatRoom : ChatData
{
    //대화방 창 프리펩에 들어가는 스크립트

    [HideInInspector]
    public int stringIndex;
    public string chatRoomName;
    [SerializeField] 
    GameObject bubbleContent, replyContent;
    public TMP_Text nameText;
    [SerializeField]
    Scrollbar sb;
    ChatManager chatMng;
    Chatter chatter;
    GameManager gameMng;

    DataManager data;
    SaveDataClass saveData;
    
    private void Start() 
    {
        chatMng = FindObjectOfType<ChatManager>();
        gameMng = FindObjectOfType<GameManager>();
        data = DataManager.singleTon;
        saveData = data.saveData;
        
        chatRoomName = GetChatRoomName();
        nameText.text = chatRoomName;
        GetBubble();
    }
/////////////////////////////////////////////////////////////////////////
    
    //순서에 맞게 채팅 버블 생성
    public void GetBubble()
    {   
        SetChatterTextPreview();

        if(chatDatasId % 2 == 0)
        {
            GetChatterBubble();
            chatDatasId++;
        }
        if(chatDatasId % 2 == 1)
        {
            GetReplyBubbleButton();
            chatDatasId++;
        }
    }    

    void GetChatterBubble()
    {
        //chatId는 setChatData에서 설정된거 가져온거
        while(stringIndex < chatDatas[chatDatasId].Length)
        {
            Bubble bubble = Instantiate(chatMng.chatterBubblePrefab).GetComponent<Bubble>();
            if(stringIndex > 0)
            {
                bubble.nameText.gameObject.SetActive(false);
                bubble.UserImage.gameObject.SetActive(false);
            }
            bubble.transform.SetParent(bubbleContent.transform);
            Fit(bubble.GetComponent<RectTransform>());
            bubble.nameText.text = chatRoomName;
            bubble.chatText.text = GetChatDialogue(chatDatasId,stringIndex).Replace("/n", "\n").Replace("name", ChasaData.chasaName); 
            stringIndex++;   
        }
        stringIndex = 0;
        ScrollDown();
    }

    void GetReplyBubbleButton()
    {
        while(chatDatas.ContainsKey(chatDatasId) && stringIndex < chatDatas[chatDatasId].Length)
        {
            Bubble replyBubble = Instantiate(chatMng.replyBubblePrefab).GetComponent<Bubble>();
            replyBubble.transform.SetParent(replyContent.transform);
            Fit(replyBubble.GetComponent<RectTransform>());
            replyBubble.GetComponent<Button>().onClick.AddListener(()=> { ReplyBubbleFunc(replyBubble); }); 
            replyBubble.chatText.text = GetChatDialogue(chatDatasId,stringIndex); 
            stringIndex++;   
        }
        stringIndex = 0;
    }
    
    public void ReplyBubbleFunc(Bubble b)
    {
        Bubble meBubble = Instantiate(chatMng.meBubblePrefab).GetComponent<Bubble>();
        meBubble.transform.SetParent(bubbleContent.transform);
        meBubble.chatText.text = b.chatText.text;
        Fit(meBubble.GetComponent<RectTransform>());
        ReplyBubbleDestroy();
        if(chatDatas.ContainsKey(chatDatasId))
        {
            GetBubble();
        }
        ScrollDown();
    }
///////////////////////////////////////////////////////////////////
    void ReplyBubbleDestroy()
    {
        for (int i = 0; i < replyContent.transform.childCount; i++)
        {    
            Destroy(replyContent.transform.GetChild(i).gameObject);
        }
    }
    
    void SetChatterTextPreview()
    {
        chatter = chatMng.chatterList[chatRoomName].transform.GetComponent<Chatter>();
        chatter.chatText.text = GetChatDialogue(chatDatasId,0).Substring(0,17).Replace("/n", " ").Replace("name", ChasaData.chasaName) + " ..."; 
    }

    void Fit(RectTransform Rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);

    public void ScrollDown()
    {
        Invoke("ScrollDelay", 0.04f);
    }
    public void ScrollDelay() => sb.value = 0;

    string GetChatRoomName()
    {
        foreach(string cn in chatMng.chatRoomList.Keys)
        {
            if (chatMng.chatRoomList[cn] == this.gameObject)
            {
                return cn;
            }
        }
        return "null";
    }

    
}
