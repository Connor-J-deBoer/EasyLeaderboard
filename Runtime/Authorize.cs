using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using UnityEngine;

namespace Connor.LeaderboardHelpers.Runtime
{
    public class Authorize
    {
        /// <summary>
        /// returns the player's name
        /// </summary>
        /// <returns></returns>
        public string GetCurrentPlayerName()
        {
            string name = AuthenticationService.Instance.PlayerName;
            if (string.IsNullOrEmpty(name)) return "userName";
            return name.ShortenString();
        }

        /// <summary>
        /// Sets a new name for the player
        /// </summary>
        /// <param name="newName">The name you would like the player to have</param>
        /// <returns>the name you gave the player</returns>
        public async Task<string> SetPlayerName(string newName)
        {
            return await AuthenticationService.Instance.UpdatePlayerNameAsync(newName);
        }

        /// <summary>
        /// Signs in the user anonymously
        /// </summary>
        public async void AuthorizeAnonymousUserAsync()
        {
            if (AuthenticationService.Instance.IsSignedIn) return;

            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}