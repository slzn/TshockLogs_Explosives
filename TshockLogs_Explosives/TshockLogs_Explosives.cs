using System;
using System.Collections.Generic;
using TShockAPI;
using TShockAPI.Hooks;
using Terraria;
using Terraria.ID;
using TerrariaApi.Server;
using System.IO;

namespace TshockLogs
{
    [ApiVersion(2, 1)]
    public class Explosives: TerrariaPlugin
    {
        private string ConfigFilePath = Path.Combine(TShock.SavePath, "TshockLogs_Explosives_Config.json");
        private Explosives_Config ExplosivesConfig;
        private Dictionary<short, string> InterestingExplosives;

        /// <summary>
      	/// Gets the author(s) of this plugin
        /// </summary>
      	public override string Author
        {
            get { return "slzn0124@gmail.com"; }
        }

        /// <summary>
        /// Gets the description of this plugin.
        /// A short, one lined description that tells people what your plugin does.
        /// </summary>
        public override string Description
        {
            get { return "Add explosives usage logs"; }
        }

        /// <summary>
        /// Gets the name of this plugin.
        /// </summary>
        public override string Name
        {
            get { return "TshockLogs_Explosives"; }
        }

        /// <summary>
        /// Gets the version of this plugin.
        /// </summary>
        public override Version Version
        {
            get { return new Version(1, 1, 0, 0); }
        }

        /// <summary>
        /// Initializes a new instance of the TestPlugin class.
        /// This is where you set the plugin's order and perfrom other constructor logic
        /// </summary>
        public Explosives(Main game) : base(game)
        {
            ExplosivesConfig = Explosives_Config.Read(ConfigFilePath);
            SetupExplosivesDictionary();
        }

        /// <summary>
        /// Handles plugin initialization. 
        /// Fired when the server is started and the plugin is being loaded.
        /// You may register hooks, perform loading procedures etc here.
        /// </summary>
        public override void Initialize()
        {
            GetDataHandlers.NewProjectile += OnNewProjectile;
            GeneralHooks.ReloadEvent += OnReload;
        }



        private void OnNewProjectile(object sender, TShockAPI.GetDataHandlers.NewProjectileEventArgs args)
        {
            string explosiveName = "";
            if (InterestingExplosives.TryGetValue(args.Type,out explosiveName))
            {                
                TSPlayer player = TShock.Players[args.Owner];
                if (ExplosivesConfig.ShowOnConsole)
                {
                    Console.WriteLine("{0} throws {1} at ({2},{3})", player.Name, explosiveName, args.Position.X, args.Position.Y);
                }
                
                TShock.Log.Info("{0} throws {1} at ({2},{3})", player.Name, explosiveName, args.Position.X, args.Position.Y);
            }
            
        }

        private void OnReload(ReloadEventArgs reloadEventArgs)
        {
            Console.WriteLine("Reload TshockLogs_Explosives_Config.json");
            ExplosivesConfig = Explosives_Config.Read(ConfigFilePath);
            SetupExplosivesDictionary();
        }

        private void SetupExplosivesDictionary()
        {
            InterestingExplosives = new Dictionary<short, string>();
            ProjectileID projectileId = new ProjectileID();
            Type type = projectileId.GetType();
            foreach (string explosive in ExplosivesConfig.Explosives)
            {
                short id = (short) type.GetField(explosive).GetValue(projectileId);
                InterestingExplosives.Add(id, explosive);
            }
        }

        /// <summary>
        /// Handles plugin disposal logic.
        /// *Supposed* to fire when the server shuts down.
        /// You should deregister hooks and free all resources here.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                GetDataHandlers.NewProjectile -= OnNewProjectile;
                GeneralHooks.ReloadEvent -= OnReload;
            }
            base.Dispose(disposing);
        }
    }
}
