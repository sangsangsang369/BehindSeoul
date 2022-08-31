using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatData : MonoBehaviour
{
    public Dictionary<int, string[]> chatDatas;
    public List<string> goblinNames;
    public static int goblinNamesIndex = 0;
    public static int chatDatasId = 0;
    public static bool chatChecked = false;
    
    private void Awake() 
    {
        chatDatas = new Dictionary<int, string[]>();
        goblinNames = new List<string>();
        GenerateData();    
        GenerateName();
    }

    void GenerateData()
    {
        // 1n = 설명 도깨비
        chatDatas.Add(10, new string[] { "반가워 name 영혼차사! /n /n나는 너에게 신의 어명과 정보를 전달하기 위해 파견된 설명의 도깨비야! /n숭례문 까지 오느라 수고했어. ",
                                         "지금 한시가 급하니 네가 잡아야만 하는, 서울을 어지럽히는 괴물 ‘수류견’부터 자세하게 설명해줄게."});
        chatDatas.Add(11, new string[] { "수류견?"});                                 
        chatDatas.Add(12, new string[] { "수류견은, 악취를 풍기고 다니면서 가위를 눌리게 하는 능력이 있어./n 악몽 능력을 사용해 꿈에서 발생하지 않은 일에 대해 보여주고 두려움을 심어줘.",
                                        "결국, 사람들은 악몽에서 등장하는 내용을 믿고 세상에 대한 불신만 커지게 되지. /n수류견의 영향을 받은 사람들은 잘못된 정보를 퍼트리면서 사회에 혼란을 야기해.",
                                        "요즘 거짓뉴스들이 떠돌아 다니는 문제가 많지? /n모두 수류견의 영향이야"});
        chatDatas.Add(13, new string[] { "알겠어 그럼 나는 뭘 하면 될까??"});                                 
        chatDatas.Add(14, new string[] { "이제 너의 임무는 어명을 받아 이 일대의 모든 괴물을 봉인한 후 흩어진 신의 영혼을 모아 원래대로 돌려놓는거야.",
                                         "여기 어명을 전달해줄게. /n지령을 나가서 확인해봐."});
        // 2n = 지원 도깨비
        chatDatas.Add(20, new string[] { "나야 나 도깨비. /n /n덕수궁까지 잘 도착했구나. /n나는 지원 담당 도깨비야. /n수류견이 덕수궁으로 도망쳤다는 소식을 듣고 온거지?",
                                         "그래서 나도 지원을 나왔는데, 이미 수류견이 덕수궁의 원래 이름을 불태우고 여기를 떠났어.",
                                         "여기, 신들의 어명을 줄게. /n영혼차사 name! 불타버린 덕수궁의 이름을 찾아줄래?"});
        chatDatas.Add(21, new string[] { "나한테 맡겨줘!"});
        //2nn = 지원도깨비에서 이어지는 부분
        chatDatas.Add(202, new string[] { "내가 도술로 수류견이 불태우고 간 장소들의 위치를 활성화시켰어!",
                                          "글자들이 흩어진 장소들을 방문해서 덕수궁의 옛이름을 돌려놔줘"});
        chatDatas.Add(206, new string[] { "여기까지 잘찾아왔어! /n /n 여기는 전통 재래시장인 통인시장이야. /n독특하게 현대 화폐를 엽전으로 바꿔서 이용할 수 있어!",
                                          "수류견은 이곳을 거쳐서 다른 곳으로 이미 가버린 것 같아.."});
        chatDatas.Add(207, new string[] { "우리가 늦었네.."});
        chatDatas.Add(208, new string[] { "그치만 걱정마! 우리에게 매번 관광정보를 주던 ‘정보도깨비’가 이곳에 살고있어! /n시장에서 사람들과 섞여 살면서 인간들의 정보를 쉽게 얻는다나봐",
                                          "문제는.. 정보 도깨비가 있는 곳을 모르겠네../n워낙 모습을 드러내지 않는 도깨비라 도깨비들도 이 정도밖에 몰라",
                                          "우리의 존재가 드러난 장소에 있다는 것"});
        chatDatas.Add(209, new string[] { "거기가 대체 어디지?"});    
        chatDatas.Add(210, new string[] { "예전에 내가 물어봤을 때 정보도깨비가 줬던 힌트는 통인시장 주변의 간판을 잘 보고 다니라고 한 거였어",
                                          "통인시장을 잘 살펴보자!"});

    }

    public string GetChatDialogue(int id, int chatDatasIndex)
    {
        return chatDatas[id][chatDatasIndex];
    } 

    void GenerateName()
    {
        goblinNames.Add("설명 도깨비");
        goblinNames.Add("지원 도깨비");
    }
}
