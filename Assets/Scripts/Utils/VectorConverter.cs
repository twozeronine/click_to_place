using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
public class VectorConverter
{
  public struct Vec3
  {
    public float x;
    public float y;
    public float z;
  }

  [SerializeField]
  private List<Vec3> veclist = new List<Vec3>();

  public static List<Vec3> ConvertUnityVectoVec3(List<Vector3> unityVec3)
  {
    List<Vec3> vecList = new List<Vec3>();
    
    foreach(var uVec3 in unityVec3)
    {
      vecList.Add(new Vec3{x = uVec3.x, y = uVec3.y, z = uVec3.z});
    }
    return vecList;
  }
}