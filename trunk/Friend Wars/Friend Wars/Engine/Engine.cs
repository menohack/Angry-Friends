using System;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;

public class Engine
{
    private DateTime lastUpdate;
    TextBlock time;

	public Engine(Canvas canvas)
	{
        DispatcherTimer timer = new DispatcherTimer();
        timer.Interval = TimeSpan.Zero;
        timer.Tick += new EventHandler(tickEvent);
        timer.Start();

        time = new TextBlock();

        canvas.Children.Add(time);

        //BitmapSource bitmap = new BitmapImage(new Uri("Resources//GunboundTitleScreen.bmp"));
        //WriteableBitmap luis = new WriteableBitmap(bitmap);

    }

    void Update()
    {
        DateTime now = DateTime.Now;
        TimeSpan elapsed = now - lastUpdate;
        lastUpdate = now;
        double fred = 1000.0 / elapsed.Milliseconds;
        time.Text = "FPS " + fred.ToString();
        int derp = 2;
        //Is this the branched version? I hope so!
    }

    public void tickEvent(object sender, EventArgs e)
    {
        Update();
    }
}
