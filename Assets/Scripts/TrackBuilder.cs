using System;
using System.Collections.Generic;
using UnityEngine;

public enum RampDirection
{
  Left,
  Straight,
  Right,
  DownLeft,
  Down,
  DownRight,
  UpLeft,
  Up,
  UpRight,
}

public class TrackBuilder : MonoBehaviour
{
  static float AngleScaleFactor = 1f;
  static float UpDownAngle = 10f;
  static float LeftRightAngle = 10f;
  static float RollAngle = 10f;


  public static Dictionary<KeyCode, RampDirection> KeyBindings = new Dictionary<KeyCode, RampDirection>()
  {
    { KeyCode.Q, RampDirection.UpLeft },
    { KeyCode.W, RampDirection.Up },
    { KeyCode.E, RampDirection.UpRight },
    { KeyCode.A, RampDirection.Left },
    { KeyCode.S, RampDirection.Straight },
    { KeyCode.D, RampDirection.Right },
    { KeyCode.Z, RampDirection.DownLeft },
    { KeyCode.X, RampDirection.Down },
    { KeyCode.C, RampDirection.DownRight },
  };

  public static Dictionary<RampDirection, Quaternion> RampRotations = new Dictionary<RampDirection, Quaternion>()
  {
    { RampDirection.UpLeft,     Quaternion.Euler(-UpDownAngle, -LeftRightAngle, RollAngle) },
    { RampDirection.Up,         Quaternion.Euler(-UpDownAngle, 0, 0) },
    { RampDirection.UpRight,    Quaternion.Euler(-UpDownAngle, LeftRightAngle, -RollAngle) },
    { RampDirection.Left,       Quaternion.Euler(0, -LeftRightAngle, 0) },
    { RampDirection.Straight,   Quaternion.Euler(0, 0, 0) },
    { RampDirection.Right,      Quaternion.Euler(0, LeftRightAngle, 0) },
    { RampDirection.DownLeft,   Quaternion.Euler(UpDownAngle, -LeftRightAngle, RollAngle) },
    { RampDirection.Down,       Quaternion.Euler(UpDownAngle, 0, 0) },
    { RampDirection.DownRight,  Quaternion.Euler(UpDownAngle, LeftRightAngle, -RollAngle) },
  };

  // public event Action<Ramp> RampAdded = delegate { };

  public GameObject RampPrefab;
  public List<Ramp> Track; // Initialised in inspector

  public void BuildRamp(RampDirection direction, Action onBallEnteredCb)
  {
    Ramp previous = Track[Track.Count - 1];
    Vector3 prevXYEuler = new Vector3(previous.transform.eulerAngles.x, previous.transform.eulerAngles.y, 0f);
    Ramp r = Instantiate(RampPrefab, previous.EndConnector.position, RampRotations[direction] * Quaternion.Euler(prevXYEuler)).GetComponent<Ramp>();
    r.OnBallEnteredCb = onBallEnteredCb;
    Track.Add(r);
  }

  public void BuildRamp(Vector3 orientation, Action onBallEnteredCb)
  {
    Ramp previous = Track[Track.Count - 1];
    Ramp r = Instantiate(RampPrefab, previous.EndConnector.position, Quaternion.Euler(orientation)).GetComponent<Ramp>();
    r.OnBallEnteredCb = onBallEnteredCb;
    Track.Add(r);
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Q)) BuildRamp(KeyBindings[KeyCode.Q], delegate { });
    if (Input.GetKeyDown(KeyCode.W)) BuildRamp(KeyBindings[KeyCode.W], delegate { });
    if (Input.GetKeyDown(KeyCode.E)) BuildRamp(KeyBindings[KeyCode.E], delegate { });
    if (Input.GetKeyDown(KeyCode.A)) BuildRamp(KeyBindings[KeyCode.A], delegate { });
    if (Input.GetKeyDown(KeyCode.S)) BuildRamp(KeyBindings[KeyCode.S], delegate { });
    if (Input.GetKeyDown(KeyCode.D)) BuildRamp(KeyBindings[KeyCode.D], delegate { });
    if (Input.GetKeyDown(KeyCode.Z)) BuildRamp(KeyBindings[KeyCode.Z], delegate { });
    if (Input.GetKeyDown(KeyCode.X)) BuildRamp(KeyBindings[KeyCode.X], delegate { });
    if (Input.GetKeyDown(KeyCode.C)) BuildRamp(KeyBindings[KeyCode.C], delegate { });
  }
}
