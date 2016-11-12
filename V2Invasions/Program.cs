using System;
using System.Windows.Forms;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace V2Invasions
{
    public class Program : Form
    {
        public static void Main()
        {
            Application.Run(new Program());
        }

        private NotifyIcon icon;
        private ContextMenu menu;
        private System.Threading.Timer timer;
        private WebClient client = new WebClient();
        private Boolean lastInvasion;

        public Program()
        {
            menu = new ContextMenu();
            menu.MenuItems.Add("Exit", OnExit);

            icon = new NotifyIcon();
            icon.Text = "V2Invasions";
            icon.Icon = Properties.Resources.Icon;
            icon.ContextMenu = menu;
            icon.Visible = true;

            timer = new System.Threading.Timer(RunInvasionCheck, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            base.OnLoad(e);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RunInvasionCheck(object state)
        {
            TTResponse response;

            try
            {
                string s = client.DownloadString("https://www.toontownrewritten.com/api/invasions");
                response = JsonConvert.DeserializeObject<TTResponse>(s);
            }
            catch (Exception)
            {
                icon.BalloonTipTitle = "Couldn't connect to Toontown Rewritten!";
                icon.BalloonTipText = "Please make sure you are connected to the internet!";
                icon.ShowBalloonTip(20000);
                return;
            }

            Invasion invasion = null;
            string district = null;

            foreach (KeyValuePair<string, Invasion> entry in response.GetInvasions())
            {
                if (entry.Value.GetCogType().Contains("Backstabber"))
                {
                    invasion = entry.Value;
                    district = entry.Key;
                    break;
                }
            }

            if (invasion != null && !lastInvasion)
            {
                icon.BalloonTipTitle = "Version 2.0 Invasion Inbound!";
                icon.BalloonTipText = "The cogs have sent " + invasion.GetCogType() + " cogs to " + district + "! Hurry up before the invasion ends!";
                lastInvasion = true;
                icon.ShowBalloonTip(20000);
            }
            else if (invasion == null && lastInvasion)
            {
                icon.BalloonTipTitle = "Version 2.0 Invasion Over!";
                icon.BalloonTipText = "The toons have saved the day!";
                lastInvasion = false;
                icon.ShowBalloonTip(20000);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                icon.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
