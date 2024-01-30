using System.Collections;
using UnityEngine;

public class DemoScale : MonoBehaviour
{
    public float scaleTime = 1.5f;
    public float rotationSpeed = 0.05f;

    public bool rotateOnly = true;

    public Transform childCube;

    Vector3 FlattenedScale => new Vector3(1.0f, 1.0f, 0.01f);

    private void Start()
    {
        if (!rotateOnly)
        {
            StartCoroutine(ScaleRoutine());
        }

        StartCoroutine(RotateCube());
    }

    private float GetSmoothInterpolation(float a, float b, float t)
    {
        return Mathf.SmoothStep(0.0f, 1.0f, Mathf.InverseLerp(a, b, t));
    }

    IEnumerator RotateCube()
    {
        while (true)
        {
            RotateObject();
            yield return null;
        }
    }

    IEnumerator ScaleRoutine()
    {
        while (true)
        {
            float startTime = Time.realtimeSinceStartup;
            float endTime = startTime + scaleTime;

            // Scale down slowly
            while (Time.realtimeSinceStartup < endTime)
            {
                transform.localScale = Vector3.Lerp(
                    Vector3.one,
                    FlattenedScale,
                    GetSmoothInterpolation(startTime, endTime, Time.realtimeSinceStartup));
                yield return null;
            }

            transform.localScale = FlattenedScale;

            // Hover in place
            startTime = Time.realtimeSinceStartup;
            endTime = startTime + scaleTime;
            while (Time.realtimeSinceStartup < endTime)
            {
                yield return null;
            }

            // Move down
            startTime = Time.realtimeSinceStartup;
            endTime = startTime + scaleTime;
            while (Time.realtimeSinceStartup < endTime)
            {
                transform.localScale = Vector3.Lerp(
                    FlattenedScale,
                    Vector3.one,
                    GetSmoothInterpolation(startTime, endTime, Time.realtimeSinceStartup));
                yield return null;
            }

            transform.localScale = Vector3.one;

            // Hover in place
            startTime = Time.realtimeSinceStartup;
            endTime = startTime + scaleTime;
            while (Time.realtimeSinceStartup < endTime)
            {
                yield return null;
            }
        }
    }

    void RotateObject()
    {
        float rotationX = Mathf.PerlinNoise(Time.time * rotationSpeed, 0) * 360f;
        float rotationY = Mathf.PerlinNoise(0, Time.time * rotationSpeed) * 360f;
        float rotationZ = Mathf.PerlinNoise(Time.time * rotationSpeed, Time.time * rotationSpeed) * 360f;

        childCube.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
    }
}
