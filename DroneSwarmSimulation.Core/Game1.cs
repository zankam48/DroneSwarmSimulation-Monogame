using DroneSwarmSimulation.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DroneSwarmSimulation.Core;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private GraphicsDevice _device;
    private SpriteBatch _spriteBatch;
    private Effect _effect;
    private Camera _camera;
    private Drone _drone = new Drone();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 600;
        _graphics.IsFullScreen = false;
        _graphics.ApplyChanges();
        Window.Title = "Drone Swarm Simulation";

        base.Initialize();
    }

    private Model LoadModel(string assetName)
    {
        Model newModel = Content.Load<Model>(assetName);
        foreach (ModelMesh mesh in newModel.Meshes)
        {
            foreach (ModelMeshPart meshPart in mesh.MeshParts)
            {
                meshPart.Effect = _effect.Clone();
            }
        }
        return newModel;
    }

    protected override void LoadContent()
    {
        // TODO: use this.Content to load your game content here

        _device = _graphics.GraphicsDevice;
        _effect = Content.Load<Effect>("effects"); // todo change this

        _camera = new Camera(_device);
        _drone.droneModel = LoadModel(_drone.AssetName);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _drone.DrawModel(_camera.ViewMatrix, _camera.ProjectionMatrix);
        base.Draw(gameTime);
    }
}
