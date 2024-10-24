using Math;
using SplashKitSDK;

namespace Initial;

public class Player
{
    private Vector2f _position;
    private Vector2f _direction;
    public Vector2f Position { get => _position; }
    public Vector2f Direction { get => _direction; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float RotationSpeed { get => _rotationSpeed; set => _rotationSpeed = value; }

    private float _moveSpeed = 1.0f;
    private float _rotationSpeed = 1.0f;

    public Player(Vector2f position, Vector2f direction)
    {
        _position = position;
        _direction = direction;
    }

    public Player() : this(new Vector2f(6.5f, 3.5f), Vector2f.Down) {}

    public void Update(float deltaTime)
    {
        if (SplashKit.KeyDown(KeyCode.WKey)) _position += Direction * MoveSpeed * deltaTime;
        if (SplashKit.KeyDown(KeyCode.SKey)) _position -= Direction * MoveSpeed * deltaTime;
        if (SplashKit.KeyDown(KeyCode.AKey)) Direction.Rotate(-RotationSpeed * deltaTime);
        if (SplashKit.KeyDown(KeyCode.DKey)) Direction.Rotate(RotationSpeed * deltaTime);
    }
}
