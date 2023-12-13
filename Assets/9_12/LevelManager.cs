using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons; // T�m b�l�m butonlar�
    public Image[] lockImages;    // T�m kilit resimleri
    public Image[] bronzeMedals;  // T�m bronz madalya g�rselleri
    public Image[] silverMedals;  // T�m g�m�� madalya g�rselleri
    public Image[] goldMedals;    // T�m alt�n madalya g�rselleri

    private int lastCompletedLevel = 0; // Tamamlanan son b�l�m
    private int[] medalLevels; // Her b�l�m i�in kazan�lan madalya seviyesi

    void Start()
    {
        // PlayerPrefs'ten son tamamlanan b�l�m� al
        lastCompletedLevel = PlayerPrefs.GetInt("LastCompletedLevel", 0);

        medalLevels = new int[levelButtons.Length];

        for (int i = 0; i < levelButtons.Length; i++)
        {
            medalLevels[i] = PlayerPrefs.GetInt("MedalLevel" + i, 0);
        }

        // Ba�lang��ta sadece ilk b�l�m butonunu etkinle�tir
        for (int i = 1; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
            lockImages[i].gameObject.SetActive(true); // Kilit resmini g�ster
            bronzeMedals[i].gameObject.SetActive(false); // Bronz madalyay� gizle
            silverMedals[i].gameObject.SetActive(false); // G�m�� madalyay� gizle
            goldMedals[i].gameObject.SetActive(false); // Alt�n madalyay� gizle

        }

        // �u ana kadar tamamlanan b�l�mlerin butonlar�n� etkinle�tir
        for (int i = 1; i <= lastCompletedLevel; i++)
        {
            levelButtons[i].interactable = true;
            lockImages[i].gameObject.SetActive(false); // Kilit resmini gizle
            DisplayMedal(i, medalLevels[i]); // Kazan�lan madalyay� g�ster

        }
    }

    // B�l�m y�kleme fonksiyonu
    public void LoadLevel(int levelIndex)
    {
        if (levelIndex-1 <= lastCompletedLevel)
        {
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            Debug.Log("Bu b�l�m hen�z tamamlanmad�.");
        }
    }

    // Kazan�lan madalyay� g�sterme fonksiyonu
    private void DisplayMedal(int levelIndex, int medalLevel)
    {
        if (medalLevel == 1) silverMedals[levelIndex].gameObject.SetActive(true);
        else if (medalLevel == 2) goldMedals[levelIndex].gameObject.SetActive(true);
        else bronzeMedals[levelIndex].gameObject.SetActive(true);
    }

    //// B�l�m tamamland���nda �a�r�lacak fonksiyon
    //public void CompleteLevel(int levelIndex)
    //{
    //    // Tamamlanan son b�l�m� g�ncelle
    //    lastCompletedLevel = Mathf.Max(lastCompletedLevel, levelIndex);

    //    // PlayerPrefs'te son tamamlanan b�l�m� sakla
    //    PlayerPrefs.SetInt("LastCompletedLevel", lastCompletedLevel);
    //    PlayerPrefs.Save();

    //    //// �u ana kadar tamamlanan b�l�mlerin butonlar�n� etkinle�tir
    //    //for (int i = 1; i <= lastCompletedLevel; i++)
    //    //{
    //    //    levelButtons[i].interactable = true;
    //    //    lockImages[i].gameObject.SetActive(false); // Kilit resmini gizle
    //    //}
    //}
}
