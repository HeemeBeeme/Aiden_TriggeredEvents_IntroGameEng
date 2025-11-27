using UnityEngine;
using UnityEngine.Playables;

public class deathScript : MonoBehaviour
{
    public PlayableDirector Timeline;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Timeline.Play();
            Destroy(this.gameObject);
        }
    }
}
