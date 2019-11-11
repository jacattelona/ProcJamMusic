using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chord : MonoBehaviour
{
    const float TOP = 5.5f;
    const float BOTTOM = -3.5f;
    const float LEFT = -7.5f;
    const float RIGHT = 7.5f;

    public Color bgColor;
    public AudioClip bgMusic;
    public AudioClip[] notes;
    public Dictionary<AudioClip, int> values;
    public GameObject particle;
    public float averageTime = 3f;
    public float maxSpread = 4f;

    AudioSource source;
    float counter = 5f;
    bool active = false;

    ParticleSystem background;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        background = GetComponentInChildren<ParticleSystem>();
        background.Stop();
        counter = averageTime + (Random.value * maxSpread - (maxSpread / 2f));
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            counter -= Time.deltaTime;
            if (counter < 0)
            {
                counter = averageTime + (Random.value * maxSpread - (maxSpread/2f));
                if (counter < .25f) counter = .25f;
                int choice = (int)Random.Range(0, 4.99f);
                PlayNote(choice);
            }
        }
    }

    public void Activate(bool act)
    {
        active = act;
        if (act)
            background.Play();
        else
            background.Stop();
    }

    public void AssignChances()
    {
        foreach(AudioClip a in notes) {
            values[a] = (int) Random.value*100;
        }
   
    }

    public AudioClip[] getNotes()
    {
        AudioClip[] b = new AudioClip[3];
        int check = 0;
        while(check < 3)
        {
            int rand = (int)Random.value * 100;
            foreach(AudioClip c in values.Keys)
            {
                if (values[c] == rand)
                {
                    b[check] = c;
                    check++;
                    AssignChances();
                }

            }
        }
        return (b);
    }



    void PlayNote(int index)
    {
        source.PlayOneShot(notes[index]);
        GameObject part = Instantiate(particle);
        part.transform.position = new Vector3(Random.Range(LEFT, RIGHT), Random.Range(BOTTOM, TOP), 0);
    }

}
