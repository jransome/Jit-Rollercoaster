using System;
using UnityEngine;

public class Ramp : MonoBehaviour
{
  public Transform EndConnector;
  public Action OnBallEnteredCb;
  public Renderer FloorRenderer;

  Material material;
  [ColorUsage(false, true)] public Color ActiveColour;
  [ColorUsage(false, true)] public Color InActiveColour;

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("ball"))
    {
      OnBallEnteredCb();
      OnBallEnteredCb = null;
      material.SetColor("_EmissionColor", ActiveColour);
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.transform.CompareTag("ball")) material.SetColor("_EmissionColor", InActiveColour);
  }

  private void Start() => material = FloorRenderer.material;
}