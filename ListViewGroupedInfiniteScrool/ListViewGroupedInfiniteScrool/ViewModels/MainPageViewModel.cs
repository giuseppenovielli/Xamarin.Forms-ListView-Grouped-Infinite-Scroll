using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ListViewGroupedInfiniteScrool.ViewModels
{ 
    public class MainPageViewModel : BaseViewModel
    {
        public ObservableCollection<ItemGroupViewModel> ItemsList { get; set; }
        public bool IsRefresh { get; set; }
        public bool IsLoadingMore { get; set; }

        public ICommand RefreshListView { get; set; }

        public MainPageViewModel()
        {
            ItemsList = new ObservableCollection<ItemGroupViewModel>();


            _ = GetItems();
        }

        public async Task GetItems()
        {
            await Task.Delay(2000);

            System.Diagnostics.Debug.WriteLine("\nGetItems");

            //Estraggo l'ultimo elemento dalla lista
            var lastItemNum = 0;
            if (ItemsList.Count > 0)
            {
                var lastItemGrouped = ItemsList[ItemsList.Count - 1];
                if (lastItemGrouped != null)
                {
                    var lastItem = lastItemGrouped[lastItemGrouped.Count - 1];
                    if (lastItem != null)
                    {
                        lastItemNum = lastItem.Num;
                        lastItemNum += 1;
                    }
                }

                IsLoadingMore = true;
            }
            else
            {
                IsRefresh = true;
            }


            for (int i = lastItemNum; i < lastItemNum + 10; i++)
            {
                var result = i / (double)10;
                System.Diagnostics.Debug.WriteLine($"result = {result}     i = {i}");

                if (result % 1 == 0)
                {
                    var numList = new ObservableCollection<ItemViewModel>
                    {
                        InstanceCell(i+1)
                    };
                    
                    var itemGrouped = new ItemGroupViewModel(i.ToString(), i.ToString(), numList);

                    ItemsList.Add(itemGrouped);

                    i += 1;
                }
                else
                {
                    var lastGroupedList = ItemsList[ItemsList.Count - 1];
                    lastGroupedList.Add(InstanceCell(i));
                }
            }

            LoadingComplete();
            
        }

        void LoadingComplete()
        {
            ItemsList = ItemsList;

            IsLoadingMore = false;
            IsRefresh = false;
        }

        ItemViewModel InstanceCell(int num)
        {
            return new ItemViewModel
            {
                Num = num
            };
        }
    }

    public class ItemGroupViewModel : ObservableCollection<ItemViewModel>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }


        public ItemGroupViewModel(string name, string shortName, ObservableCollection<ItemViewModel> listCellVM) : base(listCellVM)
        {
            Name = name;
            ShortName = shortName;
        }
    }

    public class ItemViewModel
    {
        public int Num { get; set; }
    }
}
