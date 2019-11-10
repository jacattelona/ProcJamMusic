using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    Chord[] chords;
    AudioSource[] bgSources;
    double[,] chances;

    [SerializeField]
    Material bg;

    //state 0 = starting state
    //states 1-4 = chords 1-4
    int state = 0;
    int switchingTo = -1;

    float t = 0;
    float lerpRate = .4f;

    float counter = 10f;


    void Start()
    {
        chances = new double[4, 4] {
            {.1, .2, .35, .35},
            {.1, .1, .1, .1},
            {.1, .1, .1, .1},
            {.1, .1, .1, .1}
        };

        GameObject chordParent = transform.GetChild(1).gameObject;
        chords = chordParent.GetComponentsInChildren<Chord>();

        GameObject bgParent = transform.GetChild(0).gameObject;
        bgSources = bgParent.GetComponentsInChildren<AudioSource>();

        foreach(AudioSource a in bgSources)
        {
            a.volume = 0;
            a.Play();
            a.loop = true;
        }

        foreach(Chord c in chords)
        {
            c.Activate(false);
        }
        chords[1].Activate(true);
        switchingTo = 1;
        bg.color = chords[0].bgColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (switchingTo != -1)
        {
            t += Time.deltaTime * lerpRate;
            bg.color = Color.Lerp(chords[state].bgColor, chords[switchingTo].bgColor, t);
            bgSources[state].volume = 1 - t;
            bgSources[switchingTo].volume = t;

            if (t >= 1.0f)
            {
                t = 0;
                state = switchingTo;
                switchingTo = -1;
            }
        }

        else
        {
            counter -= Time.deltaTime;
            if (counter <= 0)
            {
                NextChord();
                counter = 10;
            }
        }
    }


    void NextChord()
    {
        int choice = (int)Random.Range(0, 3.99f);
        if (choice != state)
        {
            switchingTo = choice;
            chords[state].Activate(false);
            chords[switchingTo].Activate(true);
        } 
    }


}
