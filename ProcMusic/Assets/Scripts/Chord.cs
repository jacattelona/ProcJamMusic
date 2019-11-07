using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chord : MonoBehaviour
{
    const float TOP = 5.5f;
    const float BOTTOM = -3.5f;
    const float LEFT = -7.5f;
    const float RIGHT = 7.5f;


    public AudioClip[] notes;
    public Dictionary<AudioClip, int> values;
    public GameObject particle;

    AudioSource source;
    float counter = 5f;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
        if (counter < 0)
        {
            counter = 5f;
        }
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
        //source.PlayOneShot(notes[index]);
        GameObject part = Instantiate(particle);
        part.transform.position = new Vector3(Random.Range(LEFT, RIGHT), Random.Range(BOTTOM, TOP), 0);
    }

}
