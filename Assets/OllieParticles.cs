using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OllieParticles : MonoBehaviour
{
	public ParticleSystem ParticleSystem;

	public void Trigger ()
	{
		DeactivateParticles();
		ParticleSystem.gameObject.SetActive(true);
		Invoke("DeactivateParticles", 2);
	}

	private void DeactivateParticles()
	{
		ParticleSystem.gameObject.SetActive(false);
	}
}
