using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsignmentShopLibrary;


namespace ConsignmentShopUI
{
    public partial class ConsignmentShop : Form
    {
        private Store store = new Store();
        private List<Item> shoppingCartData = new List<Item>();
        private decimal storeProfit = 0;


        BindingSource itemBinding = new BindingSource();
        BindingSource cartBinding = new BindingSource();
        BindingSource vendorBinding = new BindingSource();
        public ConsignmentShop()
        {
            InitializeComponent();
            SetupData();
            itemBinding.DataSource = store.Items.Where(x=>x.Sold==false).ToList();
            
            itemsListBox.DataSource = itemBinding;
            itemsListBox.DisplayMember = "";
            itemsListBox.DisplayMember = "Display";
            itemsListBox.ValueMember = "Display";

            cartBinding.DataSource = shoppingCartData;
            shoppingCartListBox.DataSource = cartBinding;

            shoppingCartListBox.DisplayMember = "Display";
            shoppingCartListBox.ValueMember = "Display";

            vendorBinding.DataSource = store.Vendors;
            vendorListBox.DataSource = vendorBinding;

            vendorListBox.DisplayMember = "Display";
            vendorListBox.ValueMember = "Display";


        }


        private void SetupData() {
            //Vendor demoVendor = new Vendor();
            //demoVendor.FirstName = "Burak";
            //demoVendor.LastName = "Yılmaz";
            //demoVendor.Commission = 0.5;
            //store.Vendors.Add(demoVendor);

            //demoVendor = new Vendor();
            //demoVendor.FirstName = "Bill";
            //demoVendor.LastName = "Smith";
            //demoVendor.Commission = 0.45;
            //store.Vendors.Add(demoVendor);

            store.Vendors.Add(new Vendor { FirstName = "Burak", LastName = "Yılmaz"});
            store.Vendors.Add(new Vendor { FirstName = "Bill", LastName = "Smith"});

            store.Items.Add(new Item 
            { 
                Title = "LOTR", 
                Description = "book about middle earth",
                Price = 10.45M, 
                Owner = store.Vendors[0] });
            store.Items.Add(new Item
            {
                Title = "martin eden",
                Description = "book about a writer",
                Price = 28.90M,
                Owner = store.Vendors[0]
            });
            store.Items.Add(new Item
            {
                Title = "harry potter",
                Description = "book about a wizard",
                Price = 6.50M,
                Owner = store.Vendors[1]
            });
            store.Items.Add(new Item
            {
                Title = "a tale of two cities",
                Description = "book about revolution",
                Price = 6.50M,
                Owner = store.Vendors[1]
            });
            store.Name = "kardeşler kitap";
        }


        //if u want to remove an event, first unassign event from design part
        private void addToCart_Click(object sender, EventArgs e)
        {
            Item selectedItem = (Item)itemsListBox.SelectedItem;
            shoppingCartData.Add(selectedItem);

            cartBinding.ResetBindings(false);

            
        }

        private void makePurchase_Click(object sender, EventArgs e)
        {
            foreach (Item item in shoppingCartData)
            {
                item.Sold = true;
                item.Owner.PaymentDue += (decimal)item.Owner.Commission * item.Price;
                storeProfit+= (decimal)(1-item.Owner.Commission) * item.Price;
            }
            shoppingCartData.Clear();

            itemBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            storeProfitValue.Text = string.Format("${0}",storeProfit);


            cartBinding.ResetBindings(false);
            itemBinding.ResetBindings(false);
            vendorBinding.ResetBindings(false);
        }
    }
}
