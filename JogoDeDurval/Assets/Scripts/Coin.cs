using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource aud;

    public void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            aud.PlayOneShot(clip);
            Destroy(gameObject, 0.2f);
        }
    }
}
