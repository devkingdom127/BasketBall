using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	public GameObject tempBasketBall;
	public Transform ballPoint;
	public Transform hoopPoint;
	public float shootSpeed;
	public float shootPathMaxPoint;
	public float jumpHeight;

	private Animation anim;
	private GameObject basketBall;
	private bool isShootting = false;

	//[HideInInspector]
	private GameObject currentBall;

	private Vector3 shootForce;

	void Start()
	{
		anim = GetComponent<Animation>();
		foreach (AnimationState state in anim)
		{
			state.speed = 1.0F;
		}

		//Init();
		//transform.LookAt(hoopPoint.position);
		UpdateManRotation();
	}

	void Init()
	{
		if (basketBall != null)
		{
			Destroy(basketBall);
		}
		basketBall = Instantiate(tempBasketBall, ballPoint.position, ballPoint.rotation) as GameObject;
		basketBall.transform.parent = ballPoint;
		basketBall.GetComponent<Rigidbody>().useGravity = false;
	}

	public void setCurrentBall(GameObject ball)
	{
		currentBall = ball;
		currentBall.transform.position = ballPoint.position;
		currentBall.transform.rotation = transform.rotation;
		currentBall.transform.parent = ballPoint;
	}

	public void setForce(Vector3 force)
	{
		shootForce = force;
	}

	public bool isShoot()
	{
		return isShootting;
	}

	public void shootStart()
	{
		isShootting = true;
		anim.PlayQueued("aim");
		anim.PlayQueued("fire");
        
	}
    
	public void shootEndEvent()
	{
		Vector3 dir = hoopPoint.position - ballPoint.position;
		Quaternion wantedRotation = Quaternion.LookRotation(dir);

		float deg = wantedRotation.eulerAngles.y;
		Vector3 newVector = Quaternion.AngleAxis(deg, Vector3.up) * shootForce;

		currentBall.GetComponent<BSKBall>().audioSource.PlayOneShot(BSKSoundController.data.ballWoofs[Random.Range(0, BSKSoundController.data.ballWoofs.Length)], 1);

		currentBall.GetComponent<BSKBall>().isReady = false;
		Rigidbody r = currentBall.GetComponent<Rigidbody>();
		r.isKinematic = false;
		r.AddTorque(-30, 0, 0);
		r.AddForce(newVector, ForceMode.Impulse);

		currentBall.transform.parent = null;
		currentBall = null;
		BSKShooter.shooter.currentBall = null;
		BSKShooter.shooter.shootStart = false;
	}

	public void animOver()
	{
		Debug.Log("shoot over");
		isShootting = false;
	}

	void UpdateManRotation()
	{
		Vector3 dir = hoopPoint.position - transform.parent.position;
		Quaternion wantedRotation = Quaternion.LookRotation(dir);
		wantedRotation.x = transform.parent.rotation.x;
		wantedRotation.z = transform.parent.rotation.z;

		transform.parent.rotation = wantedRotation;
	}

	void Update()
	{

		/*Vector3 center = 0.5F * (ballPoint.position + hoopPoint.position);
        center.y += shootPathMaxPoint;

        ballPoint.LookAt(center);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //GetComponent<Rigidbody>().velocity = transform.up * jumpHeight;
            Init();
            anim.PlayQueued("aim");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.Stop("aim");
            anim.PlayQueued("fire");
            basketBall.transform.parent = null;
            Rigidbody r = basketBall.GetComponent<Rigidbody>();
            r.useGravity = true;
            r.velocity = ballPoint.forward * shootSpeed;

            //Init();
        }*/
	}
}
