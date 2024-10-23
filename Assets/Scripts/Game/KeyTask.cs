using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class KeyTask : Task, IResettable
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == Constants.LAYER_PLAYER)
        {
            gameObject.SetActive(false);
            CompleteTask();
        }
    }

    public override void Reset()
    {
        gameObject.SetActive(true);
    }
}
