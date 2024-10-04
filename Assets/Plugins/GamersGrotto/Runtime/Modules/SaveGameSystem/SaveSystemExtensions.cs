using GamersGrotto.Runtime.Modules.SaveGameSystem.Profiles;

namespace GamersGrotto.Runtime.Modules.SaveGameSystem
{
    public static class SaveSystemExtensions
    {
        public static void Save(this Profile profile)
        {
            ProfileSelector.UpdateProfile(profile);
        }
    }
}