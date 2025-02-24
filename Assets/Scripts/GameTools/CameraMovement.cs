using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class CameraMovement : MonoBehaviour
{

    //摄像机跟踪速度
    public float smooth = 1.5f;

    public  Transform player;
    private Vector3 relCameraPos;
    private float relCameraPosMag;
    private Vector3 newPos;

    public float maxMouseOffsetDistance = 1;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        transform.position = player.position + new Vector3(0, 5, -2.5f);
        //transform.localPosition = transform.localPosition + new Vector3(0, 0, -2);
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
        SmoothLookAt();

    }

    void SmoothLookAt()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        Vector3 offsetDirection = new Vector3();
        if (groundPlane.Raycast(ray,out float position))
        {
            Vector3 mouseWorldPosition = ray.GetPoint(position);
            offsetDirection = mouseWorldPosition - transform.position;
            offsetDirection.y = 0;
            if (offsetDirection.magnitude > maxMouseOffsetDistance)
            {
                offsetDirection = offsetDirection.normalized * maxMouseOffsetDistance;
            }
        }
        Vector3 relPlayerPosition = player.position - transform.position + offsetDirection;

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
