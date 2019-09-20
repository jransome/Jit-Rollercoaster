using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  public bool FollowUpDown = true;
  public Rigidbody Ball;
  public float CameraSlerp = 0.2f;

  private void FixedUpdate()
  {
    Vector3 targetVector = FollowUpDown ? Ball.velocity : new Vector3(Ball.velocity.x, 0f, Ball.velocity.z);
    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetVector, Vector3.up), CameraSlerp);
    transform.position = Ball.position;
  }
}