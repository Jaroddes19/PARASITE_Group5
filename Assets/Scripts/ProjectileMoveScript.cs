using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMoveScript : MonoBehaviour
{

	public float speed;
	[Tooltip("From 0% to 100%")]
	public float accuracy;
	public float fireRate;
	public GameObject muzzlePrefab;
	public GameObject hitPrefab;
	public AudioClip shotSFX;
	public AudioClip hitSFX;
	public List<GameObject> trails;

	private float speedRandomness;
	private Vector3 offset;
	private bool collided;
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		//used to create a radius for the accuracy and have a very unique randomness
		if (accuracy != 100)
		{
			accuracy = 1 - (accuracy / 100);

			for (int i = 0; i < 2; i++)
			{
				var val = 1 * Random.Range(-accuracy, accuracy);
				var index = Random.Range(0, 2);
				if (i == 0)
				{
					if (index == 0)
						offset = new Vector3(0, -val, 0);
					else
						offset = new Vector3(0, val, 0);
				}
				else
				{
					if (index == 0)
						offset = new Vector3(0, offset.y, -val);
					else
						offset = new Vector3(0, offset.y, val);
				}
			}
		}

		if (muzzlePrefab != null)
		{
			var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
			muzzleVFX.transform.forward = gameObject.transform.forward + offset;
			var ps = muzzleVFX.GetComponent<ParticleSystem>();
			if (ps != null)
				Destroy(muzzleVFX, ps.main.duration);
			else
			{
				var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
				Destroy(muzzleVFX, psChild.main.duration);
			}
		}

		if (shotSFX != null && GetComponent<AudioSource>())
		{
			GetComponent<AudioSource>().PlayOneShot(shotSFX);
		}
	}

	void FixedUpdate()
	{
		if (speed != 0 && rb != null)
			rb.position += (transform.forward + offset) * (speed * Time.deltaTime);
	}

	void OnCollisionEnter(Collision co)
	{
		Debug.Log("Collision with " + co.gameObject.name);
		if (co.gameObject.tag != "Bullet" && !collided)
		{
			collided = true;

			if (shotSFX != null && GetComponent<AudioSource>())
			{
				GetComponent<AudioSource>().PlayOneShot(hitSFX);
			}

			speed = 0;
			GetComponent<Rigidbody>().isKinematic = true;

		}
		if (gameObject.GetComponent<ProjectileAttributes>().senderID == 1)
		{
			if (co.gameObject.tag == "Player")
			{
				co.gameObject.GetComponent<PlayerHealth>().TakeDamage(gameObject.GetComponent<ProjectileAttributes>().projectileDamage);
				Destroy(gameObject);
			}
		}
		else
		{
			if (co.gameObject.tag == "Enemy")
			{
				co.gameObject.GetComponent<EnemyHit>().TakeDamage(gameObject.GetComponent<ProjectileAttributes>().projectileDamage);
				Destroy(gameObject);

			}
		}
	}

}
