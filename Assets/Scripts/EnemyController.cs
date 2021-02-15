using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	//Variables para gestionar el radio de vision, el de ataque y la velocidad
	[Header("Raycast")]
	public float visionRadius;
	public float attackRadius;
	public float speed;
	//Vaiable para guardar al jugador
	GameObject player;

	private GameObject soldado;
	//Variable para guardar la posicion inicial
	Vector3 initialPosition;
	//Animador de cuerpo cinematico con la rotacion en z congelada
	//Animator anim;
	Rigidbody2D rb2d;
	public LayerMask mask;

	public Vector3 target;
	void Start(){
		//Recuperamos al jugador gracias al tag
		player = GameObject.FindGameObjectWithTag("Player");
		soldado = GameObject.FindGameObjectWithTag("Soldier");
		//Guardamos nuestra posicion inicial
		initialPosition = transform.position;
		
		//anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Update () {
		//Por defecto nuestro target siempre sera nuestra posicion inicial
		target = initialPosition;
		//Comprobamos un Raycast del enemigo hasta el jugador
		RaycastHit2D hit = Physics2D.Raycast (
			transform.position,
			player.transform.position - transform.position,
			visionRadius,
			//1 << LayerMask.NameToLayer ("Default")
			mask
						//Poner el propio Player en una layer distinta a Default para evitar el raycast
						//Tambien ponerl al objeto Attack y al Prefab Slash un Layer Attack
						//Sino los detectara como entorno y se mueve atras al hacer ataque
			);
		
		//Aqui podemos debuguear el Raycast
		Vector3 forward = transform.TransformDirection (target - transform.position);
		Debug.DrawRay (transform.position, forward, Color.red); //y sino en rojo

		//Debug.Log(hit.transform.position);
		//Si el RAycast encuentra al jugador lo ponemos de target
		if(hit.collider != null){
			if(hit.collider.gameObject.CompareTag("Player") || hit.collider.gameObject.CompareTag("Soldier") )
			{
				if(Vector3.Distance(player.transform.position, transform.position) < Vector3.Distance(soldado.transform.position, transform.position) )
				{
					target = player.transform.position;
				}
				else
				{
					target = soldado.transform.position;
				}
				
			}
		} 
		
		//Calculamos la distancia en direccion actual hasta el target
		float distance = Vector3.Distance (target, transform.position);
		Vector3 dir = (target - transform.position).normalized;

        //Si es el enemigo y esta en rango de ataque nos paramos y le atacamos
        if (target != initialPosition && distance < attackRadius)
        {
            //	anim.SetFloat ("movX", dir.x);
            //	anim.SetFloat ("movY", dir.y); 
            //	anim.Play ("Enemy_Walk", -1, 0); //Congela la animacion de andar
        }
        //En caso contrario nos movemos hacia el
        else
        {
            rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);

            //	//Al movernos establecemos la animacion de movimiento
            //	anim.SetFloat ("movX", dir.x);
            //	anim.SetFloat ("movY", dir.y);
            //	anim.SetBool ("walking", true);
        }

        //Una ultima comprobacion para evitar bugs forzando la posicion inicial
        if (target == initialPosition && distance < 0.02f){ 
			transform.position = initialPosition;
			//anim.SetBool ("walking", false);
		}
		//Y el debug optativo con una linea hasta el target
		Debug.DrawLine(transform.position, target, Color.green);
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, visionRadius);
		Gizmos.DrawWireSphere (transform.position, attackRadius);
	}

	void CalcularDistancia(Vector3 a, Vector3 b)
	{
		
	}
}