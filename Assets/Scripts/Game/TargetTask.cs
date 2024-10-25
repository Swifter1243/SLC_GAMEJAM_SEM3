using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(AudioSource))]
public class TargetTask : Task
{
    private AudioSource source;
    private Collider2D trigger;

    private void Start()
	{
        source = GetComponent<AudioSource>();
        trigger = GetComponent<Collider2D>();
	}


	public void Hit()
    {
        foreach (Transform child in transform) child.gameObject.SetActive(false); //this is bad but no time lol
        trigger.enabled = false;

        source.Stop();
        source.Play();

        CompleteTask();
    }

    public override void Reset()
    {
        trigger.enabled = true;
        foreach (Transform child in transform) child.gameObject.SetActive(true);
    }
}
