using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
  public float perlinNoiseScale = 0.2f;
  public float maxUpPitch = 10f;
  public float maxDownPitch = 20f;
  public float maxYaw = 10f;

  public TrackBuilder builder;
  public Rigidbody ball;

  Ramp lastRamp;

  private RampDirection GenerateNextRampDirection() => (RampDirection)Random.Range(0, 7);

  private Vector3 GenerateNextRampFromAngles()
  {
    // int nRamps = builder.Track.Count;
    // List<Ramp> recentRamps = nRamps > 5 ? builder.Track.GetRange(nRamps - 6, nRamps - 1) : builder.Track;
    // float avgYaw = recentRamps.Average(r => r.transform.rotation.eulerAngles.y);
    // float randomYaw = Random.Range(-maxYaw, maxYaw);

    float perlin = Mathf.PerlinNoise(0f, builder.Track.Count * perlinNoiseScale + 500);
    float randomYaw = (perlin * 2f - 1f) * maxYaw;
    Debug.Log(randomYaw);

    return new Vector3(
      Random.Range(-maxUpPitch, maxDownPitch),
      randomYaw,
      -randomYaw * 0.4f
    );
  }

  private void OnLastRampEntered()
  {
    // builder.BuildRamp(GenerateNextRampDirection(), OnLastRampEntered);
    builder.BuildRamp(GenerateNextRampFromAngles(), OnLastRampEntered);
  }

  private void Start()
  {
    lastRamp = builder.Track[builder.Track.Count - 1];
    lastRamp.OnBallEnteredCb = OnLastRampEntered;
  }

  private void Update() 
  {
    if (Input.GetKeyDown(KeyCode.Space)) ball.AddForce(Vector3.forward * 5f, ForceMode.VelocityChange);  
  }
}
