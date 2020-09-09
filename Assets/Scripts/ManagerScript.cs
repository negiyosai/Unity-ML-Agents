using UnityEngine;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    public static ManagerScript instance;

    [SerializeField] public Text _fpsText;
    [SerializeField] public float _hudRefreshRate = 1f;
    private float _timer;

    public Text scoreText;
    [HideInInspector] public int score = 0;
    [SerializeField] public Text objectSpawnedText;
    [HideInInspector] public int objectCounter = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        scoreText.text = "Score: " + score;
        objectSpawnedText.text = "Objects Spawned: " + objectCounter.ToString();
    }
    private void Update()
    {
        if (Time.unscaledTime > _timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            _fpsText.text = "FPS: " + fps;
            _timer = Time.unscaledTime + _hudRefreshRate;
        }

    }

    public void SetText()
    {
        objectCounter++;
        objectSpawnedText.text = "Objects Spawned: " + objectCounter.ToString();
    }
}