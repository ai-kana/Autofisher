using GDWeave.Godot;
using GDWeave.Modding;

namespace Autofisher.Mods;

public class BaitRemoverMod : IScriptMod
{
    public bool ShouldRun(string path)
    {
        return path == "res://Scenes/Singletons/playerdata.gdc";
    }

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        FunctionWaiter waiter = new("_use_bait");
        foreach (Token token in tokens)
        {
            if (waiter.Check(token))
            {
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
