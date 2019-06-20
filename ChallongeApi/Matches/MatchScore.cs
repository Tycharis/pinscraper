using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace ChallongeApi.Matches
{
    /// <summary>
    /// Represents a participant in a match.
    /// </summary>
    [PublicAPI]
    public struct MatchParticipant
    {
        [PublicAPI]
        public int Id { get; }

        [PublicAPI]
        public int Score { get; }

        [PublicAPI]
        public MatchParticipant(int id, int score)
        {
            Id = id;
            Score = score;
        }
    }

    /// <summary>
    /// Represents a match with a single set.
    /// </summary>
    [PublicAPI]
    public class MatchScore
    {
        public MatchParticipant ParticipantOne { get; }

        public MatchParticipant ParticipantTwo { get; }

        /// <summary>
        /// Requires that the match be fully instantiated.
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        [PublicAPI]
        public MatchScore(MatchParticipant one, MatchParticipant two)
        {
            ParticipantOne = one;
            ParticipantTwo = two;
        }

        /// <summary>
        /// Determines if the set/match is a tie.
        /// </summary>
        /// <returns>True if a tie occurred, otherwise false.</returns>
        [PublicAPI]
        public bool IsTie()
        {
            return ParticipantOne.Score == ParticipantTwo.Score;
        }

        [PublicAPI]
        public int? GetWinnerId()
        {
            if (IsTie())
            {
                return null;
            }

            return ParticipantOne.Score > ParticipantTwo.Score
                ? ParticipantOne.Id
                : ParticipantTwo.Id;
        }

        /// <summary>
        /// Gets the score of the set as a string.
        /// </summary>
        [PublicAPI]
        public override string ToString()
        {
            return $"{ParticipantOne.Score}-{ParticipantTwo.Score}";
        }
    }

    /// <summary>
    /// Similar to <see cref="MatchScore"/>, but meant to represent a match with a set of games.
    /// </summary>
    [PublicAPI]
    public class SetScore
    {
        public int ParticipantOneScore { get; }

        public int ParticipantTwoScore { get; }

        [PublicAPI]
        public SetScore(int one, int two)
        {
            ParticipantOneScore = one;
            ParticipantTwoScore = two;
        }

        [PublicAPI]
        public bool IsTie()
        {
            return ParticipantOneScore == ParticipantTwoScore;
        }

        [PublicAPI]
        public override string ToString()
        {
            return $"{ParticipantOneScore}-{ParticipantTwoScore}";
        }
    }

    [PublicAPI]
    public class SetScores
    {
        public IEnumerable<SetScore> Scores { get; }

        public int ParticipantOneId { get; }

        public int ParticipantTwoId { get; }

        [PublicAPI]
        public SetScores(IEnumerable<SetScore> scores, int oneId, int twoId)
        {
            Scores = scores;
            ParticipantOneId = oneId;
            ParticipantTwoId = twoId;
        }

        [PublicAPI]
        public int? GetWinnerId()
        {
            int participantOneSetScore = ParticipantOneWonSetsCount();

            int participantTwoSetScore = ParticipantTwoWonSetsCount();

            // Ensures that one of the scores is greater than the other

            if (participantOneSetScore > participantTwoSetScore)
            {
                return ParticipantOneId;
            }

            if (participantTwoSetScore > participantOneSetScore)
            {
                return ParticipantTwoId;
            }

            return null;
        }

        /// <summary>
        /// Gets a string representation of the match and its sets.
        /// </summary>
        /// <returns>example: "3-2,0-3,1-3"</returns>
        [PublicAPI]
        public string GetSetScoresString()
        {
            return string.Join(',', Scores);
        }

        /// <summary>
        /// Determines if a match with multiple sets is a tie.
        /// </summary>
        /// <returns>True if a tie occurred, otherwise false.</returns>
        [PublicAPI]
        public bool IsMatchWithSetsTie()
        {
            int participantOneSetScore = ParticipantOneWonSetsCount();

            int participantTwoSetScore = ParticipantTwoWonSetsCount();

            return participantOneSetScore == participantTwoSetScore;
        }

        /// <summary>
        /// Gets the count of sets that participant one won.
        /// </summary>
        [PublicAPI]
        public int ParticipantOneWonSetsCount()
        {
            return Scores.Count(x => x.ParticipantOneScore > x.ParticipantTwoScore);
        }

        /// <summary>
        /// Gets the count of sets that participant two won.
        /// </summary>
        [PublicAPI]
        public int ParticipantTwoWonSetsCount()
        {
            return Scores.Count(x => x.ParticipantTwoScore > x.ParticipantOneScore);
        }
    }
}
