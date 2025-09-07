using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera
{
    public Matrix ViewMatrix { get; set; }
    public Matrix ProjectionMatrix { get; set; }
    public Vector3 Position { get; set; }
    private GraphicsDevice _device;

    public Camera(GraphicsDevice device)
    {
        _device = device;
        SetUpCamera();
    }

    private void SetUpCamera()
    {
        ViewMatrix = Matrix.CreateLookAt(new Vector3(20, 13, -5), new Vector3(8, 0, -7), new Vector3(0, 1, 0));
        ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, _device.Viewport.AspectRatio, 0.2f, 500.0f);
    }

    public void UpdateCamera(Vector3 dronePosition, Quaternion droneRotation)
    {
        Position = new Vector3(0, 0.1f, 0.6f);
        Position = Vector3.Transform(Position, Matrix.CreateFromQuaternion(droneRotation));
        Position += dronePosition;
        Vector3 cameraUpDirection = new Vector3(0, 1, 0);
        cameraUpDirection = Vector3.Transform(cameraUpDirection, Matrix.CreateFromQuaternion(droneRotation));

        ViewMatrix = Matrix.CreateLookAt(Position, dronePosition, cameraUpDirection);
        ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, _device.Viewport.AspectRatio, 0.2f, 500.0f);
    }
}