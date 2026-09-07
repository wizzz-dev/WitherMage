using UnityEngine;

public class RotatingSprites : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5.0f;
    private void Update()
    {
        transform.Rotate(0, rotationSpeed, 0 * Time.deltaTime,Space.World);
    }
}
