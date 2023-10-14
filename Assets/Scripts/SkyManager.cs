using UnityEngine;

public class SkyManager : MonoBehaviour
{
    [SerializeField] private float skySpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skySpeed);
    }
}
