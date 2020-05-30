using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Power Up Property", menuName = "PowerUp")]
public class PowerUpProps : ScriptableObject {
    public int powerUpId;
    public GameObject powerUpPrefab;
    public float powerUpDuration = 10f;
    public float powerUpFallSpeed = 1;
    public float spawnIntervalMin;
    public float spawnIntervalMax;
    public AudioClip audio;
}
