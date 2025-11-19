using UnityEngine;
using UnityEngine.EventSystems;

public class Flashlight : MonoBehaviour
{

    [SerializeField]
    float raycastDistance;
    [SerializeField]
    float dimTime = 0.5f;
    [SerializeField]
    Light flashlight;

    public AudioClip FlashlightOn;
    public AudioClip FlashlightOff;

    public bool lightActivity = true;

    void Start()
    {
        flashlight = GetComponent<Light>();
    }
    void Update()
    {
        RaycastHit hit;
        Ray raycast = new Ray(transform.position, transform.forward);


        if(!Controller.Instance.m_IsPaused)
        {
            if (lightActivity)
            {
                if (Physics.Raycast(raycast, out hit))
                {
                    raycastDistance = hit.distance - 0.3f;

                    if (raycastDistance > 5)
                    {
                        raycastDistance = 5;
                    }

                    flashlight.intensity = Mathf.Lerp(flashlight.intensity, raycastDistance, dimTime);

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        lightActivity = false;
                        flashlight.intensity = 0;
                        GameManager.Instance.FlashOffSource.Play();
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Physics.Raycast(raycast, out hit);

                    raycastDistance = hit.distance - 0.3f;

                    if (raycastDistance > 5)
                    {
                        raycastDistance = 5;
                    }

                    flashlight.intensity = Mathf.Lerp(flashlight.intensity, raycastDistance, dimTime);

                    lightActivity = true;
                    GameManager.Instance.FlashOnSource.Play();
                }
            }
        }
       
        
    }
}
