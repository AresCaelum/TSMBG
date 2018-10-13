using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsComponent : MonoBehaviour
{
    private Rigidbody2D _rb;//We cannot access this, used to store the rigidbody.
    public Rigidbody2D RB//We can access this, acquires our rigidbody (if any) and returns it.
    {
        get
        {
            if (_rb == null)
            {
                _rb = gameObject.GetComponent<Rigidbody2D>();
                if(_rb == null)
                {
                    _rb = gameObject.AddComponent<Rigidbody2D>();
                }
            }
            return _rb;
        }
    }
}