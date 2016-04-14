using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class GameManager : MonoBehaviour
    {
        public int m_NumRoundsToWin = 5;            // The number of rounds a single player has to win to win the game.
        public float m_StartDelay = 3f;             // The delay between the start of RoundStarting and RoundPlaying phases.
        public float m_EndDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases.
        public CameraControl m_CameraControl;       // Reference to the CameraControl script for control during different phases.
        public Text m_MessageText;                  // Reference to the overlay Text to display winning text, etc.
        public GameObject m_PlayerPrefab;             // Reference to the prefab the players will control.
        public PlayerManager[] m_Players;               // A collection of managers for enabling and disabling different aspects of the tanks.

        
        private int m_RoundNumber;                  // Which round the game is currently on.
        private WaitForSeconds m_StartWait;         // Used to have a delay whilst the round starts.
        private WaitForSeconds m_EndWait;           // Used to have a delay whilst the round or game ends.
        private PlayerManager m_RoundWinner;          // Reference to the winner of the current round.  Used to make an announcement of who won.
        private PlayerManager m_GameWinner;           // Reference to the winner of the game.  Used to make an announcement of who won.


        private void Start()
        {
            // Create the delays so they only have to be made once.
            m_StartWait = new WaitForSeconds (m_StartDelay);
            m_EndWait = new WaitForSeconds (m_EndDelay);

            SpawnAllPlayers();
            SetCameraTargets();

            // Once the players have been created and the camera is using them as targets, start the game.
            StartCoroutine (GameLoop ());
        }


        private void SpawnAllPlayers()
        {
            // For all the players...
            for (int i = 0; i < m_Players.Length; i++)
            {
                // ... create them, set their player number and references needed for control.
                m_Players[i].m_Instance =
                    Instantiate(m_PlayerPrefab, m_Players[i].m_SpawnPoint.position, m_Players[i].m_SpawnPoint.rotation) as GameObject;
                m_Players[i].m_PlayerNumber = i + 1;
                m_Players[i].Setup();
            }
        }


        private void SetCameraTargets()
        {
            // Create a collection of transforms the same size as the number of Players.
            Transform[] targets = new Transform[m_Players.Length];

            // For each of these transforms...
            for (int i = 0; i < targets.Length; i++)
            {
                // ... set it to the appropriate player transform.
                targets[i] = m_Players[i].m_Instance.transform;
            }

            // These are the targets the camera should follow.
            m_CameraControl.m_Targets = targets;
        }


        // This is called from start and will run each phase of the game one after another.
        private IEnumerator GameLoop ()
        {
            // Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
            yield return StartCoroutine (RoundStarting ());

            // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
            yield return StartCoroutine (RoundPlaying());

            // Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
            yield return StartCoroutine (RoundEnding());

            // This code is not run until 'RoundEnding' has finished.  At which point, check if a game winner has been found.
            if (m_GameWinner != null)
            {
                // If there is a game winner, restart the level.
                Application.LoadLevel (Application.loadedLevel);
            }
            else
            {
                // If there isn't a winner yet, restart this coroutine so the loop continues.
                // Note that this coroutine doesn't yield.  This means that the current version of the GameLoop will end.
                StartCoroutine (GameLoop ());
            }
        }


        private IEnumerator RoundStarting ()
        {
            // As soon as the round starts reset the players and make sure they can't move.
            ResetAllPlayers ();
            DisablePlayerControl ();

            // Snap the camera's zoom and position to something appropriate for the reset players.
            m_CameraControl.SetStartPositionAndSize ();

            // Increment the round number and display text showing the players what round it is.
            m_RoundNumber++;
            m_MessageText.text = "LEVEL 1";

            // Wait for the specified length of time until yielding control back to the game loop.
            yield return m_StartWait;
        }


        private IEnumerator RoundPlaying ()
        {
            // As soon as the round begins playing let the players control the players.
            EnablePlayerControl ();

            // Clear the text from the screen.
            m_MessageText.text = string.Empty;

            // While there is not one player left...
            while (!OnePlayerLeft())
            {
                // ... return on the next frame.
                yield return null;
            }
        }


        private IEnumerator RoundEnding ()
        {
            // Stop players from moving.
            DisablePlayerControl ();

            // Clear the winner from the previous round.
            m_RoundWinner = null;

            // See if there is a winner now the round is over.
            m_RoundWinner = GetRoundWinner ();

            // If there is a winner, increment their score.
            if (m_RoundWinner != null)
                m_RoundWinner.m_Wins++;

            // Now the winner's score has been incremented, see if someone has one the game.
            m_GameWinner = GetGameWinner ();

            // Get a message based on the scores and whether or not there is a game winner and display it.
            string message = EndMessage ();
            m_MessageText.text = message;

            // Wait for the specified length of time until yielding control back to the game loop.
            yield return m_EndWait;
        }


        // This is used to check if there is one or fewer players remaining and thus the round should end.
        private bool OnePlayerLeft()
        {
            // Start the count of players left at zero.
            int numPlayersLeft = 0;

            // Go through all the players...
            for (int i = 0; i < m_Players.Length; i++)
            {
                // ... and if they are active, increment the counter.
                if (m_Players[i].m_Instance.activeSelf)
                    numPlayersLeft++;
            }

            // If there are one or fewer players remaining return true, otherwise return false.
            return numPlayersLeft <= 1;
        }


        // This function is to find out if there is a winner of the round.
        // This function is called with the assumption that 1 or fewer players are currently active.
        private PlayerManager GetRoundWinner()
        {
            // Go through all the players...
            for (int i = 0; i < m_Players.Length; i++)
            {
                // ... and if one of them is active, it is the winner so return it.
                if (m_Players[i].m_Instance.activeSelf)
                    return m_Players[i];
            }

            // If none of the players are active it is a draw so return null.
            return null;
        }


        // This function is to find out if there is a winner of the game.
        private PlayerManager GetGameWinner()
        {
            // Go through all the players...
            for (int i = 0; i < m_Players.Length; i++)
            {
                // ... and if one of them has enough rounds to win the game, return it.
                if (m_Players[i].m_Wins == m_NumRoundsToWin)
                    return m_Players[i];
            }

            // If no players have enough rounds to win, return null.
            return null;
        }


        // Returns a string message to display at the end of each round.
        private string EndMessage()
        {
            // By default when a round ends there are no winners so the default end message is a draw.
            string message = "DRAW!";

            // If there is a winner then change the message to reflect that.
            if (m_RoundWinner != null)
                message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

            // Add some line breaks after the initial message.
            message += "\n\n\n\n";

            // Go through all the players and add each of their scores to the message.
            for (int i = 0; i < m_Players.Length; i++)
            {
                message += m_Players[i].m_ColoredPlayerText + ": " + m_Players[i].m_Wins + " WINS\n";
            }

            // If there is a game winner, change the entire message to reflect that.
            if (m_GameWinner != null)
                message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

            return message;
        }


        // This function is used to turn all the players back on and reset their positions and properties.
        private void ResetAllPlayers()
        {
            for (int i = 0; i < m_Players.Length; i++)
            {
                m_Players[i].Reset();
            }
        }


        private void EnablePlayerControl()
        {
            for (int i = 0; i < m_Players.Length; i++)
            {
                m_Players[i].EnableControl();
            }
        }


        private void DisablePlayerControl()
        {
            for (int i = 0; i < m_Players.Length; i++)
            {
                m_Players[i].DisableControl();
            }
        }
    }
}