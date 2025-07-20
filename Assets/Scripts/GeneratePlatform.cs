using UnityEngine;

public class GeneratePlatform : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab;
    [SerializeField] GameObject platformWin;
    [SerializeField] GameObject diamondPrefab;
    [SerializeField] int numberOfPlatforms = 20;
    [SerializeField] private float[] columnPositions = { -2.5f, 0f, 2.5f };

    // MELHORIA: Variável pública para que outros scripts saibam onde o jogador deve começar.
    public Vector3 startPlatformPosition { get; private set; }

    void Awake() // Mudamos de Start para Awake para garantir que as plataformas existam antes do Player tentar se posicionar.
    {
        GeneratePlatforms();
    }

    void GeneratePlatforms()
    {
        // Guardamos a posição da primeira plataforma
        startPlatformPosition = new Vector3(0, -3f, 0);
        Instantiate(platformPrefab, startPlatformPosition, Quaternion.identity);

        Vector3 platformPosition = startPlatformPosition;

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            platformPosition.y += Random.Range(1.8f, 2.5f);
            int columnIndex = Random.Range(0, columnPositions.Length);
            platformPosition.x = columnPositions[columnIndex];
            Instantiate(platformPrefab, platformPosition, Quaternion.identity);

            if (Random.Range(0, 4) == 0)
            {
                Vector3 diamondPosition = platformPosition;
                diamondPosition.y += 1f;
                Instantiate(diamondPrefab, diamondPosition, Quaternion.identity);
            }
        }

        Vector3 winPosition = platformPosition;
        winPosition.y += 2f;
        Instantiate(platformWin, winPosition, Quaternion.identity);

        PermanentUI.perm.CountGemsInScene();
    }
}