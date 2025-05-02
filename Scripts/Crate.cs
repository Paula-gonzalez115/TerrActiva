using UnityEngine;
using UnityEngine.UI;

public class Crate : MonoBehaviour
{
    [SerializeField]
    private GameObject A_Button;
    [SerializeField]
    private GameObject Interface;
    [SerializeField] 
    private Canvas canvas;

    private GameObject controllersUI;
    private GameObject player;

    [SerializeField] private GameObject massPrefab;
    [SerializeField] private GameObject modelPrefab;
    [SerializeField] private GameObject containedObject;

    private bool countingOutside;
    private float timer;

    private bool OpenA, OpenInfo;

    private Transform cameraTransform;

    private void Start(){
        countingOutside = false;
        timer = 0.0f;
        player = gameObject;

        OpenA = false; OpenInfo = false;
        GameObject hash = transform.Find("Hash").gameObject;

        foreach(Transform child in hash.transform){
            var model = Instantiate(modelPrefab, child.transform.position, Quaternion.identity);
            model.transform.parent = child.transform;
        }
        
    }

    private void Update(){
        canvas.gameObject.transform.LookAt(cameraTransform);
        
        
        if(countingOutside){
            timer += Time.deltaTime;
            if(timer >= 2f){
                countingOutside = false;
                timer = 0;
                var mass = Instantiate(massPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
                mass.transform.parent = null;
                Debug.Log("Spawn");
            }
        }
    }

    public void ToggleUI(){
        A_Button.SetActive(!OpenA);
        OpenA = !OpenA;
        Interface.SetActive(!OpenInfo);
        OpenInfo = !OpenInfo;
    }


    private void OnTriggerEnter(Collider other){
         switch (other.gameObject.name)
        {
            case "Player":
                A_Button.SetActive(true);
                OpenA = true;
                player = other.gameObject;
                player.GetComponent<Player>().crate = GetComponent<Crate>();
                cameraTransform = player.transform;
            break;

            default:
            break;
        }
    }

    private void OnTriggerExit(Collider other){
        switch (other.tag)
        {
            case "Player":
                A_Button.SetActive(false);
                Interface.SetActive(false);
                OpenA = false;
                OpenInfo = false;
                player.GetComponent<Player>().crate = null;
                break;
            case "Cafe":
            case "Hojas":
            case "Ramas":
                countingOutside = true;
                break;
            default:
            break;
        }
    }
}
