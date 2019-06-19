using System;

namespace ChallongeApi.Enums
{
    public enum GetMatchState
    {
        All,
        Pending,
        Open,
        Complete
    }

    public static class GetMatchStateMethods
    {
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
