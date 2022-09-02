using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatRoom : ChatData
{
    [HideInInspector]
    public int stringIndex, chatRoomNum;
    [SerializeField] 
    GameObject bubbleContent, replyContent;
    [SerializeField]
    Scrollbar sb;
    ChatManager chatMng;
    Chatter chatter;
    GameManager gameMng;
    
    private void Start() 
    {
        chatMng = FindObjectOfType<ChatManager>();
        gameMng = FindObjectOfType<GameManager>();
        
        chatRoomNum = GetChatRoomNum();
        GetBubbleText();
    }

    public void GetBubbleText()
    {   
        SetChatterText();

        if(chatDatasId % 2 == 0)
        {
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
                bubble.nameText.text = goblinNames[goblinNamesIndex];
                bubble.chatText.text = GetChatDialogue(chatDatasId,stringIndex).Replace("/n", "\n").Replace("name", ChasaData.chasaName); 
                stringIndex++;   
            }
            stringIndex = 0;
            ++chatDatasId;
        }
        ScrollDown();

        if(chatDatasId % 2 == 1)
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
            ++chatDatasId;
        }
        ScrollDown();
        if(!chatDatas.ContainsKey(chatDatasId))
        {
            gameMng.nextBtnsAfterCheckedChat[gameMng.nextBtnIndex].SetActive(true);
            gameMng.nextBtnIndex++;
        }
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
            GetBubbleText();
        }
    }

    void ReplyBubbleDestroy()
    {
        for (int i = 0; i < replyContent.transform.childCount; i++)
        {    
            Destroy(replyContent.transform.GetChild(i).gameObject);
        }
    }

    void Fit(RectTransform Rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);

    public void ScrollDown()
    {
        Invoke("ScrollDelay", 0.04f);
    }
    public void ScrollDelay() => sb.value = 0;

    int GetChatRoomNum()
    {
        for(int i = 0; i < chatMng.chatRoomList.Count; i++)
        {
            if (chatMng.chatRoomList[i] == this.gameObject)
            {
                return i;
            }
        }
        return 11;
    }

    void SetChatterText()
    {
        chatter = chatMng.chatterList[chatRoomNum].transform.GetComponent<Chatter>();
        chatter.chatText.text = GetChatDialogue(chatDatasId,0).Substring(0,17).Replace("/n", " ").Replace("name", ChasaData.chasaName) + " ..."; 
    }
}
