using System;
using JetBrains.Annotations;

namespace ChallongeApi.Enums
{
    [PublicAPI]
    public enum GetMatchState
    {
        All,
        Pending,
        Open,
        Complete
    }

    [PublicAPI]
    public static class GetMatchStateMethods
    {
        [PublicAPI]
        public static string AsString(this GetMatchState state)
        {
            return state switch
            {
                GetMatchState.All => "all",
                GetMatchState.Pending => "pending",
                GetMatchState.Open => "open",
                GetMatchState.Complete => "complete",
                _ => throw new ArgumentOutOfRangeException(nameof(state))
            };
        }
    }
}
