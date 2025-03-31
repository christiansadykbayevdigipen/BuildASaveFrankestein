using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//grants access to the mixer
using UnityEngine.Audio;

public class RandomContainer : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioMixerGroup output;
    //public KeyCode keyToPress = KeyCode.Space; //use to test
    public float minPitch = 0.75f;
    public float maxPitch = 1.25f;


    // Update is called once per frame
    void Update()
    {
        //default playback method for testing
        /*if (Input.GetKeyDown(keyToPress))
        {
            PlaySound();
        }*/
    }

    public void PlaySound(bool noPitchVar)
    {
        //randomize within the array length
        int randomClip = Random.Range(0, clips.Length);

        //create audiosource
        AudioSource source = gameObject.GetComponent<AudioSource>();

        //load clip into audiosource
        source.clip = clips[randomClip];

        //set output for audiosource
        source.outputAudioMixerGroup = output;

        if (noPitchVar)
        {
            //for sounds with no pitch variation
            source.pitch = 1;
        }
        else
        {
            //otherwise set pitch variation
            source.pitch = Random.Range(minPitch, maxPitch);
        }
        

        //play clip
        source.Play();
    }
}