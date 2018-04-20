using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    //跟随的目标
    private Transform player;
    //位置偏移值
    private Vector3 offset;
    //平滑度
    private float smoothing = 10;

    // Use this for initialization
    void Start()
    {
        //获得目标的transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //偏移值=当前位置-角色位置
        offset = transform.position - player.position;

    }

    // Update is called once per frame
    void LateUpdate()
    {


        //需要移动到的位置=角色我位置+偏移位置（TransformDirection世界坐标转换成player的局部坐标，使镜头永远在player背后）
        
        //Vector3 targetPosition = player.position + player.TransformDirection(offset);
        Vector3 targetPosition = player.position + offset;
        //进行移动
        transform.position = targetPosition;/*Vector3.Lerp(transform.position, targetPosition, Time.deltaTime*smoothing  );*/
        //让相机望着主角，旋转自身，使得当前对象的正z轴指向目标对象target所在的位置
        transform.LookAt(player.position);
    }
}
