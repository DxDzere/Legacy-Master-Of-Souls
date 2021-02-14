using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[Header("Raycast")]
	public float visionRadius;
	public float attackRadius;

	public float speed;

	GameObject enemy;

	public SpriteRenderer spriteRenderer;

	void Start(){
		enemy = GameObject.FindGameObjectWithTag("Enemy");
	}

	void Update () {
		//en el de vicion lo mira para tenerlo siempre en rango(lo pense por la idea de daño a distancia distancia de ultima se saca) y el de ataque para que ataque corta
		RaycastHit2D hit = Physics2D.Raycast (
			transform.position,
			enemy.transform.position - transform.position,
			visionRadius,
			1 << LayerMask.NameToLayer ("Default")
		);

		Vector3 forward = transform.TransformDirection (enemy.transform.position - transform.position);
		/*Debug.DrawRay (transform.position, forward, Color.red);*/
		//si si estaba biendo algo nomas pero creo que se me borro parte del codigo xD
		if(hit.collider != null){
			if(hit.collider.tag == "Enemy"){
				//si esto es del raycast aca se activa primero la vicion lo que seria del enemigo el movimiento
			}
		}

		float distance = Vector3.Distance (enemy.transform.position, transform.position);
		Vector3 dir = (enemy.transform.position - transform.position).normalized;

		if (distance < attackRadius) {
			
		}

		/*Debug.DrawLine (transform.position, enemy.transform.position, Color.green);*/

		if(Input.GetKey("a"))
		{ 
			gameObject.transform.Translate (-speed, 0, 0);
			spriteRenderer.flipX = true;
		}
		if(Input.GetKey("d"))
		{ 
			gameObject.transform.Translate (speed, 0, 0);
			spriteRenderer.flipX = false;
		}
		if(Input.GetKey("w"))
		{ 
			gameObject.transform.Translate (0,speed,0);
		}
		if(Input.GetKey("s"))
		{ 
			gameObject.transform.Translate (0,-speed, 0);
		}
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, visionRadius);
		Gizmos.DrawWireSphere (transform.position, attackRadius);
	}
}
