using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	[Header("Estadisticas")]
	public float vida;
	public Text textoVida;
	public float defensa;
	public float ataque;
	public float tiempoDeAtaque;
	
	[Header("Raycast")]
	public float visionRadius;
	public float attackRadius;

	public float speed;
	private bool estaEnEspera = false;
	GameObject enemy;
	private EnemyController ec;
	//GameObject gameMannager;
	//GameMannager gm;
	public SpriteRenderer spriteRenderer;
	public LayerMask mask;
	private Animator anim;
	
	void Start(){
		enemy = GameObject.FindGameObjectWithTag("Enemy");
		ec = enemy.GetComponent<EnemyController>();
		//gameMannager = GameObject.FindGameObjectWithTag("GameMannager");
		//gm = gameMannager.GetComponent<GameMannager>();
		textoVida.text = vida.ToString();
		anim = GetComponent<Animator>();
	}

	void Update () {
		
		anim.SetBool ("isWalking", false);
		anim.SetBool ("isAttack", false);
		//en el de vicion lo mira para tenerlo siempre en rango(lo pense por la idea de daño a distancia distancia de ultima se saca) y el de ataque para que ataque corta
		RaycastHit2D hit = Physics2D.Raycast (
			transform.position,
			enemy.transform.position - transform.position,
			visionRadius,
			mask
		);

		Vector3 forward = transform.TransformDirection (enemy.transform.position - transform.position);
		/*Debug.DrawRay (transform.position, forward, Color.red);*/
		//si si estaba biendo algo nomas pero creo que se me borro parte del codigo xD
		if(hit.collider != null){
			if(hit.collider.gameObject.CompareTag("Enemy")){
				//si esto es del raycast aca se activa primero la vicion lo que seria del enemigo el movimiento
			}
		}

		float distance = Vector3.Distance (enemy.transform.position, transform.position);
		Vector3 dir = (enemy.transform.position - transform.position).normalized;

		if (distance < attackRadius && estaEnEspera == false)
		{
			anim.SetBool ("isAttack", true);
			StartCoroutine(Espera());
		}

		/*Debug.DrawLine (transform.position, enemy.transform.position, Color.green);*/

		if(Input.GetKey("a"))
		{ 
			gameObject.transform.Translate (-speed*Time.deltaTime, 0, 0);
			anim.SetBool ("isWalking", true);
			spriteRenderer.flipX = false;
		}
		if(Input.GetKey("d"))
		{ 
			gameObject.transform.Translate (speed*Time.deltaTime, 0, 0);
			anim.SetBool ("isWalking", true);
			spriteRenderer.flipX = true;
		}
		if(Input.GetKey("w"))
		{ 
			gameObject.transform.Translate (0,speed*Time.deltaTime,0);
			anim.SetBool ("isWalking", true);
		}
		if(Input.GetKey("s"))
		{ 
			gameObject.transform.Translate (0,-speed*Time.deltaTime, 0);
			anim.SetBool ("isWalking", true);
		}
	}

	public void ResibirDaño(float dañoEnemigo)
	{
		float dañoRecivido = dañoEnemigo - (dañoEnemigo*defensa)/100;
		vida = vida - dañoRecivido;
		textoVida.text = vida.ToString();
		if (vida <= 0)
		{
			Mori();
		}
	}

	void Mori()
	{
		//gm.DesactivarGameObjectPlayer();
		textoVida.text = "Estoy Muerto";
	}
	
	void OnDrawGizmosSelected(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, visionRadius);
		Gizmos.DrawWireSphere (transform.position, attackRadius);
	}

	IEnumerator Espera()
	{
		estaEnEspera = true;
		ec.ResibirDaño(ataque);
		yield return new WaitForSeconds(tiempoDeAtaque);
		estaEnEspera = false;
	}
}
