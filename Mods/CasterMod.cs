using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace Autofisher.Mods;

public class CasterMod : IScriptMod
{
    public bool ShouldRun(string path)
    {
        return path == "res://Scenes/Entities/Player/player.gdc";
    }

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> t)
    {

        Token[] tokens = t.ToArray();

        int i = 0;

        FunctionWaiter waiter = new("_cast_fishing_rod");
        for (; i < tokens.Length; i++)
        {
            yield return tokens[i];

            if (waiter.Check(tokens[i]))
            {
                break;
            }
        }

        for (; i < tokens.Length; i++)
        {
            if (tokens[i].Type == TokenType.PrYield)
            {
                break;
            }

            yield return tokens[i];
        }

        for (; i < tokens.Length && tokens[i].Type != TokenType.ParenthesisClose; i++);
        i++;

        for (; i < tokens.Length && tokens[i].Type != TokenType.OpAssign; i++) yield return tokens[i];
        yield return tokens[i];
        yield return new ConstantToken(new RealVariant(5f));

        for (; i < tokens.Length && tokens[i].Type != TokenType.ParenthesisClose; i++);
        i++;
        for (; i < tokens.Length; i++) yield return tokens[i];
    }
}
