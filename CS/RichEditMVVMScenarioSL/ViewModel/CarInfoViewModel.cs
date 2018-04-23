using System.Windows.Input;
using DevExpress.Xpf.Core.Commands;
using RichEditMVVMScenarioSL.CarInfoServiceReference;

namespace RichEditMVVMScenarioSL.ViewModel {
    public class CarInfoViewModel : ObservableObject {
        private RichEditMVVMScenarioSL.Model.CarInfo model;

        public CarInfoViewModel()
            : this(new RichEditMVVMScenarioSL.Model.CarInfo()) {

            LoadCommand.Execute(null);
        }

        public CarInfoViewModel(RichEditMVVMScenarioSL.Model.CarInfo model) {
            this.model = model;

            name = model.Name;
            description = model.Description;

            LoadCommand = new DevExpress.Xpf.Mvvm.DelegateCommand<object>(LoadCommandExecute);
            SaveCommand = new DevExpress.Xpf.Mvvm.DelegateCommand<object>(SaveCommandExecute);
        }

        private string name;
        private string description;

        public string Name {
            get { return name; }
            set {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description {
            get { return description; }
            set {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public ICommand LoadCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        private void LoadCommandExecute(object parameter) {
            CarInfoServiceClient proxy = new CarInfoServiceClient();

            proxy.LoadCarInfoCompleted += ((s, e) => {
                model.Name = e.Result.Name;
                model.Description = e.Result.Description;
                this.Name = model.Name;
                this.Description = model.Description;
            });

            proxy.LoadCarInfoAsync();
        }

        private void SaveCommandExecute(object parameter) {
            model.Name = name;
            model.Description = description;

            CarInfoServiceClient proxy = new CarInfoServiceClient();

            proxy.SaveCarInfoAsync(new RichEditMVVMScenarioSL.CarInfoServiceReference.CarInfo() {
                Name = model.Name,
                Description = model.Description
            });
        }
    }
}
