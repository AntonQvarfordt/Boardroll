using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandParticles : MonoBehaviour
{
	public ParticleSystem ParticleSys;

	public void Activate()
	{
		Deactivate();
		ParticleSys.gameObject.SetActive(true);
		Invoke("Deactivate", 1);
	}

	public void Deactivate()
	{
		ParticleSys.gameObject.SetActive(false);

	}
}
