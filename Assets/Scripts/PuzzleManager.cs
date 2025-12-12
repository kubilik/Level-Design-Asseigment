using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    // Singleton deseni: Diğer scriptlerin kolayca erişmesini sağlar
    public static PuzzleManager Instance;

    // Unity editöründen buraya çıkış kapısı objesini sürükleyin
    [Header("Kapı Ayarları")]
    public GameObject exitDoor;
    public float openSpeed = 2f; // Kapı açılma hızı

    [Header("Pano Durumu")]
    private int activatedPanelCount = 0;
    private const int TOTAL_PANELS_REQUIRED = 3;

    private bool isDoorOpen = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Bir pano aktif edildiğinde PanelInteraction tarafından çağrılacak
    public void PanelActivated()
    {
        activatedPanelCount++;
        Debug.Log("Aktif Panel Sayısı: " + activatedPanelCount);

        if (activatedPanelCount >= TOTAL_PANELS_REQUIRED)
        {
            OpenExitDoor();
        }
    }

    private void OpenExitDoor()
    {
        if (isDoorOpen) return;

        isDoorOpen = true;
        Debug.Log("Tüm paneller aktif! Çıkış kapısı açılıyor.");
        // Gerekirse başka bir ses veya görsel efekt tetikleyin
    }

    // Kapıyı hareket ettirme mantığı Update içinde
    private void Update()
    {
        if (isDoorOpen && exitDoor != null)
        {
            // Kapıyı yavaşça yukarı (Y ekseninde) kaydırarak aç
            // Yerine göre (Rotation, Active/Passive vb.) farklı kapı açma mekaniği kullanabilirsiniz.
            exitDoor.transform.Translate(Vector3.up * openSpeed * Time.deltaTime);

            // Eğer kapı belirli bir yüksekliğe ulaştıysa durdur
            // Örnek: Eğer kapı 10 birim yukarı kaydıysa durdur.
            if (exitDoor.transform.position.y > 10f)
            {
                // Kapı artık yeterince yüksekte. Kapının daha fazla hareket etmesini engelle.
            }
        }
    }
}