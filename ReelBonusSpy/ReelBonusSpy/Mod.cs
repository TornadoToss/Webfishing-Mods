using GDWeave;

namespace ReelBonusSpy;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
		modInterface.Logger.Information("[Erythtini.ReelBonusSpy] Applying patches for RBS...");
		modInterface.RegisterScriptMod(new SlowReelNotifPatch());
		modInterface.Logger.Information("[Erythtini.ReelBonusSpy] Patches applied successfully! Good luck out there, fisher :D");
	}

    public void Dispose() {
        // Cleanup anything you do here
    }
}
