using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseMask : MonoBehaviour
{
    public GameObject mask, canvas;
    bool pressed;
    public List<GameObject> eraseMasks = new List<GameObject>();
    public List<Vector2> poslist = new List<Vector2>();
    float timer = 0;

    void Update() 
    {
        timer += Time.deltaTime;
        if(timer >= 0.04f)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), 
            new Vector2(Input.mousePosition.x, Input.mousePosition.y), null, out pos);

            if(pressed) 
            {
                GameObject m = Instantiate(mask, pos, Quaternion.identity);
                m.transform.SetParent(this.gameObject.transform, false);
                eraseMasks.Add(m);
            }

            timer -= 0.04f;
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

    public void DestroyEraseMasks()
    {
        foreach(GameObject mp in eraseMasks)
        {
            Destroy(mp);
        }
        eraseMasks.Clear();
    }
}
