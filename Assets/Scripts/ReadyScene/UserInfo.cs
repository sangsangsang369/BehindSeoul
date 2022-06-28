using UnityEngine;
using Firebase.Firestore;

[FirestoreData]
public struct UserInfo
{
   [FirestoreProperty]
   public string name { get; set; }
   [FirestoreProperty]
   public string email { get; set; }
   [FirestoreProperty]
   public string password { get; set; }
}
