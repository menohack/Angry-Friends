using System.Windows.Controls;
using Library.GameLogic;
using System.Windows.Input;
using Library.Engine.Object;
namespace Client {
	public partial class MainPage : UserControl {

		/// <summary>
		/// This class represents our Silverlight page.
		/// </summary>
		public MainPage() {
			InitializeComponent();
            Initialize();
		}

		Controller controller;

        /// <summary>
        /// The hook for the Engine.
        /// </summary>
        public void Initialize()
        {
            Canvas viewport = new Canvas();
			canvas.Children.Add(viewport);

			controller = Controller.Instance;

			Game game = new Game(viewport);
        }

		protected override void OnKeyDown(KeyEventArgs e)
		{
			controller.OnKeyDown(this, e);
			e.Handled = true;
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			controller.OnKeyUp(this, e);
			e.Handled = true;
		}

		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			controller.OnMouseLeftButtonDown(this, e);
			e.Handled = true;
		}

		protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
		{
			controller.OnMouseLeftButtonDown(this, e);
			e.Handled = true;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			controller.OnMouseMove(this, e);
		}
	}
}