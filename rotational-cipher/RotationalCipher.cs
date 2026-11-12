public static class RotationalCipher
{
    public static string Rotate(string text, int shiftKey)
    {
        int shift = ((shiftKey % 26) + 26) % 26;
        var result = new char[text.Length];
        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            if (c >= 'a' && c <= 'z')
            {
                result[i] = (char)('a' + (c - 'a' + shift) % 26);
            }
            else if (c >= 'A' && c <= 'Z')
            {
                result[i] = (char)('A' + (c - 'A' + shift) % 26);
            }
            else
            {
                result[i] = c;
            }
        }
        return new string(result);
    }
}
