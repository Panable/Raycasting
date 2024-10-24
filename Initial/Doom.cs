using SplashKitSDK;
namespace Initial;

public class Doom
{
    /* Window Dimensions */
    public const float SCALE = 1.0f;
    public const int WIDTH = (int)(800 * SCALE);
    public const int HEIGHT = (int)(800 * SCALE);

    private Player _player;
    private Map _map;
    private Raycaster _raycaster;
    private Minimap _minimap;

    public Doom()
    {
        _player = new Player();
        _map = new Map();
        _raycaster = new Raycaster(_player, _map);
        _minimap = new Minimap(_player, _map, _raycaster, 0.0f, 0.0f, 0.2f);
    }

    public void Run()
    {
        SplashKit.HideMouse();
        Color backgroundColor = SplashKit.StringToColor("#181818");
        Window window = new Window("Raycasting", WIDTH, HEIGHT);
        double _lastTime = SplashKit.CurrentTicks();
        while (!window.CloseRequested)
        {
            // Calculate deltaTime
            double currentTime = SplashKit.CurrentTicks();
            double deltaTime = (currentTime - _lastTime) / 1000.0;
            _lastTime = currentTime;
            SplashKit.ProcessEvents();
            SplashKit.ClearScreen(backgroundColor);
            Update((float)deltaTime);
            Render();
            SplashKit.RefreshScreen(144);
        }
    }

    public void Update(float deltaTime)
    {
        _player.Update(deltaTime);
    }

    public void Render()
    {
        _minimap.Render();
    }

}
