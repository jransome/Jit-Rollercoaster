using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
  public TrackBuilder builder;
  public Rigidbody ball;

  Ramp lastRamp;

  private RampDirection GenerateNextRampDirection()
  {
    return (RampDirection)Random.Range(0, 7);
  }

  private void OnLastRampEntered()
  {
    builder.BuildRamp(GenerateNextRampDirection(), OnLastRampEntered);
  }

  private void Start()
  {
    lastRamp = builder.Track[builder.Track.Count - 1];
    lastRamp.OnBallEnteredCb = OnLastRampEntered;
  }
}
