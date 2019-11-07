using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Chord[] chords;


    void Start()
    {
        chords = GetComponentsInChildren<Chord>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
