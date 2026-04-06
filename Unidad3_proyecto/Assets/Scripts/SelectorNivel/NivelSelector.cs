using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NivelSelector : MonoBehaviour
{
    public GameObject nivelButtonPrefab;
    public Transform buttonContainer;
    public int totalLevels = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenereteLevelButtons();
    }

    // Update is called once per frame
    void GenereteLevelButtons()
    {
        for (int i = 1; i <= totalLevels; i++)
        {
            GameObject buttonObj = Instantiate(nivelButtonPrefab, buttonContainer);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();

            int levelIndex = i;
            buttonObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Nivel_" + levelIndex);
            }
            );
        }
    }
}
