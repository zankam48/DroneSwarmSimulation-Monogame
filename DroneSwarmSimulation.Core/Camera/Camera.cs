using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera
{
    public Matrix ViewMatrix { get; set; }
    public Matrix ProjectionMatrix { get; set; }

    public Camera(GraphicsDevice device)
    {
        SetUpCamera(device);
    }

    private void SetUpCamera(GraphicsDevice device)
    {
        ViewMatrix = Matrix.CreateLookAt(new Vector3(20, 13, -5), new Vector3(8, 0, -7), new Vector3(0, 1, 0));
        ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, device.Viewport.AspectRatio, 0.2f, 500.0f);
    }
}