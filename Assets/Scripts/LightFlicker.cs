using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField]
    Light LightObject;
    [SerializeField]
    float minIntensity = 0.5f;
    [SerializeField]
    float maxIntensity = 7.5f;
    [SerializeField]
    float intensityTiming = 0.1f;

    private float currentTimer;

    void Start()
    {
        LightObject = GetComponent<Light>();
    }
    void LateUpdate()
    {
        currentTimer += Time.deltaTime;
        if (!(currentTimer >= intensityTiming)) return;
        {
            LightObject.intensity = Random.Range(minIntensity, maxIntensity);
            currentTimer = 0;
        }
    }
}
