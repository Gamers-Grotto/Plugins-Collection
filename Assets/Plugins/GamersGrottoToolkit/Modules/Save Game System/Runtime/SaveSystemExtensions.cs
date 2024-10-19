using GamersGrotto.Save_Game_System.Profiles;

namespace GamersGrotto.Save_Game_System
{
    public static class SaveSystemExtensions
    {
        public static void Save(this Profile profile)
        {
            ProfileSelector.UpdateProfile(profile);
        }
    }
}