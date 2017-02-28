using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NumbersToWordsClient {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {


		public MainWindow() {
			InitializeComponent();
		}

		private void CanConvertToWords(object sender, CanExecuteRoutedEventArgs e) {
			throw new NotImplementedException();
		}

		private void ConvertToWords(object sender, ExecutedRoutedEventArgs e) {
			throw new NotImplementedException();
		}

		private async void GetWords(object sender, RoutedEventArgs e) {
			var vm = DataContext as ConvertToWordsViewModel;
			if (vm == null)
				return;
			await vm.ConvertToWords();
			InputBox.Select(0, vm.Input.Length);
			InputBox.Focus();
		}

		private void OnTextChanged(object sender, KeyEventArgs keyEventArgs) {
			if (keyEventArgs.Key == Key.Enter)
			{
				GetWords(null, null);
			}
		}
	}
}
