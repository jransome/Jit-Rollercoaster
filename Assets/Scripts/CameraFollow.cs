using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  public Rigidbody Ball;
  public float CameraSlerp = 0.2f;

  private void FixedUpdate()
  {
    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Ball.velocity, Vector3.up), CameraSlerp);
    transform.position = Ball.position;
  }
}