using UnityEngine;

public class KeyTask : Task
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == Constants.LAYER_PLAYER)
        {
            gameObject.SetActive(false);
            CompleteTask();
        }
    }

    protected override void _Reset()
    {
        gameObject.SetActive(true);
    }
}
