using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using NumbersToWordsClient.Annotations;

namespace NumbersToWordsClient {

	public class NumToWordModel {
		public string EnteredNumber { get; set; }
		public string ErrorInformation { get; set; }
		public bool Success { get; set; }
		public string NumberToWords { get; set; }
	}

	public class ConvertToWordsViewModel : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private ObservableCollection<NumToWordModel> _numToWordCollection = new ObservableCollection<NumToWordModel>();
		public ObservableCollection<NumToWordModel> NumbersToWordCollection {
			get { return _numToWordCollection; }
			private set {
				if (_numToWordCollection == value) return;
				_numToWordCollection = value;
				OnPropertyChanged();
			}
		}

		private string _input;

		public string Input {
			get { return _input; }
			set {
				if (_input == value) return;
				_input = value;
				OnPropertyChanged();
			}
		}

		public async Task ConvertToWords() {
			var input = _input; // copy over because value can be changed while process awaits response

			try {
				var client = new NumberToWordsService.Service1Client();
				var response = await client.GetWordRepresentationAsync(Input);
				var model = new NumToWordModel {
					Success = response.Success,
					EnteredNumber = input,
					NumberToWords = response.Result,
					ErrorInformation = response.FailureReason
				};
				_numToWordCollection.Insert(0, model);
			}
			catch (Exception ex) {
				var model = new NumToWordModel {
					Success = false,
					EnteredNumber = input,
					NumberToWords = "",
					ErrorInformation = ex.Message
				};
				_numToWordCollection.Insert(0, model);
			}
		}
	}
	public class BoolToVisibilityConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
	public class NegatedBoolToVisibilityConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return ((bool)value) ? Visibility.Collapsed : Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
