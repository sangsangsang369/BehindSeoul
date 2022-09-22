using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageChangeBtn : MonoBehaviour
{
    [SerializeField]
    List<GameObject> prevPage, nextPage;

    public void GoToNextPage()
    {
        foreach(GameObject page in prevPage)
        {
            page.SetActive(false);
        } 
        foreach(GameObject page in nextPage)
        {
            page.SetActive(true);
        } 
        
    }
    public void JustSetActiveTNextPage()
    {
        foreach(GameObject page in nextPage)
        {
            page.SetActive(true);
        } 
    }
    public void JustSetActiveFPrevPage()
    {
        foreach(GameObject page in prevPage)
        {
            page.SetActive(false);
        } 
    }
    
}
