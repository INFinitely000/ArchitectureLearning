using System.Linq;
using System.Text;
using Gameplay.MainPlayer;
using GameState;
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
            Object.FindFirstObjectByType<Bootstrap>().StateMachine.Entry<InitialGameState>();
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

        [Command("summon", "Create a entity at mouse position")]
        public static void Summon(string entityName)
        {
            var prefab = Services.Instance.Single<IGameFactory>().Create<Transform>(entityName);

            if (prefab)
            {
                prefab.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                prefab.transform.rotation = Quaternion.identity;
            }
        }

        [Command("bombparty", "Create a massive bomb attack")]
        public static void BombParty()
        {
            var position = (Vector2)Object.FindFirstObjectByType<Player>().transform.position;
            position.y += 20f;

            for (int index = 0; index < 30; index++)
            {
                Services.Instance.Single<IGameFactory>().CreateBomb( position + Random.insideUnitCircle * 10f, Vector3.zero, Random.Range(1,3) );
            }
        }
    }
}
