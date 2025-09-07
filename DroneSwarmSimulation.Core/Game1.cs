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
    private Texture2D _sceneryTexture;
    private Drone _drone = new Drone();
    private FloorPlan _floorPlan;
    private Vertices _vertices;
    private Vector3 _lightDirection = new Vector3(3, -2, 5);

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

        _floorPlan = new FloorPlan();
        _lightDirection.Normalize();

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
        _effect = Content.Load<Effect>("effects"); 
        _sceneryTexture = Content.Load<Texture2D>("texturemap");

        _camera = new Camera(_device);
        _vertices = new Vertices(_device, _effect);
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

        _vertices.DrawCity(_camera.ViewMatrix, _camera.ProjectionMatrix, Matrix.Identity, _sceneryTexture, _lightDirection);
        _drone.DrawModel(_camera.ViewMatrix, _camera.ProjectionMatrix, _lightDirection);
        base.Draw(gameTime);
    }
}
