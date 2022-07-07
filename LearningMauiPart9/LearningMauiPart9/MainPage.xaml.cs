using LearningMauiPart9.ViewModel;

namespace LearningMauiPart9;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel viewModel)
	{
        BindingContext =viewModel;
        InitializeComponent();

    }

}

