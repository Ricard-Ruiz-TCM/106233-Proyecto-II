using UnityEngine;

public interface ISystem {

    public void ToggleSystem();
    public void DisableSystem();
    public void EnableSystem();
    public bool IsEnabled();
    
}