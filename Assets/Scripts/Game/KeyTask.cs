using UnityEngine;


[RequireComponent(typeof(Collider2D), typeof(AudioSource))]
public class KeyTask : Task, IResettable
{

    private Collider2D trigger;
    private AudioSource source;

    private void Start()
    {
        trigger = GetComponent<Collider2D>();
        source = GetComponent<AudioSource>();
    }
	private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == Constants.LAYER_PLAYER)
        {
            trigger.enabled = false;
            foreach (Transform child in transform) child.gameObject.SetActive(false);

            source.Stop();
            source.Play();

            CompleteTask();
        }
    }

    public override void Reset()
    {
        trigger.enabled = true;
        foreach (Transform child in transform) child.gameObject.SetActive(true);
    }
}
