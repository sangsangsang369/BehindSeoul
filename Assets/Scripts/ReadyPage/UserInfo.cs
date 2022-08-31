using UnityEngine;
using Firebase.Firestore;

[FirestoreData]
public struct UserInfo
{
   //firestore에 저장할 데이터 형식

   [FirestoreProperty]
   public string password { get; set; }
}
