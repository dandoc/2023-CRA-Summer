using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*/


public class PlayerBehavior : MonoBehaviour
{
    //컨포넌트
    public Rigidbody2D rigid;
    public Animator anim;
    public SpriteRenderer rend;
    public Transform trans;

    // 물리 변수
    public float moveSpeed;
    public float maxSpeed;
    public float jumpPower;
    public int maxJump;
    public float gravityForce;
    

    // 애니메이션 관련 변수
    public bool isMove;
    public bool isAttack;
    public bool isJump;
    public bool isAir;

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
        // 최대 속도를 넘어서면 최대 속도로 고정시김
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < -maxSpeed)
        {
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
        }

        if((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow)) && !isJump)
        {
            isJump = true;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        
        anim.SetBool("isMove", isMove);
        //anim.SetBool("isJump", isJump);
        anim.SetBool("isAttack", isAttack);

        // rigid.AddForce(Vector3.down * gravityForce);

       
        CheckisJump();
    }

    public void CheckisJump()
    // 레이케스트를 사용하여 점프 유무와 공중에 떠있는지 여부를 파악함
    {
         if(rigid.velocity.y != 0)
        {
            isAir = true;
        }

        Vector3 rayPos = new Vector3(0.21f, 0, 0);

        // Debug.DrawRay(trans.position, Vector3.down * 0.7f, new Color(0, 1, 0));
        Debug.DrawRay(trans.position - rayPos, Vector3.down * 0.7f, new Color(0, 1, 0));
        Debug.DrawRay(trans.position + rayPos, Vector3.down * 0.7f, new Color(0, 1, 0));

        if (rigid.velocity.y < 0)
        {

            RaycastHit2D[] rayHit = new RaycastHit2D[2];
            rayHit[0] = Physics2D.Raycast(trans.position - rayPos, Vector2.down, 0.7f, LayerMask.GetMask("Platform"));
            rayHit[1] = Physics2D.Raycast(trans.position + rayPos, Vector2.down, 0.7f, LayerMask.GetMask("Platform"));

            if((rayHit[0].collider != null && rayHit[0].distance < 0.35f) || (rayHit[1].collider != null && rayHit[1].distance < 0.35f))
            {
                isJump = false;
                isAir = false;
            }

        }

        if(isAir)
        {
            RaycastHit2D[] rayHit = new RaycastHit2D[2];
            rayHit[0] = Physics2D.Raycast(trans.position - rayPos, Vector3.down, 0.7f, LayerMask.GetMask("Platform"));
            rayHit[1] = Physics2D.Raycast(trans.position + rayPos, Vector3.down, 0.7f, LayerMask.GetMask("Platform"));

            if((rayHit[0].collider != null && rayHit[0].distance < 0.35f) || (rayHit[1].collider != null && rayHit[1].distance < 0.35f))
            {
                isAir = false;
            }

        }
    }

}