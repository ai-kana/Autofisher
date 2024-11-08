using GDWeave.Godot;
using GDWeave.Modding;

namespace Autofisher.Mods;

public class AnimationRemoverMod : IScriptMod
{
    public bool ShouldRun(string path)
    {
        return path == "res://Scenes/Entities/Player/player.gdc";
    }

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        FunctionWaiter waiter = new("_obtain_item");
        foreach (Token token in tokens)
        {
            if (waiter.Check(token))
            {
                yield return token;
                yield return new IdentifierToken("_cast_fishing_rod");
                yield return new(TokenType.ParenthesisOpen);
                yield return new(TokenType.ParenthesisClose);
                yield return token;

                yield return new(TokenType.CfReturn);
                yield return token;
            }
            else
            {
                yield return token;
            }
        }
    }
}
