using UnityEngine;
using System.Collections;
using System;

public class CameraMovement : MonoBehaviour
{

    //摄像机跟踪速度
    public float smooth = 1.5f;

    public  Transform player;
    private Vector3 relCameraPos;
    private float relCameraPosMag;
    private Vector3 newPos;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = player.position + new Vector3(0, 5, -2.5f);
        relCameraPos = transform.position - player.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;
    }
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        //初始位置
        Vector3 standardPos = player.position + relCameraPos;
        //俯视位置
        Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;
        Vector3[] checkPoints = new Vector3[1];

        checkPoints[0] = standardPos;
        transform.position = Vector3.Lerp(transform.position, standardPos, smooth * Time.deltaTime);
        //   SmoothLookAt();
        //RaycastHit hit;
        //if (Physics.Linecast(player.position,transform.position,out hit))
        //{
        //    if (hit.transform.tag != "player")
        //    {
        //        Vector3 currentLocalPosition = transform.localPosition;
        //        currentLocalPosition.z = hit.point.z;
        //        transform.localPosition = currentLocalPosition;
        //    }
        //}
    }

    void SmoothLookAt()
    {
        Vector3 relPlayerPosition = player.position - transform.position;

        Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
    }

    private void ChangeItemTransparency(Collider2D collision, float alpha)
    {        
        SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1, 1, 1, alpha);
        }
    }

    //bool ViewingPosCheck(Vector3 checkPos)
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag))
    //        if (hit.transform != player)
    //            return false;

    //    newPos = checkPos;
    //    return true;
    //}

}
