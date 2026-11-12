using System;
using System.Collections.Generic;
using System.Linq;
public enum Color { Red, Green, Ivory, Yellow, Blue }
public enum Nationality { Englishman, Spaniard, Ukrainian, Japanese, Norwegian }
public enum Pet { Dog, Snails, Fox, Horse, Zebra }
public enum Drink { Coffee, Tea, Milk, OrangeJuice, Water }
public enum Hobby { Dancing, Painting, Reading, Football, Chess }
public static class ZebraPuzzle
{
    private static Nationality _waterDrinker;
    private static Nationality _zebraOwner;
    static ZebraPuzzle()
    {
        Solve();
    }
    public static Nationality DrinksWater() => _waterDrinker;
    public static Nationality OwnsZebra() => _zebraOwner;
    private static bool Next(int a, int b) => Math.Abs(a - b) == 1;
    private static void Solve()
    {
        var perms = Permutations(5).ToList();
        foreach (var color in perms)
        {
            if (color[(int)Color.Green] != color[(int)Color.Ivory] + 1) continue;
            foreach (var nat in perms)
            {
                if (nat[(int)Nationality.Englishman] != color[(int)Color.Red]) continue;
                if (nat[(int)Nationality.Norwegian] != 0) continue;
                if (!Next(nat[(int)Nationality.Norwegian], color[(int)Color.Blue])) continue;
                foreach (var drink in perms)
                {
                    if (drink[(int)Drink.Coffee] != color[(int)Color.Green]) continue;
                    if (drink[(int)Drink.Tea] != nat[(int)Nationality.Ukrainian]) continue;
                    if (drink[(int)Drink.Milk] != 2) continue;
                    foreach (var hobby in perms)
                    {
                        if (hobby[(int)Hobby.Painting] != color[(int)Color.Yellow]) continue;
                        if (hobby[(int)Hobby.Football] != drink[(int)Drink.OrangeJuice]) continue;
                        if (hobby[(int)Hobby.Chess] != nat[(int)Nationality.Japanese]) continue;
                        foreach (var pet in perms)
                        {
                            if (pet[(int)Pet.Dog] != nat[(int)Nationality.Spaniard]) continue;
                            if (pet[(int)Pet.Snails] != hobby[(int)Hobby.Dancing]) continue;
                            if (!Next(hobby[(int)Hobby.Reading], pet[(int)Pet.Fox])) continue;
                            if (!Next(hobby[(int)Hobby.Painting], pet[(int)Pet.Horse])) continue;
                            _waterDrinker = (Nationality)Array.IndexOf(nat, drink[(int)Drink.Water]);
                            _zebraOwner = (Nationality)Array.IndexOf(nat, pet[(int)Pet.Zebra]);
                            return;
                        }
                    }
                }
            }
        }
        throw new InvalidOperationException("No solution found");
    }
    private static IEnumerable<int[]> Permutations(int n)
    {
        return Permute(new int[n], new bool[n], 0);
    }
    private static IEnumerable<int[]> Permute(int[] current, bool[] used, int index)
    {
        if (index == current.Length)
        {
            yield return (int[])current.Clone();
            yield break;
        }
        for (int i = 0; i < current.Length; i++)
        {
            if (used[i]) continue;
            used[i] = true;
            current[index] = i;
            foreach (var p in Permute(current, used, index + 1))
                yield return p;
            used[i] = false;
        }
    }
}
