using LabRab5App.ViewModels;

namespace LabRab5App.Pages;

public partial class SongDetails : ContentPage
{
	public SongDetails(SongDetailsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}