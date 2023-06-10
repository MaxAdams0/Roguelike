using Godot;

public partial class CharacterController : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 100;
	private Vector2 velocity = new Vector2(0, 0);

	public void InputHandler()
	{
		Vector2 input_direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		velocity = input_direction * Speed;
		GD.Print(input_direction);

		if (input_direction.X < 0) // Left
		{
			ApplyScale(new Vector2(-1, 1));
		}
		else if (input_direction.X > 0) // Right
		{
			ApplyScale(new Vector2(1, 1));
		}
	}

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