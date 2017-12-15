using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	ParticleManager: Manages movement for the particles									*/
/*																						*/
/*		Functions:																		*/
/*			private:																	*/
/*				void Start () 															*/
/*				void ApplyMagneticForce(MovingParticle particle)						*/
/*																						*/
/*			public:																		*/
/*				IEnumerator Cycle(MovingParticle particle)								*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class ParticleManager : MonoBehaviour 
{
	public float cycleInterval = 0.007f;					//	Increase to make force greater because it cycles more often
    public float maxForce = 10.0f;

	private List<Particle> particles;						//	List of all particles
	private List<MovingParticle> movingParticles;			//	List of all moving particles
    [SerializeField]
    private int numberOfBlues;
    [SerializeField]
    private int currentNumberOfBlues;

    /*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	Start: Runs once at the begining of the game. Initalizes variables.					*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
    void Start () 
	{
		particles = new List<Particle>(FindObjectsOfType<Particle>());
		movingParticles = new List<MovingParticle>(FindObjectsOfType<MovingParticle>());

        numberOfBlues = movingParticles.Count;

		foreach(MovingParticle particle in movingParticles)
		{
			StartCoroutine(Cycle(particle));
		}
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	ApplyMagneticForce: Applies magnetic force to all moving particles					*/
	/*			param:																		*/
	/*				MovingParticle particle - the particle being moved						*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void ApplyMagneticForce(MovingParticle particle)
	{
		//	New force is always set at zero during the beginning 
		Vector3 newFocrce = Vector3.zero;

		//	Goes through all charged particles
		foreach(Particle thisParticle in particles)
		{
			//	Skips particle so it's force isn't applied to itself
			if (thisParticle == particle)
			{
				continue;
			}

			//	Taks distance between the current particle in the list and the particle we're applying the force to
			float distance = Vector3.Distance(particle.transform.position, thisParticle.gameObject.transform.position);
			// Coulomb's law:	Force = (charge * charge) / (Distance * Distance) 
			float force = 200 * particle.charge *  thisParticle.charge / Mathf.Pow(distance, 2);

			//	Gets direction the particle should move
			Vector3 direction = particle.transform.position - thisParticle.transform.position;
			//	Normalize vector since we don't care about the magnitude
			direction.Normalize();

			//	A Summation of all forces applied to the moving particle
			newFocrce += force * direction * cycleInterval;

			//	If force is undefined (divide by zero) the newForce is zero
			if (float.IsNaN(newFocrce.x))
			{
				newFocrce = Vector3.zero;
			}

            Vector3 addedForce = Vector3.ClampMagnitude(newFocrce, maxForce);

            //	Applies force to the Rigidbody2D
            particle.myRigidbody.AddForce(addedForce);
		}
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Cycle: Cylces through all particles to apply magnetic force							*/
	/*			param:																		*/
	/*				MovingParticle particle - the particle the force is being applied to	*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public IEnumerator Cycle(MovingParticle particle)
	{
		bool isFirst = true;
		//	Applies force as long as game is running
		while(true)
		{
			//	This statement is used to get rid of spike in processing for smooth updating
			if (isFirst)
			{
				isFirst = false;
				yield return new WaitForSeconds(Random.Range(0, cycleInterval));
			}

			//	Applies force on particle
			ApplyMagneticForce(particle);

			yield return new WaitForSeconds(cycleInterval);
		}
	}

    private void Update()
    {
        
        if(ObjectPool.bluePool.Count > 0)
        {
            ObjectPool.GetFromPool(Poolable.types.BLUE);
        }
    }
}
