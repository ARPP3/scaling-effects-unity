using System.Collections;
using UnityEngine;

public class DemoCubeMovement : MonoBehaviour
{
    private Vector3 startPosition;

    public float hoverHeight = 2f;
    public float hoverTime = 1.5f;
    public float rotationSpeed = 0.05f;

    private void Start()
    {
        startPosition = transform.position;

        StartCoroutine(HoverRoutine());
    }

    private float GetSmoothInterpolation(float a, float b, float t)
    {
        return Mathf.SmoothStep(0.0f, 1.0f, Mathf.InverseLerp(a, b, t));
    }

    IEnumerator HoverRoutine()
    {
        while (true)
        {
            float startTime = Time.realtimeSinceStartup;
            float endTime = startTime + hoverTime;

            // Move up slowly
            while (Time.realtimeSinceStartup < endTime)
            {
                transform.position = Vector3.Lerp(
                    startPosition,
                    startPosition + Vector3.up * hoverHeight,
                    GetSmoothInterpolation(startTime, endTime, Time.realtimeSinceStartup));

                RotateObject();
                yield return null;
            }

            transform.position = startPosition + Vector3.up * hoverHeight;

            // Hover in place
            startTime = Time.realtimeSinceStartup;
            endTime = startTime + hoverTime;
            while (Time.realtimeSinceStartup < endTime)
            {
                RotateObject();
                yield return null;
            }

            // Move down
            startTime = Time.realtimeSinceStartup;
            endTime = startTime + hoverTime;
            while (Time.realtimeSinceStartup < endTime)
            {
                transform.position = Vector3.Lerp(
                    startPosition + Vector3.up * hoverHeight,
                    startPosition,
                    GetSmoothInterpolation(startTime, endTime, Time.realtimeSinceStartup));

                RotateObject();
                yield return null;
            }

            transform.position = startPosition;

            // Hover in place
            startTime = Time.realtimeSinceStartup;
            endTime = startTime + hoverTime;
            while (Time.realtimeSinceStartup < endTime)
            {
                RotateObject();
                yield return null;
            }
        }
    }

    void RotateObject()
    {
        float rotationX = Mathf.PerlinNoise(Time.time * rotationSpeed, 0) * 360f;
        float rotationY = Mathf.PerlinNoise(0, Time.time * rotationSpeed) * 360f;
        float rotationZ = Mathf.PerlinNoise(Time.time * rotationSpeed, Time.time * rotationSpeed) * 360f;

        transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
    }
}
