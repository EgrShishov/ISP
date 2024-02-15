
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyMauiApp;

public partial class ProgressBarPage : ContentPage, INotifyPropertyChanged
{
	public ProgressBarPage()
	{
		InitializeComponent();
		BindingContext=this;
	}

	static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
	CancellationToken cancellationToken = cancellationTokenSource.Token;

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
    public double Progress { get; set; }
	private void ButtonPressed(object sender, EventArgs e)
	{
		((Button)sender).BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgba("#4f6354");
	}

	private void ButtonRealesed(object sender, EventArgs e)
	{
        ((Button)sender).BackgroundColor = Colors.SeaGreen;
	}

	private async void StartCalculation(object sender, EventArgs e)
	{
		StartButton.IsEnabled = false;
		CancelButton.IsEnabled = true;

		progressBar.Progress = 0;
		TopLabel.Text = "����������";

        cancellationTokenSource = new CancellationTokenSource();
        cancellationToken = cancellationTokenSource.Token;

        await Calculation();
    }

	private void CancelCalculation(object sender, EventArgs e)
	{
		if (cancellationTokenSource == null) return;
		cancellationTokenSource.Cancel();
		StartButton.IsEnabled = true;
		CancelButton.IsEnabled = false;
		TopLabel.Text = "������� �������";
	}

	private async Task Calculation()
	{
        var result = 0.0;
        TopLabel.Text = "����������";
        var h = 1E-4;
        var a = 0.0; var b = 1.0;
		for (double i = a; i < b; i += h)
		{
			if (cancellationToken.IsCancellationRequested) 
			{
				cancellationTokenSource.Dispose();
				return;
			}
			result += Math.Sin(i) * h;
			await progressBar.ProgressTo(i, 1, Easing.Linear);
			Progress =  Math.Round(i*100, 2);
			OnPropertyChanged("Progress");

            //ProgressBarLabel.Text = $"��������� ��������: {Math.Round(i*100, 2)}%";
		}

        TopLabel.Text = $"������� �������� � ����������� : {result}";
        StartButton.IsEnabled = true;
        CancelButton.IsEnabled = false;
    }
}