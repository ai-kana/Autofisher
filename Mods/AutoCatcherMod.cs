using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace Autofisher.Mods;

public class AutoCatcherMod : IScriptMod
{
    public bool ShouldRun(string path)
    {
        return path == "res://Scenes/Entities/Player/player.gdc";
    }

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        Token[] t = tokens.ToArray();
        // hud . _open_minigame ( fishing3
        int count = t.Count();
        int index = 0;
        for (int i = 0; i < count; i++)
        {
            if (t[i] is IdentifierToken id && id.Name == "hud")
            if (t[i + 1].Type == TokenType.Period)
            if (t[i + 2] is IdentifierToken id2 && id2.Name == "_open_minigame")
            if (t[i + 3].Type == TokenType.ParenthesisOpen)
            if (t[i + 4] is ConstantToken token && token.Value is StringVariant v && v.Value == "fishing3")
            {
                index = i;
                break;
            }
        }

        if (index == 0) throw new();

        for (int i = 0; i < count; i++)
        {
            if (i == index)
            {
                for (; t[i].Type != TokenType.ParenthesisClose; i++);
                i++;
                for (; t[i].Type != TokenType.ParenthesisClose; i++);
                i++;

                foreach (Token token in ScriptTokenizer.Tokenize(
                """
                var success = true
                """, 1)) yield return token;
            }

            yield return t[i];
        }
    }
}
