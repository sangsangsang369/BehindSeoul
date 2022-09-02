using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignDraw : MonoBehaviour
{
    public GameObject dot, canvas, signBtn;
    bool pressed;
    public List<GameObject> signDots = new List<GameObject>();
    public List<Vector2> poslist = new List<Vector2>();
    float timer = 0;

    void Update() 
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), 
        new Vector2(Input.mousePosition.x, Input.mousePosition.y), null, out pos);

        if(pressed && -250 < pos.y && pos.y < 350 ) 
        {
            GameObject m = Instantiate(dot, pos, Quaternion.identity);
            m.transform.SetParent(this.gameObject.transform, false);
            signDots.Add(m);
        }
    
        if(Input.GetMouseButtonDown(0))
        {
            pressed = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            pressed = false;
        }
    }

    public void CopySign()
    {
        signBtn.GetComponent<Button>().enabled = false;
        GameObject miniSign = Instantiate(this.gameObject);
        miniSign.transform.SetParent(signBtn.transform, false);
        miniSign.transform.localScale = new Vector3(0.4f ,0.4f ,0.4f);
    }
}
