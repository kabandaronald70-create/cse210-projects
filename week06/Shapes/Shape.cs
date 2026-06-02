// Base class
public class Shape
{
    private string color;

    // Constructor
    public Shape(string color)
    {
        this.color = color;
    }

    // Getter and Setter for color
    public string GetColor()
    {
        return color;
    }

    public void SetColor(string color)
    {
        this.color = color;
    }

    // Virtual method to be overridden by derived classes
    public virtual double GetArea()
    {
        return 0;
    }
}