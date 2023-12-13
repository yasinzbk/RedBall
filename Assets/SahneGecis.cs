using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneGecis : MonoBehaviour
{
    public Animator anim;

    public string sahneAdi = "";

    private int lastCompletedLevel;

    public int bolumSeviyesi;

    public int collectedStars = 0;

    public int starsInLevel;

    private void Start()
    {
        lastCompletedLevel = PlayerPrefs.GetInt("LastCompletedLevel", 1);
    }

    public void BolumuBitir()
    {
        // Yýldýzlara göre madalya seviyesini belirle
        int medalLevel = DetermineMedalLevel(collectedStars);

        // Tamamlanan son bölümü güncelle
        lastCompletedLevel = Mathf.Max(lastCompletedLevel, bolumSeviyesi);
    
        int oncekiMedalLevel = PlayerPrefs.GetInt("MedalLevel" + bolumSeviyesi, 0);

        if (medalLevel > oncekiMedalLevel)          // onceki kayittan daha kotu skor yapmayi engelliyor
        {
            PlayerPrefs.SetInt("MedalLevel" + bolumSeviyesi, medalLevel);
        }

        // PlayerPrefs'te son tamamlanan bölümü ve madalya seviyesini sakla
        PlayerPrefs.SetInt("LastCompletedLevel", lastCompletedLevel);

        PlayerPrefs.Save();

        anim.SetTrigger("Son");
        Invoke("SahneGec", 0.3f);
    }

    // Yýldýzlara göre madalya seviyesini belirleme fonksiyonu
    private int DetermineMedalLevel(int collectedStars)
    {
        if (collectedStars == starsInLevel) return 2; // Gümüþ madalya
        else if (collectedStars > 0 && collectedStars >= (0.5f * starsInLevel)) return 1; // Altýn madalya
        else return 0; // Bronz madalya
    }

    private void SahneGec()
    {
        SceneManager.LoadScene(sahneAdi);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Karakter")
        {
            BolumuBitir();
        }
    }

}
