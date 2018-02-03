using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlefRendererExtensions 
{
    /// <summary>
    /// 检查对象渲染器是否在摄像机的可见范围内
    /// </summary>
    /// <param name="renderer">渲染对象</param>
    /// <param name="camera">摄像机</param>
    /// <returns></returns>
    public static bool IsVisibleFrom(Bounds bound, Camera camera) {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, bound);
    }
}
