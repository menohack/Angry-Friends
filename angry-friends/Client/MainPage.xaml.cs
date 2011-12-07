using System.Windows.Controls;
using Library.GameLogic;
using System.Windows.Input;
using Library.Engine.Object;
namespace Client {
	public partial class MainPage : UserControl {

		/// <summary>
		/// The hook for our Silverlight application.
		/// </summary>
		public MainPage() {
            InitializeComponent();
            Initialize();
		}

		Controller controller;

        /// <summary>
        /// The hook for Engine.
        /// </summary>
        private void Initialize()
        {
<<<<<<< HEAD
            Canvas viewport = new Canvas();
			canvas.Children.Add(viewport);

			controller = Controller.Instance;

			Game game = new Game(viewport);
=======
            new EngineObjectHelper(canvas);
>>>>>>> 2c6dee3145729079b8c7cc36b123172a8f324145
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