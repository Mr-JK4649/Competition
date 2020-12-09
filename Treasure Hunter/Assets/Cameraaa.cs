using UnityEngine;

public class Camearaaa : MonoBehaviour
{
    [Range(-5139, 4000)] public float campos;
    public Transform cam;

    private void Update()
    {
        cam.position = new Vector3(campos,cam.position.y,cam.position.z);
    }
}
