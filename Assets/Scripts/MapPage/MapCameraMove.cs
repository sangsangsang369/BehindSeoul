using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class MapCameraMove : MonoBehaviour
{
    [SerializeField]
    GameObject player, mapScene;
    [SerializeField]
    Vector2d position;
    [SerializeField]
    Camera mainCamera;

    private float Speed = 1f;
    private Vector2 nowPos, prePos;
    private Vector3 movePos;

    public void CameraLocationInitToPlayer()
    {
        mainCamera.transform.SetParent(player.transform);
        mainCamera.transform.position = new Vector3(0, mainCamera.transform.position.y, 0);
    }
    
    void Update()
    {
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch (0);
            if(touch.phase == TouchPhase.Began)
            { 
                mainCamera.transform.SetParent(mapScene.transform);
                prePos = touch.position - touch.deltaPosition;
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                nowPos = touch.position - touch.deltaPosition;
                movePos = (Vector3)(prePos - nowPos) * Time.deltaTime * Speed;
                mainCamera.transform.Translate(movePos); 
                prePos = touch.position - touch.deltaPosition;
            }
        }
    }
}
