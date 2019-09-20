using System.Collections.Generic;
using UnityEngine;

public class TrackBuilder : MonoBehaviour
{
  static float AngleScaleFactor = 1f;
  static float UpDownAngle = 10f;
  static float LeftRightAngle = 10f;
  static float RollAngle = 10f;

  public enum RampDirection
  {
    UpLeft,
    Up,
    UpRight,
    Left,
    Straight,
    Right,
    DownLeft,
    Down,
    DownRight,
  }

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

  public GameObject RampPrefab;
  public List<Ramp> Track; // Initialised in inspector

  private void CreateRamp(RampDirection direction)
  {
    Ramp previous = Track[Track.Count - 1];
    Ramp r = Instantiate(RampPrefab, previous.EndConnector.position, RampRotations[direction] * Quaternion.AngleAxis(previous.transform.rotation.eulerAngles.y, Vector3.up)).GetComponent<Ramp>();
    Track.Add(r);
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Q)) CreateRamp(KeyBindings[KeyCode.Q]);
    if (Input.GetKeyDown(KeyCode.W)) CreateRamp(KeyBindings[KeyCode.W]);
    if (Input.GetKeyDown(KeyCode.E)) CreateRamp(KeyBindings[KeyCode.E]);
    if (Input.GetKeyDown(KeyCode.A)) CreateRamp(KeyBindings[KeyCode.A]);
    if (Input.GetKeyDown(KeyCode.S)) CreateRamp(KeyBindings[KeyCode.S]);
    if (Input.GetKeyDown(KeyCode.D)) CreateRamp(KeyBindings[KeyCode.D]);
    if (Input.GetKeyDown(KeyCode.Z)) CreateRamp(KeyBindings[KeyCode.Z]);
    if (Input.GetKeyDown(KeyCode.X)) CreateRamp(KeyBindings[KeyCode.X]);
    if (Input.GetKeyDown(KeyCode.C)) CreateRamp(KeyBindings[KeyCode.C]);
  }
}
