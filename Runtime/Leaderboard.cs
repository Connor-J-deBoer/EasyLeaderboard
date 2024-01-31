using System;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Leaderboards;

namespace Connor.LeaderboardHelpers.Runtime
{
    public static class Leaderboard
    {
        private static Authorize _authorize = new Authorize();
        /// <summary>
        /// Searches a leaderboard for a given amount of entries
        /// </summary>
        /// <param name="leaderBoardId">the leaderboard to get the scores from</param>
        /// <param name="limit">the amount of scores you want to get</param>
        /// <param name="nextToPlayer">should these scores be relative to the player or relative to the top?</param>
        /// <returns>An array of LeaderboardEntry</returns>
        public static async Task<Unity.Services.Leaderboards.Models.LeaderboardEntry[]> GetScores(string leaderBoardId, int limit = 10, bool nextToPlayer = false)
        {
            // this guy justs ensures we are signed in
            await UnityServices.InitializeAsync();
            _authorize.AuthorizeAnonymousUserAsync();

            Unity.Services.Leaderboards.Models.LeaderboardEntry[] scoresResponse;
            switch (nextToPlayer)
            {
                case true:
                    var rangeResponse = await LeaderboardsService.Instance.GetPlayerRangeAsync(leaderBoardId, new GetPlayerRangeOptions { RangeLimit = limit });
                    scoresResponse = rangeResponse.Results.ToArray();
                    break;

                case false:
                    var globalResponse = await LeaderboardsService.Instance.GetScoresAsync(leaderBoardId, new GetScoresOptions { Offset = 0, Limit = limit });
                    scoresResponse = globalResponse.Results.ToArray();
                    break;
            }

            return scoresResponse;
        }

        /// <summary>
        /// Posts a score to a leaderboard
        /// </summary>
        /// <param name="leaderboardId">the id of the leaderboard you would like to post to</param>
        /// <param name="score">the score you would like to post</param>
        /// <returns>A LeaderboardEntry of the score you just posted</returns>
        public static async Task<Unity.Services.Leaderboards.Models.LeaderboardEntry> PostScores(string leaderboardId, int score)
        {
            // this guy justs ensures we are signed in
            await UnityServices.InitializeAsync();
            _authorize.AuthorizeAnonymousUserAsync();

            Unity.Services.Leaderboards.Models.LeaderboardEntry output = null;
            try
            {
                output = await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            Debug.Log($"Posted {score} to {leaderboardId}");
            return output;
        }
    }
}
