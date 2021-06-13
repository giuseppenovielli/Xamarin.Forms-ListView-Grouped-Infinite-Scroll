using System.Threading.Tasks;
using ListViewGroupedInfiniteScrool.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace ListViewGroupedInfiniteScrool
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel();

            On<iOS>().SetUseSafeArea(true);
        }

        async void listView_ItemAppearing(System.Object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        {
            var cellVM = BindingContext as MainPageViewModel;
            if (cellVM != null && e.Item != null)
            {

                var itemsList = cellVM.ItemsList;
                var itemsListCount = itemsList.Count;

                var itemGroup = e.Item as ItemViewModel;


                if (itemsList != null &&
                    itemsList.Count > 0 &&
                    itemGroup != null
                    )
                {

                    var lastGroupedItem = itemsList[itemsListCount - 1];
                    if (lastGroupedItem.Count > 0)
                    {
                        var lastItem = lastGroupedItem[lastGroupedItem.Count - 1];
                        if (itemGroup.Num == lastItem.Num)
                        {
                            System.Diagnostics.Debug.WriteLine($"\n\nLoadingMore lastItem.Num = {lastItem.Num}");
                            System.Diagnostics.Debug.WriteLine($"LoadingMore itemGroup.Num = {itemGroup.Num}");

                            await cellVM.GetItems();
                        }
                    }

                }
            }
        }
    }
}
