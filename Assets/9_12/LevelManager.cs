using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons; // Tüm bölüm butonlarý
    public Image[] lockImages;    // Tüm kilit resimleri
    public Image[] bronzeMedals;  // Tüm bronz madalya görselleri
    public Image[] silverMedals;  // Tüm gümüþ madalya görselleri
    public Image[] goldMedals;    // Tüm altýn madalya görselleri

    private int lastCompletedLevel = 0; // Tamamlanan son bölüm
    private int[] medalLevels; // Her bölüm için kazanýlan madalya seviyesi

    void Start()
    {
        // PlayerPrefs'ten son tamamlanan bölümü al
        lastCompletedLevel = PlayerPrefs.GetInt("LastCompletedLevel", 0);

        medalLevels = new int[levelButtons.Length];

        for (int i = 0; i < levelButtons.Length; i++)
        {
            medalLevels[i] = PlayerPrefs.GetInt("MedalLevel" + i, 0);
        }

        // Baþlangýçta sadece ilk bölüm butonunu etkinleþtir
        for (int i = 1; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
            lockImages[i].gameObject.SetActive(true); // Kilit resmini göster
            bronzeMedals[i].gameObject.SetActive(false); // Bronz madalyayý gizle
            silverMedals[i].gameObject.SetActive(false); // Gümüþ madalyayý gizle
            goldMedals[i].gameObject.SetActive(false); // Altýn madalyayý gizle

        }

        // Þu ana kadar tamamlanan bölümlerin butonlarýný etkinleþtir
        for (int i = 1; i <= lastCompletedLevel; i++)
        {
            levelButtons[i].interactable = true;
            lockImages[i].gameObject.SetActive(false); // Kilit resmini gizle
            DisplayMedal(i, medalLevels[i]); // Kazanýlan madalyayý göster

        }
    }

    // Bölüm yükleme fonksiyonu
    public void LoadLevel(int levelIndex)
    {
        if (levelIndex-1 <= lastCompletedLevel)
        {
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            Debug.Log("Bu bölüm henüz tamamlanmadý.");
        }
    }

    // Kazanýlan madalyayý gösterme fonksiyonu
    private void DisplayMedal(int levelIndex, int medalLevel)
    {
        if (medalLevel == 1) silverMedals[levelIndex].gameObject.SetActive(true);
        else if (medalLevel == 2) goldMedals[levelIndex].gameObject.SetActive(true);
        else bronzeMedals[levelIndex].gameObject.SetActive(true);
    }

    //// Bölüm tamamlandýðýnda çaðrýlacak fonksiyon
    //public void CompleteLevel(int levelIndex)
    //{
    //    // Tamamlanan son bölümü güncelle
    //    lastCompletedLevel = Mathf.Max(lastCompletedLevel, levelIndex);

    //    // PlayerPrefs'te son tamamlanan bölümü sakla
    //    PlayerPrefs.SetInt("LastCompletedLevel", lastCompletedLevel);
    //    PlayerPrefs.Save();

    //    //// Þu ana kadar tamamlanan bölümlerin butonlarýný etkinleþtir
    //    //for (int i = 1; i <= lastCompletedLevel; i++)
    //    //{
    //    //    levelButtons[i].interactable = true;
    //    //    lockImages[i].gameObject.SetActive(false); // Kilit resmini gizle
    //    //}
    //}
}
