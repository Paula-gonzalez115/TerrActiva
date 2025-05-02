using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UI.Collections;

public class quemable : MonoBehaviour
{
    private ParticleSystem fuego;
    private Image lifeBar;
    
    [SerializeField] private bool inHorno;
    public bool burning = false;
    private bool alive;
    
    [SerializeField] private GameObject carbon;
    [SerializeField] private float lifespan;

    private float timer;
    private GameObject _Player;

    void Start(){
        fuego = GetComponent<ParticleSystem>();
        timer = 0.0f;
        alive = true;
        
        lifeBar = transform.Find("Canvas").transform.Find("ProgressBar").GetComponent<Image>();
        _Player = GameObject.Find("Player").transform.Find("Camera Offset").transform.Find("Main Camera").gameObject;
    }

    void Update()
    {
        
        if (burning)
        {
            lifeBar.transform.parent.transform.position = gameObject.transform.position + new Vector3(0, 0.5f, 0);
            lifeBar.gameObject.transform.parent.transform.LookAt(_Player.transform);
            timer += Time.deltaTime;
            lifeBar.fillAmount = 1 - (timer / lifespan);
        }

        if (alive && timer >= lifespan)
        {
            try
            {
                var newCarbon = Instantiate(carbon, transform.position, Quaternion.identity);
                newCarbon.GetComponent<ParticleSystem>().Play();
                newCarbon.GetComponent<quemable>().burning = true;
            }
            catch
            {
            }

            Destroy(gameObject);
            alive = false;
        }
    }
    private void OnTriggerEnter(Collider collider){
        Debug.Log(collider.tag);
        if(collider.gameObject.CompareTag("Horno"))
        {
            inHorno = true;
        }
        else if(collider.gameObject.CompareTag("Fuego") && inHorno)
        {
            burning = true;
            lifeBar.gameObject.transform.parent.gameObject.SetActive(true);
            fuego.Play();
        }
    }

    public void StartFire()
    {
        fuego.Play();
    }

    public void StopFire()
    {
        fuego.Stop();
    }
}
