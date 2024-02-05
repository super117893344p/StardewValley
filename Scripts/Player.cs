using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed =3 ;
    private Animator anim  ;
    private ToolbarUI toolbarUI ;
    private Vector2 direction = Vector2.zero ;
    void Awake() { anim =GetComponent<Animator>();}

    void Update()
    {
        if (direction.magnitude> 0 )
        {
             anim.SetBool("isWalking",true );
             anim .SetFloat("horizontal",direction.x);
             anim .SetFloat( "vertical",direction.y);

        }
        else
        {
            anim.SetBool( "isWalking" ,false);
        }

        if (toolbarUI .GetSelectedSlotUI()!=null &&  toolbarUI.GetSelectedSlotUI().GetData().item.type==
            ItemType.Hoe&& Input.GetKeyDown(KeyCode.Space) )
        {
            PlantManager .Instance.GroundHoe(transform.position);
            anim.SetTrigger("hoe");
        }
    }

    void FixedUpdate()
    {
        float x= Input.GetAxisRaw("Horizontal") ;
        float y= Input.GetAxisRaw("Vertical") ;
        direction =new Vector2(x,y ) ;
        transform.Translate( direction*speed *Time .deltaTime) ;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Pickable" && collision.GetComponent<Pickable>().type!=null)
        {
              InventoryManager .Instance .AddToBackpack(collision.GetComponent<Pickable>().type);
              Destroy(collision.gameObject);
        }
    }

   public void ThrowItem(GameObject prefab, int count)
    {
        for (int i = 0; i<count; i++)
        {
            GameObject go =GameObject.Instantiate(prefab ) ;
            Vector2  direction = Random.insideUnitCircle.normalized*1.2f ;
            go.transform.position= new Vector3(direction.x,direction.y,0) ;
            go.GetComponent<Rigidbody2D>().AddForce(direction*3) ;

        }



    }

}
