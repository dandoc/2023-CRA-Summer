using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    //컨포넌트
    public Rigidbody2D rigid;
    public Animator anim;
    public SpriteRenderer rend;
    public Transform trans;

    // 물리 변수
    public float moveSpeed;
    public float jumpPower;
    public float gravityForce;
    
    // 애니메이션 관련 변수
    public bool isMove;
    public bool isAttack;
    public bool isJump;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        trans = GetComponent<Transform>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX;
        inputX = Input.GetAxisRaw("Horizontal");
        

        if(inputX != 0)
        {
            if(inputX == 1)
            {
                rend.flipX = false;
            }
            else if(inputX == -1)
            {
                rend.flipX = true;
            }
            isMove = true;
            rigid.AddForce(new Vector2(inputX, 0) * moveSpeed, ForceMode2D.Impulse);
        }
        else
        {
            isMove = false;
        }

        if((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow)) && !isJump)
        {
            isJump = true;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        
        
        anim.SetBool("isMove", isMove);
        //anim.SetBool("isJump", isJump);
        anim.SetBool("isAttack", isAttack);

        rigid.AddForce(Vector3.down * gravityForce);
    }

    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Floor")
        {
            isJump = false;
        }
    }

}