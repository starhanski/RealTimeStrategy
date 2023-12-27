using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public Light directLight;
    private ParticleSystem _ps;
    private bool _isRain = false;
    private void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        StartCoroutine(Weather());
    }
    private void Update()
    {
        if (_isRain && directLight.intensity > 0.25f)
        {
            LightIntensity(-1);
        }
        else if (!_isRain && directLight.intensity < 0.5f)
        {
            LightIntensity(1);
        }
    }
    public void LightIntensity(int mult)
    {
        directLight.intensity += 0.1f * Time.deltaTime * mult;
    }
    private IEnumerator Weather()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(60f, 180f));

            if (_isRain)
                _ps.Stop();
            else
                _ps.Play();

            _isRain = !_isRain;
        }

    }
}
