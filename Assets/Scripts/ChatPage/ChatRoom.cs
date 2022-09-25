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
    GameManager gameMng;

    DataManager data;
    SaveDataClass saveData;
    int chatDataFirstNum = 0;

    private void Start() 
    {
        chatMng = FindObjectOfType<ChatManager>();
        gameMng = FindObjectOfType<GameManager>();
        data = DataManager.singleTon;
        saveData = data.saveData;
        
        chatRoomName = GetChatRoomName();
        nameText.text = chatRoomName;
        
        for(int i = 0; i < chatMng.chatterNameList.Length; i++)
        {
            if(chatRoomName == chatMng.chatterNameList[i])
            {
                chatDataFirstNum = i + 1;
                break;
            }
        }

        if(chatDataFirstNum == 1 && !saveData.ischatOneHavePrev)
        {
            GetBubble_Save();
        }
        else if(chatDataFirstNum == 2 && !saveData.ischatTwoHavePrev)
        {
            GetBubble_Save();
        }
        else if(chatDataFirstNum == 3 && !saveData.ischatThreeHavePrev)
        {
            GetBubble_Save();
        }
    }
    
    public void GetBubble_Save()
    {   
        data = DataManager.singleTon;
        saveData = data.saveData;

        int prevChatId = chatDatasId;

        GetBubble();
        ScrollDown();  
        
        if(prevChatId > 100)
        {
            if(prevChatId / 100 == 2)
            {
                saveData.endedChatDataId_two.Add(prevChatId);
            }
            else if(prevChatId / 100 == 3)
            {
                saveData.endedChatDataId_three.Add(prevChatId);
            }
        }
        else
        {
            if(prevChatId / 10 == 1)
            {
                saveData.endedChatDataId_one.Add(prevChatId);
            }
            else if(prevChatId / 10 == 2)
            {
                saveData.endedChatDataId_two.Add(prevChatId);
            }
            else if(prevChatId / 10 == 3)
            {
                saveData.endedChatDataId_three.Add(prevChatId);
            }
        }
        data.Save();
    }

    public void GetBubble()
    {   
        SetChatterTextPreview();

        if(chatDatasId % 2 == 0)
        {
            GetChatterBubble(chatDatasId);
            chatDatasId++;
        }
        if(chatDatasId % 2 == 1)
        {
            GetReplyBubbleButton();
            chatDatasId++;
        }

        if(chatDataFirstNum == 1)
        {
            saveData.ischatOneHavePrev = true;
        }
        else if(chatDataFirstNum == 2)
        {
            saveData.ischatTwoHavePrev = true;
        }
        else if(chatDataFirstNum == 3)
        {
            saveData.ischatThreeHavePrev = true;
        }
        data.Save(); 
    }    
    
    public void GetbubbleAuto(int id)
    {   //자동으로 챗 생성
        if(chatDatas.ContainsKey(id) && id % 2 == 0)
        {
            GetChatterBubble(id);
            id++;
        }
        if(chatDatas.ContainsKey(id) && id % 2 == 1)
        {
            Bubble meBubble = Instantiate(chatMng.meBubblePrefab).GetComponent<Bubble>();
            meBubble.transform.SetParent(bubbleContent.transform);
            meBubble.chatText.text = GetChatDialogue(id,stringIndex);
            Fit(meBubble.GetComponent<RectTransform>());
            id++;
        }
        ScrollDown();  
        if(chatDatas.ContainsKey(id))
        {
            GetbubbleAuto(id);
        }         
    }

    void GetChatterBubble(int id)
    {   //도깨비한테 온 채팅 띄우는 함수
        chatMng = FindObjectOfType<ChatManager>();
        //chatId는 setChatData에서 설정된거 가져온거
        while(stringIndex < chatDatas[id].Length)
        {
            Bubble bubble = Instantiate(chatMng.chatterBubblePrefab).GetComponent<Bubble>();
            if(stringIndex > 0)
            {
                bubble.nameText.gameObject.SetActive(false);
                bubble.UserImage.gameObject.SetActive(false);
            }
            bubble.transform.SetParent(bubbleContent.transform);
            Fit(bubble.GetComponent<RectTransform>());
            chatRoomName = GetChatRoomName();
            bubble.nameText.text = chatRoomName;
            bubble.chatText.text = GetChatDialogue(id,stringIndex).Replace("/n", "\n").Replace("name", ChasaData.chasaName); 
            stringIndex++;   
        }
        stringIndex = 0;
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

    void ReplyBubbleDestroy()
    {
        for (int i = 0; i < replyContent.transform.childCount; i++)
        {    
            Destroy(replyContent.transform.GetChild(i).gameObject);
        }
    }
    
    void SetChatterTextPreview()
    {
        chatRoomName = GetChatRoomName();
        chatMng.chatterList[chatRoomName].gameObject.GetComponent<Chatter>().chatText.text 
        = GetChatDialogue(chatDatasId,0).Substring(0,17).Replace("/n", " ").Replace("name", ChasaData.chasaName) + " ..."; 
    }

    string GetChatRoomName()
    {
        chatMng = FindObjectOfType<ChatManager>();
        foreach(string cn in chatMng.chatRoomList.Keys)
        {
            if (chatMng.chatRoomList[cn] == this.gameObject)
            {
                return cn;
            }
        }
        return "null";
    }
    
    void Fit(RectTransform Rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);

    public void ScrollDown()
    {
        Invoke("ScrollDelay", 0.04f);
    }
    public void ScrollDelay() => sb.value = 0;
}
