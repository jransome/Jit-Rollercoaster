using UnityEngine;

public class MouseInput : MonoBehaviour
{
  public TrackBuilder builder;
  public Rigidbody ball;
  public float maxPitch = 30f;
  public float maxYaw = 30f;

  private void OnLastRampEntered()
  {
    float xOffset = (Input.mousePosition.x / Screen.width) * 2 - 1f;
    float yOffset = (Input.mousePosition.y / Screen.height) * 2 - 1f;

    Vector3 orientation = new Vector3(
      -yOffset * maxPitch,
      xOffset * maxYaw,
      xOffset * -maxYaw * 0.4f
    );

    builder.BuildRamp(orientation, OnLastRampEntered);
  }

  private void Start()
  {
    builder.Track[builder.Track.Count - 1].OnBallEnteredCb = OnLastRampEntered;
  }

  private void Update() 
  {
    if (Input.GetKeyDown(KeyCode.Space)) ball.AddForce(Vector3.forward * 5f, ForceMode.VelocityChange);  
  }
}
