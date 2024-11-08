using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace Autofisher.Mods;

public class AutoScratcherMod : IScriptMod
{
    public bool ShouldRun(string path)
    {
        return path == "res://Scenes/Minigames/ScratchTicket/scratch_ticket.gdc";
    }

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        FunctionWaiter waiter = new("_slot_scratched");
        Token[] t = tokens.ToArray();
        int count = t.Count();

        int i = 0;
        for (; i < 354; i++) yield return t[i];

        yield return new(TokenType.Newline, 1);
        yield return new IdentifierToken("_slot_scratched");
        yield return new(TokenType.ParenthesisOpen);
        yield return new(TokenType.ParenthesisClose);

        int start = 658;
        int end = 673;

        for (; i < count; i++)
        {
            if (i == start)
            {
                yield return new(TokenType.CfIf);
                yield return new ConstantToken(new BoolVariant(true));
                i = end;
            }

            yield return t[i];
        }
    }
}
