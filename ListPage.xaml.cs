using CristacheAnaLab7.Models;
using CristacheAnaLab7.Data;

namespace CristacheAnaLab7
{
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var slist = (ShopList)BindingContext;
            slist.Date = DateTime.UtcNow;
            await App.Database.SaveShopListAsync(slist);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var slist = (ShopList)BindingContext;
            await App.Database.DeleteShopListAsync(slist);
            await Navigation.PopAsync();
        }

        async void OnChooseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
            {
                BindingContext = new Product()
            });
        }

        async void OnDeleteItemClicked(object sender, EventArgs e)
        {
            // Ensure that the BindingContext is of type ShopList
            if (BindingContext is ShopList shopList)
            {
                // Implement the logic to delete the selected item from the shopping list
                if (listView.SelectedItem != null)
                {
                    var selectedItem = (Product)listView.SelectedItem;
                    await App.Database.DeleteProductAsync(selectedItem);
                    listView.ItemsSource = await App.Database.GetListProductsAsync(shopList.ID);
                }
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var shopList = (ShopList)BindingContext;

            listView.ItemsSource = await App.Database.GetListProductsAsync(shopList.ID);
        }
    }
}
