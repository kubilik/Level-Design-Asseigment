using UnityEngine;

public class PanelInteraction : MonoBehaviour
{
    public enum PanelType { Panel1, Panel2, Panel3 }
    public PanelType panelType;

    private bool playerIsNear = false;
    // PlayerController yerine Player script'i tutulacak
    private Player playerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
            // PlayerController yerine Player kodunu al
            playerScript = other.GetComponent<Player>();
            // İPUCU: Burada oyuncuya "E tuşuna bas" gibi bir UI ipucu gösterebilirsiniz.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            playerScript = null;
            // İPUCU: Burada UI ipucunu gizleyebilirsiniz.
        }
    }

    private void Update()
    {
        // Oyuncu yakındaysa VE E tuşuna basmışsa kontrol et.
        if (playerIsNear && playerScript != null && Input.GetKeyDown(KeyCode.E))
        {
            TryActivatePanel();
        }
    }

    private void TryActivatePanel()
    {
        bool hasKey = false;

        // Hangi panonun hangi anahtara ihtiyacı olduğunu kontrol et
        switch (panelType)
        {
            case PanelType.Panel1:
                hasKey = playerScript.exitKey1;
                if (hasKey) playerScript.exitKey1 = false; // Anahtarı kullan
                break;
            case PanelType.Panel2:
                hasKey = playerScript.exitKey2;
                if (hasKey) playerScript.exitKey2 = false; // Anahtarı kullan
                break;
            case PanelType.Panel3:
                hasKey = playerScript.exitKey3;
                if (hasKey) playerScript.exitKey3 = false; // Anahtarı kullan
                break;
        }

        if (hasKey)
        {
            ActivatePanelSuccess();
        }
        else
        {
            ActivatePanelFailure();
        }
    }

    private void ActivatePanelSuccess()
    {
        Debug.Log(panelType.ToString() + " başarıyla aktive edildi! Puzzle ilerliyor.");
        // Gerekli bulmaca olaylarını tetikleyin (kapı açmak, ışık yakmak vb.)
        // Örneğin: gameObject.SetActive(false); // Panoyu pasif yap
    }

    private void ActivatePanelFailure()
    {
        Debug.Log("Bu panoyu aktive etmek için doğru anahtara sahip değilsiniz.");
        // Oyuncuya sesli veya görsel bir geri bildirim verin
    }
}