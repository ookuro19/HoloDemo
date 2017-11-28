using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class Controller230 : Singleton<Controller230>
{

    [Tooltip("扫描过多少秒开始转换")]
    public float scanTime = 30.0f;

    [Tooltip("扫描时使用的材质")]
    public Material defaultMaterial;

    [Tooltip("停止扫描所使用的材质")]
    public Material secondaryMaterial;

    [Tooltip("最小Plane实体数量")]
    public uint minimumPlanes = 10;

    /// <summary>
    /// 判断是否正在处理Mesh
    /// </summary>
    private bool meshesProcessed = false;

    private void Start()
    {
        // 设置空间扫描器的材质为默认材质
        SpatialMappingManager.Instance.SetSurfaceMaterial(defaultMaterial);

        // 网格转Plane实体完成后的事件监听
        SurfaceMeshesToPlanes.Instance.MakePlanesComplete += SurfaceMeshesToPlanes_MakePlanesComplete;
    }

    private void Update()
    {
        if (!meshesProcessed)
        {
            if ((Time.time - SpatialMappingManager.Instance.StartTime) < scanTime)
            {
                // 还未到指定扫描耗时，则跳过
            }
            else
            {
                // 停止扫描器
                if (SpatialMappingManager.Instance.IsObserverRunning())
                {
                    SpatialMappingManager.Instance.StopObserver();
                }

                // 开始将网格转换为Plane
                CreatePlanes();

                meshesProcessed = true;
            }
        }
    }

    private void SurfaceMeshesToPlanes_MakePlanesComplete(object source, System.EventArgs args)
    {
        // 获取转换后得到的Plane实体对象
        List<GameObject> planes = new List<GameObject>();
        planes = SurfaceMeshesToPlanes.Instance.GetActivePlanes(PlaneTypes.Wall | PlaneTypes.Floor | PlaneTypes.Table);

        // 如果大于我们设置的最小Plane识别值，则对Plane做进一步处理
        if (planes.Count >= minimumPlanes)
        {
            // 将与Plane实体重叠的顶点删除
            RemoveVertices(SurfaceMeshesToPlanes.Instance.ActivePlanes);

            // 扫描结束后，切换第二个材质，本例中使用了“剔除材质（使用了一个剔除Shader）”，即除了Plane外，其他的三角面将被隐藏
            SpatialMappingManager.Instance.SetSurfaceMaterial(secondaryMaterial);
        }
        else
        {
            // 未达到最小Plane识别数，继续扫描
            SpatialMappingManager.Instance.StartObserver();

            meshesProcessed = false;
        }
    }

    /// <summary>
    /// 扫描网格转Plane实体
    /// </summary>
    private void CreatePlanes()
    {
        SurfaceMeshesToPlanes surfaceToPlanes = SurfaceMeshesToPlanes.Instance;
        if (surfaceToPlanes != null && surfaceToPlanes.enabled)
        {
            surfaceToPlanes.MakePlanes();
        }
    }

    /// <summary>
    /// 移除与指定对象组重叠的顶点
    /// </summary>
    private void RemoveVertices(IEnumerable<GameObject> boundingObjects)
    {
        RemoveSurfaceVertices removeVerts = RemoveSurfaceVertices.Instance;
        if (removeVerts != null && removeVerts.enabled)
        {
            removeVerts.RemoveSurfaceVerticesWithinBounds(boundingObjects);
        }
    }

    protected override void OnDestroy()
    {
        if (SurfaceMeshesToPlanes.Instance != null)
        {
            SurfaceMeshesToPlanes.Instance.MakePlanesComplete -= SurfaceMeshesToPlanes_MakePlanesComplete;
        }

        base.OnDestroy();
    }
}