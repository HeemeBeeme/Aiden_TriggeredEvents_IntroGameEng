using UnityEngine;
using UnityEngine.EventSystems;

public class Flashlight : MonoBehaviour
{

    [SerializeField]
    float raycastDistance;
    [SerializeField]
    float dimSpeed = 0.5f;
    [SerializeField]
    (float, float) flashlightMinMax= (0.3f, 5);

    public Light flashlight;

    public AudioClip FlashlightOn;
    public AudioClip FlashlightOff;

    public bool lightActivity = true;
    void Update()
    {
        RaycastHit hit;
        Ray raycast = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(raycast, out hit))
        {
            raycastDistance = hit.distance;

            flashlight.intensity = Mathf.Clamp(Mathf.Lerp(flashlight.intensity, raycastDistance, dimSpeed), flashlightMinMax.Item1, flashlightMinMax.Item2);
        }
    }
}
