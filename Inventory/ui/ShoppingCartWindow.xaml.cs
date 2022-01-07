﻿using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Threading;
using Inventory.data;
using Inventory.model;

namespace Inventory.ui
{
	public partial class ShoppingCartWindow
	{
		public static readonly ShoppingCartWindow Instance = new();
		private DispatcherTimer NewProductToBuyLookupTimer { get; set; }
		private InventoryDbContext InventoryDb { get; set; }
		private ObservableCollection<ProductForBuy> ProductsToBuyCollection { get; set; }
		private CollectionViewSource ProductsToBuyView { get; set; }
		public ShoppingCartWindow()
		{
			InitializeComponent();
		}
	}
}
