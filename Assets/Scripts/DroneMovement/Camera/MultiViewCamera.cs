using UnityEngine;

public class MultiViewCamera : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        RenderTexture temp = RenderTexture.GetTemporary(src.width, src.height);

        // Render the first camera view
        camera1.targetTexture = temp;
        camera1.Render();

        // Render the second camera view
        camera2.targetTexture = dest;
        camera2.Render();

        // Combine the two views into a single image
        Graphics.Blit(temp, dest);

        RenderTexture.ReleaseTemporary(temp);
    }
}
