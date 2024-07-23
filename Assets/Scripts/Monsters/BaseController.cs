using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] protected Vector3 destPos;
    [SerializeField] protected Define.State state = Define.State.Idle;
    [SerializeField] protected GameObject target;
    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;
    

    public virtual Define.State State
    {
        get { return state;}
        set
        {
            state = value;

            Animator anim = GetComponent<Animator>();
            switch (state)
            {
                case Define.State.Idle:
                    break;
                case Define.State.Moving:
                    break;
                case Define.State.Jumping:
                    break;
                case Define.State.Die:
                    break;
                case Define.State.Attack:
                    break;
            }
        }
    }
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Moving:
                UpdateMoving();
                break;
            case Define.State.Jumping:
                UpdateJumping();
                break;
            case Define.State.Die:
                UpdateDie();
                break;
            case Define.State.Attack:
                UpdateAttack();
                break;
            case Define.State.Target:
                UpdateTarget();
                break;
            
                
        }
    }

    public abstract void Init();
    protected virtual void UpdateIdle(){}
    protected virtual void UpdateMoving(){}
    protected virtual void UpdateJumping(){}
    protected virtual void UpdateDie(){}
    protected virtual void UpdateAttack(){}
    protected virtual void UpdateTarget(){}
}
