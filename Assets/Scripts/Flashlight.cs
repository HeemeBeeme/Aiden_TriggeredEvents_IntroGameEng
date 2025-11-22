using UnityEngine;
using UnityEngine.EventSystems;

public class Flashlight : MonoBehaviour
{

    [SerializeField]
    float raycastDistance;
    [SerializeField]
    float dimTime = 0.5f;
    [SerializeField]
    (float, float) flashlightMinMax= (0.3f, 5);
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
        if (lightActivity)
        {
            if (Physics.Raycast(raycast, out hit))
            {
                raycastDistance = hit.distance - 0.3f;

                if (raycastDistance > flashlightMinMax.Item2)
                {
                    raycastDistance = flashlightMinMax.Item2;
                }

                flashlight.intensity = Mathf.Clamp(Mathf.Lerp(flashlight.intensity, raycastDistance, dimTime), flashlightMinMax.Item1, flashlightMinMax.Item2);

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

                if (raycastDistance > flashlightMinMax.Item2)
                {
                    raycastDistance = flashlightMinMax.Item2;
                }

                flashlight.intensity = Mathf.Clamp(Mathf.Lerp(flashlight.intensity, raycastDistance, dimTime), flashlightMinMax.Item1, flashlightMinMax.Item2);

                lightActivity = true;
                GameManager.Instance.FlashOnSource.Play();
            }
        }
       
        
    }
}
