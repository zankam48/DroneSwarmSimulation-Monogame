using DroneSwarmSimulation.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DroneSwarmSimulation.Core;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private GraphicsDevice _device;
    private Effect _effect;
    private Camera _camera;
    private Texture2D _sceneryTexture;
    private Vector3 _dronePosition = new Vector3(8, 1, -3);
    private Quaternion _droneRotation = Quaternion.Identity;
    private Drone _drone;
    private FloorPlan _floorPlan;
    private Vertices _vertices;
    private Vector3 _lightDirection = new Vector3(3, -2, 5);
    private float _gameSpeed = 1.0f;

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
        _drone = new Drone(_dronePosition, _droneRotation);

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

    private void ProcessKeyboard(GameTime gameTime)
    {
        float leftRightRotation = 0;
        float upDownRotation = 0;

        float turningSpeed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
        turningSpeed *= 1.6f * _gameSpeed;

        KeyboardState keys = Keyboard.GetState();

        if (keys.IsKeyDown(Keys.Right))
        {
            leftRightRotation += turningSpeed;
        }
        if (keys.IsKeyDown(Keys.Left))
        {
            leftRightRotation -= turningSpeed;
        }
        if (keys.IsKeyDown(Keys.Down))
        {
            upDownRotation += turningSpeed;
        }
        if (keys.IsKeyDown(Keys.Up))
        {
            upDownRotation -= turningSpeed;
        }

        Quaternion additionalRotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, -1), leftRightRotation) * Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), upDownRotation);
        _droneRotation *= additionalRotation;
    }

    private void MoveForward(ref Vector3 position, Quaternion rotationQuat, float speed)
    {
        Vector3 addVector = Vector3.Transform(new Vector3(0, 0, -1), rotationQuat);
        position += addVector * speed;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _camera.UpdateCamera(_dronePosition, _droneRotation);
        ProcessKeyboard(gameTime);

        float moveSpeed = gameTime.ElapsedGameTime.Milliseconds / 500.0f * _gameSpeed;
        MoveForward(ref _dronePosition, _droneRotation, moveSpeed);
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
