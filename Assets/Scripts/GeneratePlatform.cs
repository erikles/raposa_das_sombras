using UnityEngine;

public class GeneratePlatform : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab;
    [SerializeField] GameObject platformWin;
    [SerializeField] GameObject diamondPrefab; // Adicione o prefab do diamante
    [SerializeField] int numberOfPlatforms = 5;

    void Start()
    {
        GeneratePlatforms();
    }

    void GeneratePlatforms()
    {
        Vector3 platformPosition = new Vector3(0, -3f, 0);
        Instantiate(platformPrefab, platformPosition, Quaternion.identity);

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            platformPosition.y += Random.Range(1.6f, 2f);
            platformPosition.x += Random.Range(-5f, 5f);
            Instantiate(platformPrefab, platformPosition, Quaternion.identity);

            // Instancia um diamante a cada 5 plataformas
            if ((i + 1) % 5 == 0)
            {
                Vector3 diamondPosition = platformPosition;
                diamondPosition.x += Random.Range(-1f, 1f); // Próximo à plataforma
                diamondPosition.y += 1f; // Um pouco acima da plataforma
                Instantiate(diamondPrefab, diamondPosition, Quaternion.identity);
            }
        }

        // Instancia a plataforma de vitória um pouco acima da última
        Vector3 winPosition = platformPosition;
        winPosition.y += 2f;
        Instantiate(platformWin, winPosition, Quaternion.identity);

        // Chama a função para contar os coletáveis na cena
        PermanentUI.perm.CountGemsInScene();
    }
}
