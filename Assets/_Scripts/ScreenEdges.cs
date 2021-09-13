public struct ScreenEdges
{
    private float _left;
    private float _right;
    private float _top;
    private float _bottom;

    public ScreenEdges(float a_left,
                       float a_right,
                       float a_top,
                       float a_bottom)
    {
        _left = a_left;
        _right = a_right;
        _top = a_top;
        _bottom = a_bottom;
    }

    public float Left { get => _left;}
    public float Right { get => _right;}
    public float Top { get => _top;}
    public float Bottom { get => _bottom;}
}
