using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField]private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    [SerializeField, Range(0,24)] private float TimeOfDay;

    private float timescaler = 0.25f;

    private void OnValidate()
    {
        if (DirectionalLight != null) return;
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                }

            }
        }
    }

    private void Update()
    {
        if (Preset == null) return;
        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime * timescaler;
            TimeOfDay %= 24;
            updatelighting(TimeOfDay / 24);
            
        }
        else
        {
            updatelighting(TimeOfDay / 24);
        }
    }

    private void updatelighting(float TimePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(TimePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(TimePercent);


        if (DirectionalLight != null)
        {
            
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(TimePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(
                new Vector3((TimePercent * 360f) - 90f, 170f, 0f));
        }
    }
}
