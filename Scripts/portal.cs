using UnityEngine;
using UnityEngine.SceneManagement;


public class portal : MonoBehaviour
{
    public void OnTriggerEnter(Collider collider){
        if(collider.gameObject.name == "Player"){
        SceneManager.LoadScene("TerrActiva");
        }
    }
}
