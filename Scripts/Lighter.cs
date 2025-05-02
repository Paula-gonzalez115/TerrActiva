using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class Lighter : MonoBehaviour
{
    private ParticleSystem fuego;
    private bool playing;

    private TextMeshProUGUI text;
    private XRGrabInteractable interactableXR;
    void Start(){
        fuego = transform.Find("Fuego").GetComponent<ParticleSystem>();
        interactableXR = GetComponent<XRGrabInteractable>();
    }

    private void Update()
    {
        // Esta sección de acá verifica si hay algún interactor agarrando el objeto para encender las partículas
        try
        {
            var interactor = interactableXR
                .interactorsSelecting[0].ToString();
            StartFire();
        }
        catch
        {
            StopFire();
        }

    }

    public void StartFire(){
        if (!fuego.isEmitting)
        {
            fuego.Play();    
        }
        
    }
    public void StopFire(){
        fuego.Stop();
    }
}
