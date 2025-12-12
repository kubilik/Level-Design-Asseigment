using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    // Hangi panoyu açtığını belirleyen enum.
    public enum KeyType { Key1, Key2, Key3 }
    public KeyType keyType;

    // Oyuncu yakında mı?
    private bool playerIsNear = false;

    // Oyuncu script'ine erişim için
    private Player playerScript;

    // Oyuncu trigger alanına girdiğinde
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
            playerScript = other.GetComponent<Player>();

            // İPUCU: Burada oyuncuya "E tuşuna basarak anahtarı al" gibi bir UI ipucu gösterebilirsiniz.
            Debug.Log("Anahtar yakında. Almak için E'ye basın.");
        }
    }

    // Oyuncu trigger alanından çıktığında
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            playerScript = null;
            // İPUCU: Burada UI ipucunu gizleyebilirsiniz.
        }
    }

    // Her karede kontrol
    private void Update()
    {
        // 1. Oyuncu yakında mı?
        // 2. Player script'i mevcut mu?
        // 3. 'E' tuşuna basıldı mı?
        if (playerIsNear && playerScript != null && Input.GetKeyDown(KeyCode.E))
        {
            PickupKey();
        }
    }

    private void PickupKey()
    {
        // Player script'indeki ilgili bool değerini true yap
        switch (keyType)
        {
            case KeyType.Key1:
                playerScript.exitKey1 = true;
                break;
            case KeyType.Key2:
                playerScript.exitKey2 = true;
                break;
            case KeyType.Key3:
                playerScript.exitKey3 = true;
                break;
        }

        // Anahtarı oyun dünyasından kaldır (veya pasif yap)
        gameObject.SetActive(false);
        Debug.Log(keyType.ToString() + " E tuşuna basılarak toplandı!");

        // Anahtarı aldıktan sonra etkileşim durumunu sıfırla
        playerIsNear = false;
        playerScript = null;
    }
}