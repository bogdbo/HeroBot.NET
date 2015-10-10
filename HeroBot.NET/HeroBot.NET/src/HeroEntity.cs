using System;
using System.Drawing;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using InputManager;

namespace HeroBot.NET
{
	public class HeroEntity
	{
		private const string SimpleImageFormat = "{0}.png";
		private const string GoldenImageFormat = "{0}gold.png";
		private const string HireImageFormat = "hire.png";
		private const string LevelImageFormat = "lvlup.png";

		public Heroes Name { get; set; }

		private Image<Gray, byte> simpleImage;
		private Image<Gray, byte> goldenImage;
		private Image<Gray, byte> hireImage;
		private Image<Gray, byte> levelImage;

		public HeroEntity(Heroes hero)
		{
			Name = hero;
		}

		public void LevelUp(int levels = Int32.MaxValue)
		{
			//ScrollToHero();
			Point? p = Utility.Match(Utility.CurrentScreen, levelImage);
			if (p != null)
			{
				Mouse.Move(p.Value.X, p.Value.Y);
			}
		}

		public void ScrollToHero()
		{
			Image<Gray, byte> currentScreen = Utility.CurrentScreen;
			Point? p = Utility.Match(currentScreen, simpleImage) ?? Utility.Match(currentScreen, goldenImage);

			if (p != null)
			{
				Mouse.Move(p.Value.X, p.Value.Y);
			}

			// logging
		}

		public void BuyUpgrades()
		{
			//ScrollToHero();

	
		}

		public void Load()
		{
			string name = Name.ToString().ToLower();
			string folder = Path.Combine(Environment.CurrentDirectory, "img", name);

			simpleImage = new Image<Gray, byte>((Bitmap)Image.FromFile(Path.Combine(folder, String.Format(SimpleImageFormat, name))));
			goldenImage = new Image<Gray, byte>((Bitmap)Image.FromFile(Path.Combine(folder, String.Format(GoldenImageFormat, name))));
			levelImage = new Image<Gray, byte>((Bitmap)Image.FromFile(Path.Combine(folder, LevelImageFormat)));
			hireImage = new Image<Gray, byte>((Bitmap)Image.FromFile(Path.Combine(folder, HireImageFormat)));
		}
	}
}