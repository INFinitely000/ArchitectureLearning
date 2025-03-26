using System.Linq;
using System.Text;
using Gameplay.MainPlayer;
using Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RuntimeConsole
{
    public static class Commands
    {
        [Command("timescale", "Set a world time scale")]
        public static void SetTimeScale(float scale)
        {
            Time.timeScale = scale;
        }
        
        [Command("timescale", "Set a world time scale")]
        public static void SetTimeScale(int scale)
        {
            Time.timeScale = scale;
        }

        [Command("restart", "Reload a current scene")]
        public static void Reload()
        {
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
        }

        [Command("help", "Displays console commands and their descriptions")]
        public static void Help()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine();
            
            foreach (var command in Console.Commands)
            {
                foreach (var variant in command.Value)
                {
                    builder.Append("/");
                    builder.Append(command.Key);
                    builder.Append(" ");

                    foreach (var parameter in variant.parameters)
                        builder.Append("[" + parameter.Name +"] ");

                    builder.Append("- ");
                    builder.Append(variant.description);

                    builder.AppendLine();
                }
            }

            Debug.Log(builder);
        }

        [Command("teleport", "Teleport main player to the coordinates")]
        public static void Teleport(int x, int y, int z)
        {
            var player = Object.FindFirstObjectByType<Player>();

            player.transform.position = new Vector3(x,y,z);
        }

        [Command("setplayerscale", "Set a main player's scale")]
        public static void SetPlayerScale(float scale)
        {
            var player = Object.FindFirstObjectByType<Player>();

            player.transform.localScale = Vector3.one * scale;
        }
        
        [Command("damage", "Damage a main player")]
        public static void TakeDamage(int damage)
        {
            Services.Instance.Single<IGameFactory>().Player.Health.TakeDamage(damage);
        }
        
        [Command("heal", "Heal a main player")]
        public static void Heal()
        {
            var health = Services.Instance.Single<IGameFactory>().Player.Health;
            
            health.SetHealth( health.MaxHealth );
        }
    }
}
