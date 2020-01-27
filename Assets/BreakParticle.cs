using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakParticle : ParticleCore
{
	public ParticleSystem ParticleSysA;
	public ParticleSystem ParticleSysB;

	public void Activate ()
	{
		FinishDeactivation();
		ParticleSysA.gameObject.SetActive(true);
		ParticleSysB.gameObject.SetActive(true);
		ParticleSysA.Play();
		ParticleSysB.Play();
	}

	public void Deactivate ()
	{
		ParticleSysA.Stop();
		ParticleSysB.Stop();
		Invoke("FinishDeactivation", 2);

	}

	private void FinishDeactivation ()
	{
		ParticleSysA.gameObject.SetActive(false);
		ParticleSysB.gameObject.SetActive(false);
	}
}
