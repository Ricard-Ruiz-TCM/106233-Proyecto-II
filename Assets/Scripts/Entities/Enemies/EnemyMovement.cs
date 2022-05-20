using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour , ISlowMo
{
    protected float _speed;

    protected bool _slowMo;

    public void DisableSlowMo()
    {
        _slowMo = false;
        _speed *= 2;
    }

    public void EnableSlowMo()
    {
        _slowMo = true;
        _speed /= 2;
    }

    public bool IsSlowTime()
    {
        return _slowMo;
    }


}
