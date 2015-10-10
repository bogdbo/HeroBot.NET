using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace HeroBot.NET
{
	public class Utility
	{
		private static Point? windowPoint;

		public static Point GetWindow()
		{
			if (windowPoint != null)
			{
				return windowPoint.Value;
			}

			string start = Path.Combine(Environment.CurrentDirectory, "img", "start.png");
			Image<Gray, byte> template = new Image<Gray, byte>((Bitmap)Image.FromFile(start));

			Point? match = Match(Utility.CurrentScreen, template);
			if (match == null)
			{
				throw new FileNotFoundException("Cannot find Window");
			}

			return (windowPoint = new Point(match.Value.X + 11, match.Value.Y + 171)).Value;
		}


		public static Image<Gray, byte> CurrentScreen
		{
			get
			{
				Bitmap bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
						Screen.PrimaryScreen.Bounds.Height);
				{
					using (Graphics g = Graphics.FromImage(bmpScreenCapture))
					{
						g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0,
								bmpScreenCapture.Size, CopyPixelOperation.SourceCopy);
					}
				}

				return new Image<Gray, byte>(bmpScreenCapture);
			}
		}

		public static HeroEntity GetHero(Heroes hero)
		{
			HeroEntity entity = new HeroEntity(hero);
			entity.Load();
			return entity;
		}

		public static Point? Match(Image<Gray, byte> image, Image<Gray, byte> template)
		{
			Image<Gray, float> result = image.MatchTemplate(template, TemplateMatchingType.CcoeffNormed);
			double[] min, max;
			Point[] point1, point2;
			result.MinMax(out min, out max, out point1, out point2);

			return point2.Length != 0 && max[0] > 0.75 ? point2[0] : Point.Empty;
		}
	}
}