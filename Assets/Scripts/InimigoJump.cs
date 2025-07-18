using UnityEngine;
using System.Collections;

public class InimigoJump : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float minY = -4f;
    [SerializeField] float maxY = 4f;
    [SerializeField] Transform cameraTransform;

    [SerializeField] float tempoEntreTiros = 0.5f; 
    [SerializeField] GameObject tiroPrefab;

    void Start()
    {
        StartCoroutine(DispararTiro());
    }
    void Update()
    {
        float range = maxY - minY;
        float baseY = cameraTransform != null ? cameraTransform.position.y : 0f;
        float newY = baseY + minY + Mathf.PingPong(Time.time * speed, range);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private IEnumerator DispararTiro()
    {
        while (true)
        {
            if (tiroPrefab != null)
            {
                // Instancia o tiro na posição do inimigo, apontando para baixo
                Instantiate(tiroPrefab, transform.position, Quaternion.Euler(0, 0, -90));
            }
            yield return new WaitForSeconds(tempoEntreTiros);
        }
    }
}
