using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public enum Particles
    {
        sparks,
        bloodSplatter,
        bloodSplatterOnHit,
        dustLand
    }
    
    
    public static ParticleManager Instance;

    
    public GameObject sparksParticle;
    public GameObject bloodSplatter;
    public GameObject bloodSplatterOnHit;
    public GameObject dustLand;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnParticle(Transform _pos, Particles _particle)
    {
        switch (_particle)
        {
            case Particles.sparks:
                Instantiate(sparksParticle, _pos);
                return;
            case Particles.bloodSplatter:
                Instantiate(bloodSplatter, _pos);
                return;
            case Particles.bloodSplatterOnHit:
                Instantiate(bloodSplatterOnHit, _pos);
                return;
            case Particles.dustLand:
                Instantiate(dustLand, _pos);
                return;
                
        }
    }


}
