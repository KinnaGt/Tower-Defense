using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Image fillAmountImage;
    [SerializeField] WaveManager waveManager;
    
    // Start is called before the first frame update
    void Start()
    {
        fillAmountImage = GetComponent<Image>();
    }
    void Update()
    {
        // Debug.Log(waveManager.GetRemainingWaves() + " Of " + waveManager.GetTotalWaves());
        int remainingWaves = waveManager.GetRemainingWaves();
        float maxWaves = waveManager.GetTotalWaves();
        // Debug.Log(remainingWaves / maxWaves);

        fillAmountImage.fillAmount = remainingWaves / maxWaves;
    }
}
