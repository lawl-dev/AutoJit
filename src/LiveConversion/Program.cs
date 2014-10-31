using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	internal static class Program
	{
		/// <summary>
		///     Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		private static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );
			Application.Run( new Form1() );
		}
	}
}
