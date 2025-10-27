using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScripEfectoZonas : MonoBehaviour
{
    public Image traicionEffect;
    public Image gulaEffect;
    public Image avariciaEffect;
    public Image violenciaEffect;
    public Image lujuriaEffect;
    public Image herejiaEffect;
    public Image perezaEffect;

    void Start()
    {
        if (traicionEffect != null)
            traicionEffect.gameObject.SetActive(false);
        if (gulaEffect != null)
            gulaEffect.gameObject.SetActive(false);
        if (avariciaEffect != null)
            avariciaEffect.gameObject.SetActive(false);
        if (violenciaEffect != null)
            violenciaEffect.gameObject.SetActive(false);
        if (lujuriaEffect != null)
            lujuriaEffect.gameObject.SetActive(false);
        if (herejiaEffect != null)
            herejiaEffect.gameObject.SetActive(false);
        if (perezaEffect != null)
            perezaEffect.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
