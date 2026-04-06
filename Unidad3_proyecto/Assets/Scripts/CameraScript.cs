using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Wizard;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position;

        position = transform.position;
        position.x = Wizard.transform.position.x;
        transform.position = position;
    }
}
