using System.Collections;
using TMPro;
using UnityEngine;

public class SceneUI : MonoBehaviour
{
    private BarComponentUI healthBar;
    public GameObject gameOverText;
    public GameObject waveIntroText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar = transform.Find("HealthBar").GetComponent<BarComponentUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowGameOverScreen()
    {
        gameOverText.SetActive(true);
    }

    public IEnumerator ShowWaveIntroText(int wave)
    {
        waveIntroText.GetComponent<TextMeshProUGUI>().text = "Wave " + wave;
        waveIntroText.SetActive(true);
        yield return new WaitForSeconds(2);
        waveIntroText.SetActive(false);
    }

    void ShowGameUI()
    {

    }

    void HideGameUI()
    {

    }

    public void SetPlayerHealth(float health, float maxHealth)
    {
        // Debug.Log("Display player health value of " + health / maxHealth);
        healthBar.SetDisplayValue(health / maxHealth);
    }
}
