using UnityEngine;

public class PanelInteraction : MonoBehaviour
{
    public enum PanelType { Panel1, Panel2, Panel3 }
    public PanelType panelType;

    private bool playerIsNear = false;
    private Player playerScript;

    [Header("Görsel Ayarlar")]
    // 1. Panel üzerindeki anahtarın yerleşeceği alan
    public GameObject keySlotObject;
    // 2. Panel üzerindeki durum lambası
    public GameObject panelLightObject;

    // Unity editöründen buraya aktif (YEŞİL) materyali sürükleyin
    public Material activeMaterial;

    // Panonun zaten aktif olup olmadığını tutan değişken
    public bool isPanelActivated = false; // PUZZLE MANAGER için public yaptık!

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
            playerScript = other.GetComponent<Player>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            playerScript = null;
        }
    }

    private void Update()
    {
        if (playerIsNear && playerScript != null && Input.GetKeyDown(KeyCode.E))
        {
            TryActivatePanel();
        }
    }

    private void TryActivatePanel()
    {
        if (isPanelActivated)
        {
            Debug.Log(panelType.ToString() + " zaten aktif.");
            return;
        }

        bool hasKey = false;

        switch (panelType)
        {
            case PanelType.Panel1:
                hasKey = playerScript.exitKey1;
                if (hasKey) playerScript.exitKey1 = false; // Anahtarı kullan
                break;
            case PanelType.Panel2: // Diğer panelleri ekleyin
                hasKey = playerScript.exitKey2;
                if (hasKey) playerScript.exitKey2 = false;
                break;
            case PanelType.Panel3: // Diğer panelleri ekleyin
                hasKey = playerScript.exitKey3;
                if (hasKey) playerScript.exitKey3 = false;
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
        Debug.Log(panelType.ToString() + " başarıyla aktive edildi!");
        isPanelActivated = true; // Pano aktif edildi

        // 1. Anahtar Yuvası ve Panel Işığını Yeşil Yap
        if (keySlotObject != null && activeMaterial != null)
        {
            keySlotObject.GetComponent<Renderer>().material = activeMaterial;
        }
        if (panelLightObject != null && activeMaterial != null)
        {
            panelLightObject.GetComponent<Renderer>().material = activeMaterial;
        }

        // 2. Puzzle Yöneticisine haber ver
        PuzzleManager.Instance.PanelActivated();
    }

    private void ActivatePanelFailure()
    {
        Debug.Log("Bu panoyu aktive etmek için doğru anahtara sahip değilsiniz.");
    }
}