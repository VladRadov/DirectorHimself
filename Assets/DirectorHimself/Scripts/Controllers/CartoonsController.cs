public class CartoonsController
{
    public Cartoon CreateCartoon(int id, string name)
    {
        var cartoon = new Cartoon()
        {
            Id = id,
            Name = name
        };

        return cartoon;
    }
}
