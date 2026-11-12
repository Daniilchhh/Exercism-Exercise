using System;
public class CircularBuffer<T>
{
    private readonly T[] _buffer;
    private int _head;
    private int _count;
    public CircularBuffer(int capacity)
    {
        _buffer = new T[capacity];
    }
    public T Read()
    {
        if (_count == 0)
            throw new InvalidOperationException("Buffer is empty");
        T value = _buffer[_head];
        _buffer[_head] = default;
        _head = (_head + 1) % _buffer.Length;
        _count--;
        return value;
    }
    public void Write(T value)
    {
        if (_count == _buffer.Length)
            throw new InvalidOperationException("Buffer is full");
        _buffer[(_head + _count) % _buffer.Length] = value;
        _count++;
    }
    public void Overwrite(T value)
    {
        if (_count == _buffer.Length)
        {
            _buffer[_head] = value;
            _head = (_head + 1) % _buffer.Length;
        }
        else
        {
            Write(value);
        }
    }
    public void Clear()
    {
        Array.Clear(_buffer, 0, _buffer.Length);
        _head = 0;
        _count = 0;
    }
}
