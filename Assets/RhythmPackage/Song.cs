using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Song", menuName = "Song", order = 1)]
public class Song : ScriptableObject
{
    [Header("Temp")]
    public float bpm;
    [DisplayWithoutEdit()]
    public float spb;

    [Space(20)]
    [Header("Time Signature")]
    // (crotchets in a bar)
    public int cib;

    [Space(20)]
    [Header("Audio")]
    public AudioClip song;

    void OnValidate() {
        spb = 60/bpm;
    }
}