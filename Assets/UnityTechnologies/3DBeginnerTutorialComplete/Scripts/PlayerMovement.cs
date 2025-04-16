using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public bool IsInvisible { get; private set; } = false;
    private float invisibilityTime = 5f; // amount of time player is invisible
    private float invisibilityElapsed = 0f;
    private float inv_lim; // invisibility limit to be calculated once.

    Animator m_Animator;
    
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    public Icon eye; 

    void Start ()
    {
        inv_lim = 1f / invisibilityTime;
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();
    }

    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("IsWalking", isWalking);
        
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);

        // new: if i am invisible, add the time i have spent invisible to the invisibility counter.
        // then continue to lerp the eye opening to help the player with timing.
        // if the amount of invisibility time has exceeded the allotted time,
        // set my invisibility to false.
        if (IsInvisible)
        {
            float t = invisibilityElapsed * inv_lim;
            eye.transform.localScale = Vector3.Lerp(eye.closed, eye.opened, t);
            invisibilityElapsed += Time.deltaTime;

            if (invisibilityElapsed >= invisibilityTime)
            {
                IsInvisible = false;
            }
        }
    }

    void OnAnimatorMove ()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }


    private void OnTriggerEnter(Collider other)
    {
        // new: if in contact with the invisiblizer, close the eyeball, play the 
        // sound of the invisiblizer, and set self to invisible. 
        if (other.gameObject.CompareTag("Invisiblizer")) {
            eye.Close();
            eye.playAudio();
            IsInvisible = true;
            invisibilityElapsed = 0f;
        }
    }

}