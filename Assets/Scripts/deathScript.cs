using UnityEngine;

public class deathScript : MonoBehaviour
{
    public GameObject DeathUI;
    public GameObject PlayUI;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            DeathUI.SetActive(true);
            PlayUI.SetActive(false);
        }
    }
}
