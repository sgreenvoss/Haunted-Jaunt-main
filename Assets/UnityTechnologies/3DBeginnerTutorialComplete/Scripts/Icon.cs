using UnityEditor.UI;
using UnityEngine;

public class Icon : MonoBehaviour
{
    // new script for the Eyecons.
    // used in the PlayerMovement script where the player has an eye visible
    // when they are not invisible.
    // also used in the Observer script to indicate that the player is in 
    // possible range of an enemy to be seen. 

    public Transform character;
    Transform eye;
    public Vector3 offset = new Vector3(0f, 1.8f, 0f);
    public Vector3 opened = new Vector3(0.4f, 0.4f, 0.4f);
    public Vector3 closed = new Vector3(0.4f, 0f, 0.4f);
    public AudioSource sparkle;

    private void Start()
    {
        eye = GetComponent<Transform>();
        sparkle = GetComponent<AudioSource>();
        eye.localScale = opened;
    }
    void FixedUpdate()
    {
        eye.position = character.position + offset;
    }

    public void Close()
    {
        eye.localScale = closed;
    }

    public void DrawOpening(float value)
    {
        Vector3 vector3 = new Vector3(.4f, value, .4f);
        eye.localScale = vector3;
    }

    public void playAudio()
    {
        // stupidly I have delegated audio playing here only because I don't
        // yet know how to handle the case where John Lemon needs to play two
        // audios. so when John collides with an invisiblizer, his eye will 
        // actually be the one to play the audio.
        sparkle.Play();
    }
}
