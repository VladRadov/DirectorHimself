public interface IAnimation
{
    string Name { get; set; }

    float Duration { get; set; }

    Animation Clip { get; set; }
}
