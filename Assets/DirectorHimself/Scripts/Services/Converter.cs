using System;

public class Converter
{
    public static int ToInt(object value) => value.ToString() == "" ? 0 : int.Parse(value.ToString());

    public static float ToFloat(object value) => value.ToString() == "" ? 0f : Convert.ToSingle(value);
}
