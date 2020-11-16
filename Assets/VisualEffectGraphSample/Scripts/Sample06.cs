using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample06 : MonoBehaviour
{

    [SerializeField]
    UnityEngine.VFX.VisualEffect _vfxGraph = default;

    [Range(0, 512)]
    public int EmitRateAmplitude = 128;

    public Vector3 EmitterPositionCenter      = Vector3.zero;
    public float   EmitterPositionNoiseRadius = 2.0f;

    Vector3 _currentEmitterPosition  = Vector3.zero; 
    Vector3 _previousEmitterPosition = Vector3.zero;


    void Awake()
    {
        if (_vfxGraph == null)
        {
            _vfxGraph = GetComponent<UnityEngine.VFX.VisualEffect>();
        }
    }

    void Update()
    {
        if (_vfxGraph != null)
        {
            var noiseTime = Time.time * 0.5f;
            _currentEmitterPosition = (new Vector3(
                Mathf.PerlinNoise(1, noiseTime),
                Mathf.PerlinNoise(2, noiseTime),
                Mathf.PerlinNoise(3, noiseTime)
            ) - Vector3.one * 0.5f) * EmitterPositionNoiseRadius + EmitterPositionCenter;

            var emitterDirection = _currentEmitterPosition - _previousEmitterPosition;

            _vfxGraph.SetVector3("EmitterPosition", _currentEmitterPosition);
            _vfxGraph.SetVector3("EmitterDirection", emitterDirection);

            var emitRate = EmitRateAmplitude * Mathf.PerlinNoise(0, Time.time * 0.5f);
            _vfxGraph.SetFloat("EmitRate", emitRate);


            _previousEmitterPosition = _currentEmitterPosition;
        }
    }
}
