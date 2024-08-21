using UnityEngine;

public class CameraSmoother : MonoBehaviour
{
    public Transform target;

    public float delay = 0.025f;
    public Vector3 offset;

    private void LateUpdate()
    {
        Vector3 anlikPos = target.position + offset;
        Vector3 yumosPos = Vector3.Lerp(transform.position, anlikPos, delay);
        transform.position = yumosPos;
    }
}