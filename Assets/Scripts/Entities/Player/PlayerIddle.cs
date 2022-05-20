
public class PlayerIddle : PlayerState, IHaveStates {

    void Awake(){
        LoadState();
        ////////////
    }

    // IHaveStates
    public void OnEnterState(){
        EnableSystem();
        ///////////////
        _animator.SetBool("Iddle", true);
    }

    public void OnExitState(){
        _animator.SetBool("Iddle", false);
        ////////////////        
        DisableSystem();
    }

    public void OnState(){
        if (!IsEnabled()) return;
        /////////////////////////
    }

}
