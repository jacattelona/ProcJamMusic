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
            {.1, .45, .6, 1},
            {.25, .35, .65, 1},
            {.4, .7, .8, 1},
            {.25, .6, .9, 1}
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
        chords[0].Activate(true);
        switchingTo = -1;
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
        double rand = Random.Range(0, 1f);
        int choice = state;

        if (rand <= chances[state, 0])
        {
            choice = 0;
        }

        else if (rand > chances[state,0] && rand <= chances[state, 1])
        {
            choice = 1;
        }

        else if (rand > chances[state,1] && rand <= chances[state, 2])
        {
            choice = 2;
        }

        else if (rand > chances[state,2] && rand <= chances[state, 3])
        {
            choice = 3;
        }


        if (choice != state)
        {
            switchingTo = choice;
            chords[state].Activate(false);
            chords[switchingTo].Activate(true);
        } 
    }

    public void SetAverageNoteTime(float average)
    {
        foreach (Chord c in chords)
        {
            c.averageTime = average;
        }
    }

    public void SetMaxNoteTime(float max)
    {
        foreach (Chord c in chords)
        {
            c.maxSpread = max;
        }
    }


}
