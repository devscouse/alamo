using UnityEngine;

public class PlayerCubeShaderManager : MonoBehaviour
{
    public PlayerController playerController;
    public Material mat;
    public int resolution = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Texture2D tex = new Texture2D(resolution, resolution);
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                tex.SetPixel(x, y, Color.black);
            }
        }
        tex.Apply();
        mat.mainTexture = tex;
        playerController.PlayerFired += SetFireTime;
        mat.SetFloat("_LastFireTime", -10.0f);
        mat.SetFloat("_CurrTime", Time.time);
    }

    void SetFireTime()
    {
        Debug.Log("SetFireTime()");
        mat.SetFloat("_LastFireTime", Time.time);
    }

    void Update()
    {
        mat.SetFloat("_CurrTime", Time.time);
    }
}
