using System.Collections;
using UnityEngine;

public class EffectsCamera : MonoBehaviour
{
    public AnimationCurve curve;
    public float shakeDuration;
    public bool start = false;

    public float movementSpeed = 0.4f; 
    public float movementMagnitude = 0.4f;

    private Camera cam;
    private bool startElapsedTwo = false;

    private Vector3 initialPosition;

    private void Start()
    {
        cam = GetComponent<Camera>();

        initialPosition = transform.position;
    }

    private void Update()
    {
        // Subtle camera movement
        float xMovement = Mathf.Sin(Time.time * movementSpeed) * movementMagnitude;
        float yMovement = Mathf.Cos(Time.time * movementSpeed) * movementMagnitude;
        
        transform.position = initialPosition + new Vector3(xMovement, yMovement, 0);
    }

    public IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / shakeDuration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }

    public IEnumerator CameraZoom(float start, float target, float duration)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            cam.fieldOfView = Mathf.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
        }

        if (elapsed >= duration)
        {
            startElapsedTwo = true;
        }

        float elapsedTwo = 0.0f;

        while (elapsedTwo < duration && startElapsedTwo)
        {
            cam.fieldOfView = Mathf.Lerp(target, start, elapsedTwo / duration);
            elapsedTwo += Time.deltaTime;
            yield return null;
        }
    }
}
