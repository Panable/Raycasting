using SplashKitSDK;
namespace Initial;

public class Doom
{
    public void Run()
    {
        float scale = 2.5f;
        int width = (int)(800 * scale);
        int height = (int)(600 * scale);
        Window window = new Window("Raycasting", width, height);
        double _lastTime = SplashKit.CurrentTicks();
        while (!window.CloseRequested)
        {
            // Calculate deltaTime
            double currentTime = SplashKit.CurrentTicks();
            double deltaTime = (currentTime - _lastTime) / 1000.0;
            _lastTime = currentTime;

            SplashKit.ProcessEvents();
            SplashKit.ClearScreen(Color.CornflowerBlue);
            Update(deltaTime);
            Render();
            SplashKit.RefreshScreen(144);
        }
    }

    public void Update(double deltaTime)
    {
    }

    public void Render()
    {
    }
}
