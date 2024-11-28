using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraFollow : MonoBehaviour
{
  [SerializeField] private Vector3 offset;
  [SerializeField] private float smoothing;

  public Transform target;
  public bool bounds;

  public Vector3 minCameraPos;
  public Vector3 maxCameraPos;
  
  private Vector3 velocity = Vector3.zero;
  private void FixedUpdate()
  {
    Vector3 targetPosition = target.position + offset;
    targetPosition.z = transform.position.z;
    
    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);

    if (bounds)
    {
      transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x), Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
    }
  }
}
