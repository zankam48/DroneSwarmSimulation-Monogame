using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DroneSwarmSimulation.Models;

public class Drone
{
    private Motor[] _motors;
    private string _assetName = "drone";
    public string AssetName
    {
        get => _assetName;
        set => _assetName = value;
    }

    public Model droneModel { get; set; }
    public Drone()
    {

    }

    public void DrawModel(Matrix viewMatrix, Matrix projectionMatrix, Vector3 lightDirection)
    {
        Matrix worldMatrix = Matrix.CreateScale(0.0005f, 0.0005f, 0.0005f) *
                                Matrix.CreateRotationY(MathHelper.Pi) *
                                Matrix.CreateTranslation(new Vector3(19, 12, -5));

        Matrix[] xwingTransforms = new Matrix[droneModel.Bones.Count];
        droneModel.CopyAbsoluteBoneTransformsTo(xwingTransforms);

        foreach (ModelMesh mesh in droneModel.Meshes)
        {
            foreach (Effect currentEffect in mesh.Effects)
            {
                currentEffect.CurrentTechnique = currentEffect.Techniques["Colored"];
                currentEffect.Parameters["xWorld"].SetValue(xwingTransforms[mesh.ParentBone.Index] * worldMatrix);
                currentEffect.Parameters["xView"].SetValue(viewMatrix);
                currentEffect.Parameters["xProjection"].SetValue(projectionMatrix);
                currentEffect.Parameters["xEnableLighting"].SetValue(true);
                currentEffect.Parameters["xLightDirection"].SetValue(lightDirection);
                currentEffect.Parameters["xAmbient"].SetValue(0.5f);
            }
            mesh.Draw();
        }
    }
}
