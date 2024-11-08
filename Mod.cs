using Autofisher.Mods;
using GDWeave;

namespace Autofisher;

public class Mod : IMod
{
    public Mod(IModInterface mod)
    {
        mod.RegisterScriptMod(new AutoCatcherMod());
        mod.RegisterScriptMod(new AutoScratcherMod());
        mod.RegisterScriptMod(new AnimationRemoverMod());
        mod.RegisterScriptMod(new CasterMod());
        mod.RegisterScriptMod(new BaitRemoverMod());
    }

    public void Dispose()
    {
    }
}
