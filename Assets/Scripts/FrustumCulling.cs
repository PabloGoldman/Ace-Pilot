using UnityEngine;

public class FrustumCulling : MonoBehaviour
{
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void OnBecameVisible()
    {
        // The object is within the camera's frustum, so enable rendering
        gameObject.SetActive(true);
    }

    private void OnBecameInvisible()
    {
        // The object is outside the camera's frustum, so disable rendering
        gameObject.SetActive(false);
    }

    private void Update()
    {
        // Check if the object is within the camera's frustum
        if (camera != null && GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), gameObject.GetComponent<Renderer>().bounds))
        {
            // The object is within the camera's frustum, so enable rendering
            gameObject.SetActive(true);
        }
        else
        {
            // The object is outside the camera's frustum, so disable rendering
            gameObject.SetActive(false);
        }
    }
}
