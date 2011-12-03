using System.Windows.Controls;
using Library.GameLogic;
using System.Windows.Input;
namespace Client {
	public partial class MainPage : UserControl {

		/// <summary>
		/// This class represents our Silverlight page.
		/// </summary>
		public MainPage() {
			InitializeComponent();
            Initialize();
		}

		Input input;

        /// <summary>
        /// The hook for the Engine.
        /// </summary>
        public void Initialize()
        {
            Canvas viewport = new Canvas();
			canvas.Children.Add(viewport);

			input = Input.Instance;

			Game game = new Game(viewport);
        }

		protected override void OnKeyDown(KeyEventArgs e)
		{
			input.OnKeyDown(this, e);
			e.Handled = true;
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			input.OnKeyUp(this, e);
			e.Handled = true;
		}

		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			input.OnMouseLeftButtonDown(this, e);
			e.Handled = true;
		}

		protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
		{
			input.OnMouseLeftButtonDown(this, e);
			e.Handled = true;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			input.OnMouseMove(this, e);
		}
	}
}