using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using InputManager;

namespace HeroBot.NET
{
	public partial class MainWindow : Form
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void MainWindow_Load(object sender, EventArgs e)
		{
			//HeroEntity h = Utility.GetHero(Heroes.Abaddon);

			//Image<Gray, byte> screen = Utility.CurrentScreen;
			//HeroEntity terra = Utility.GetHero(Heroes.Terra);

			//terra.ScrollToHero();
			//terra.LevelUp();

			Point window = Utility.GetWindow();
			Mouse.Move(window.X, window.Y);
		}
	}
}
