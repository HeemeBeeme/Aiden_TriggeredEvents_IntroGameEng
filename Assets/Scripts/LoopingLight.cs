using UnityEngine;
using UnityEngine.UIElements;

public class LoopingLight : MonoBehaviour
{

    [SerializeField]
    float axisOffset = 1;
    [SerializeField]
    float amplitude = 1;
    [SerializeField]
    float frequency = 1;
    [SerializeField]
    float X;

    [SerializeField]
    Vector3 WavePosition;


    void Wave()
    {
        WavePosition = new Vector3(0, amplitude * Mathf.Sin(frequency * X) + axisOffset, 0);

        transform.localPosition = WavePosition;
    }

    void Update()
    {
        X = Time.time * 2;
        Wave();
    }
}