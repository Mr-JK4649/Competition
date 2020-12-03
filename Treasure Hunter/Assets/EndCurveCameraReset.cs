using UnityEngine;

public class EndCurveCameraReset : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2;
    public GameObject camera3;

    private Vector3 cam1;
    private Vector3 cam2;
    private Vector3 cam3;


    // Start is called before the first frame update
    void Start()
    {
        cam1 = camera1.transform.localPosition;
        cam2 = camera2.transform.localPosition;
        cam3 = camera3.transform.localPosition;
    }

    public void CameraPosReset() {
        camera1.transform.localPosition = cam1;
        camera2.transform.localPosition = cam2;
        camera3.transform.localPosition = cam3;
    }
}
