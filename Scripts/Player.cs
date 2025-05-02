using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Crate crate;
    
    
    public void Select(InputAction.CallbackContext context){
        if(context.canceled && crate != null){
            crate.ToggleUI();
        }
    }

}
