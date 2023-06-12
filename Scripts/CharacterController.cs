using Godot;
using System;

class Directional
{
	public float SINE_45 = Math.Sin(45);
	public static Vector2 NORTH = (0.0, 1.0);
	public static Vector2 NORTH_EAST = (SINE_45, SINE_45);
	public static Vector2 EAST = (1.0, 0.0);
	public static Vector2 SOUTH_EAST = (SINE_45, -SINE_45);
	public static Vector2 SOUTH = (0.0, -1.0);
	public static Vector2 SOUTH_WEST = (-SINE_45, -SINE_45);
	public static Vector2 WEST = (-1.0, 0.0);
	public static Vector2 NORTH_WEST = (-SINE_45, SINE_45);
}

// For future notice - underscore indicates godot-owned function or variable
public partial class CharacterController : CharacterBody2D
{
	private const float DIAGONAL = Math.Sin(45);

	[Export]
	private int Speed { get; set; } = 100;
	private Vector2 velocity = new Vector2(0, 0);
	public Sprite2D sprite;

	public void InputHandler()
	{
		Vector2 input_direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		velocity = input_direction * Speed;
		SpriteHandler(input_direction);
	}

	public void SpriteHandler(Vector2 input)
	{
		// Clockwise format starting from 12 o'clock
		switch (input)
		{
			case Directional.NORTH:
				sprite.SetFrame(0);
				break;
			case Directional.NORTH_EAST:
				sprite.SetFrame(1);
				break;
			case Directional.EAST:
				sprite.SetFrame(2);
				break;
			case Directional.SOUTH_EAST:
				sprite.SetFrame(3);
				break;
			case Directional.SOUTH:
				sprite.SetFrame(4);
				break;
			case Directional.SOUTH_WEST:
				sprite.SetFrame(5);
				break;
			case Directional.WEST:
				sprite.SetFrame(6);
				break;
			case Directional.NORTH_WEST:
				sprite.SetFrame(7);
				break;
		}
	}

	// On start of script
	public override void _Ready() 
	{
		sprite = GetNode<Sprite2D>("Sprite");
	}

	// While game process is running and script is active
	public override void _PhysicsProcess(double delta)
	{
		InputHandler();
		var collision_info = MoveAndCollide(velocity * (float)delta);
		if (collision_info == null)
		{
			MoveAndSlide();
		}

	}
}

/*TODO:
 - fix wall sticking problem
possible solution: check individual axis of velocity and only apply change to ones not affected*/