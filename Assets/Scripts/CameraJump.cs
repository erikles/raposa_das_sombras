using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void LateUpdate()
    {
        // Update the camera's position to follow the player
        if (playerTransform != null)
        {
            gameObject.transform.position = Vector3.Lerp(transform.position, playerTransform.position + offset, Time.deltaTime);
            gameObject.transform.position = new Vector3(
                gameObject.transform.position.x,
                gameObject.transform.position.y,
                -10f
            );
        }
    }
}
