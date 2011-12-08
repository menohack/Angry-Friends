using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Library.Engine.Object;

namespace Client
{
    public partial class TestGame : UserControl
    {
        public TestGame()
        {
            InitializeComponent();
            Initialize();
        }

        /// <summary>
        /// The hook for the game.
        /// </summary>
        public void Initialize() {
            new EngineObjectHelper(canvas);
        }

		protected override void OnKeyDown(KeyEventArgs e)
		{
			EngineObject.Instance.Input.OnKeyDown(this, e);
			e.Handled = true;
		}

		public void MyKeyDown(KeyEventArgs e)
		{
			OnKeyDown(e);
		}

		public void MyKeyUp(KeyEventArgs e)
		{
			OnKeyUp(e);
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			EngineObject.Instance.Input.OnKeyUp(this, e);
			e.Handled = true;
		}

		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			EngineObject.Instance.Input.OnMouseLeftButtonDown(this, e);
			e.Handled = true;
		}

		protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
		{
			EngineObject.Instance.Input.OnMouseRightButtonDown(this, e);
			e.Handled = true;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			EngineObject.Instance.Input.OnMouseMove(this, e);
		}
    }
}
