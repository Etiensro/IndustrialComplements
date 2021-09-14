﻿using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Inventory.enums;
using Inventory.model;

namespace Inventory.ui
{
	public partial class ProductWindow : Window
	{
		private int _currentTask;
		private Product _product;
		private int CurrentTask
		{
			get => _currentTask;
			set
			{
				if (value < 3)
				{
					_currentTask = value;
				}
			}
		}
		private Product Product
		{
			get => _product;
			set => _product = value;
		}

		public ProductWindow(int task)
		{
			InitializeComponent();
			CurrentTask = task;
			Product = new Product();
			ConfigureControlsForTask();
		}
		public ProductWindow(int task, string productId)
		{
			InitializeComponent();
			CurrentTask = task;
			Product = new Product();
			AssignProductDataToControls(productId);
			ConfigureControlsForTask();
		}
		public ProductWindow(int task, Product product)
		{
			InitializeComponent();
			CurrentTask = task;
			Product = product;
			AssignProductDataToControls(Product.ProductId.ToString());
			ConfigureControlsForTask();
		}
		private void AssignProductDataToControls(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				MessageBox.Show("El campo ID no puede estar vacio", "Error");
				return;
			}

			if (Product.ProductId == 0)
			{
				Product.GetDataFromSqlDatabase(id);
			}

			TxtBoxId.Text = Product.ProductId.ToString();
			TxtBoxIdCode.Text = Product.ProductId.ToString();
			TxtBoxEnrollment.Text = Product.Enrollment;
			TxtBoxShortDescription.Text = Product.ShortDescription;
			TxtBoxCurrentProductStock.Text = Product.CurrentProductStock;
			TxtBoxMinProductStock.Text = Product.MinProductStock;
			TxtBoxMaxProductStock.Text = Product.MaxProductStock;
			TxtBoxContainer.Text = Product.Container;
			TxtBoxLocation.Text = Product.Location;
			TxtBoxBranchOffice.Text = Product.BranchOffice;
			TxtBoxShelf.Text = Product.Shelf;
			TxtBoxLedge.Text = Product.Rack;
			TxtBoxPurchasePrice.Text = Product.PurchasePrice;
			TxtBoxManufacturerPartNumber.Text = Product.ManufacturerPartNumber;
			TxtBoxPercentageOfProfit.Text = Product.PercentageOfProfit;
			TxtBoxDiscountRate.Text = Product.DiscountRate;
			TxtBoxSalePrice.Text = Product.SalePrice;
			TxtBoxPriceWithDiscount.Text = Product.PriceWithDiscount;
			TxtBoxUtility.Text = Product.Utility;
			TxtBoxProfitWithDiscount.Text = Product.ProfitWithDiscount;
			TxtBoxFullDescription.Text = Product.FullDescription;
			TxtBoxMemo.Text = Product.Memo;
			
			CmbBoxStatus.Items.Clear();
			CmbBoxProductType.Items.Clear();
			CmbBoxCategories.Items.Clear();
			CmbBoxManufacturer.Items.Clear();
			CmbBoxUnit.Items.Clear();
			CmbBoxEncapsulationType.Items.Clear();
			CmbBoxMountingTechnology.Items.Clear();
			
			CmbBoxStatus.Items.Add(Product.State);
			CmbBoxProductType.Items.Add(Product.ProductType);
			CmbBoxUnit.Items.Add(Product.Unit);
			CmbBoxManufacturer.Items.Add(Product.Manufacturer);
			CmbBoxCategories.Items.Add(Product.Category);
			CmbBoxMountingTechnology.Items.Add(Product.MountingTechnology);
			CmbBoxEncapsulationType.Items.Add(Product.EncapsulationType);
			
			CmbBoxStatus.SelectedIndex = 0;
			CmbBoxProductType.SelectedIndex = 0;
			CmbBoxUnit.SelectedIndex = 0;
			CmbBoxManufacturer.SelectedIndex = 0;
			CmbBoxCategories.SelectedIndex = 0;
			CmbBoxMountingTechnology.SelectedIndex = 0;
			CmbBoxEncapsulationType.SelectedIndex = 0;
			
			if (Product.TheProductUsesInventory)
			{
				ChkBoxTheProductUsesInventory.IsChecked = true;
				EnableControlsForInventory();
			}

			if (!Product.TheProductUsesInventory)
			{
				ChkBoxTheProductUsesInventory.IsChecked = false;
				DisableControlsForInventory();
			}

			if (Product.ManualProfit)
			{
				RadioBtnAutomaticProfit.IsChecked = false;
				EnableControlsForManualProfit();
			}

			if (!Product.ManualProfit)
			{
				RadioBtnAutomaticProfit.IsChecked = true;
				DisableControlsForAutomaticProfit();
			}
		}
		private void ConfigureControlsForTask()
		{
			if (CurrentTask == (int)ProductWindowTasks.AddNewProduct)
			{
				UnlockEditableControls();
				TxtBlockProductTask.Text = "Nuevo Producto";
				BtnAddModifyAndSave.Content = "Agregar";
				return;
			}

			if (CurrentTask == (int)ProductWindowTasks.Modify)
			{
				UnlockEditableControls();
				TxtBlockProductTask.Text = "Modificar Producto";
				BtnAddModifyAndSave.Content = "Guardar";
				return;
			}

			LockEditableControls();
			TxtBlockProductTask.Text = "Detalles del Producto";
			BtnAddModifyAndSave.Content = "Modificar";
		}
		private void LockEditableControls()
		{
			TxtBlockProductTask.Foreground = new SolidColorBrush(Colors.Crimson);
			
			CmbBoxStatus.IsReadOnly = true;
			CmbBoxStatus.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxEnrollment.IsReadOnly = true;
			TxtBoxEnrollment.Foreground = new SolidColorBrush(Colors.Crimson);
			CmbBoxMountingTechnology.IsReadOnly = true;
			CmbBoxMountingTechnology.Foreground = new SolidColorBrush(Colors.Crimson);
			CmbBoxEncapsulationType.IsReadOnly = true;
			CmbBoxEncapsulationType.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxShortDescription.IsReadOnly = true;
			TxtBoxShortDescription.Foreground = new SolidColorBrush(Colors.Crimson);
			CmbBoxCategories.IsReadOnly = true;
			CmbBoxCategories.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxFullDescription.IsReadOnly = true;
			TxtBoxFullDescription.Foreground = new SolidColorBrush(Colors.Crimson);

			ChkBoxTheProductUsesInventory.IsEnabled = false;
			TxtBoxCurrentProductStock.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxMinProductStock.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxMaxProductStock.Foreground = new SolidColorBrush(Colors.Crimson);
			DisableControlsForInventory();

			TxtBoxContainer.IsReadOnly = true;
			TxtBoxContainer.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxLocation.IsReadOnly = true;
			TxtBoxLocation.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxBranchOffice.IsReadOnly = true;
			TxtBoxBranchOffice.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxShelf.IsReadOnly = true;
			TxtBoxShelf.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxLedge.IsReadOnly = true;
			TxtBoxLedge.Foreground = new SolidColorBrush(Colors.Crimson);

			TxtBoxPurchasePrice.IsReadOnly = true;
			TxtBoxPurchasePrice.Foreground = new SolidColorBrush(Colors.Crimson);
			CmbBoxUnit.IsReadOnly = true;
			CmbBoxUnit.Foreground = new SolidColorBrush(Colors.Crimson);
			CmbBoxProductType.IsReadOnly = true;
			CmbBoxProductType.Foreground = new SolidColorBrush(Colors.Crimson);
			CmbBoxManufacturer.IsReadOnly = true;
			CmbBoxManufacturer.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxManufacturerPartNumber.IsReadOnly = true;
			TxtBoxManufacturerPartNumber.Foreground = new SolidColorBrush(Colors.Crimson);
			
			RadioBtnAutomaticProfit.IsEnabled = false;
			RadioBtnManualProfit.IsEnabled = false;
			TxtBoxPercentageOfProfit.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxSalePrice.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxUtility.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxDiscountRate.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxPriceWithDiscount.Foreground = new SolidColorBrush(Colors.Crimson);
			TxtBoxProfitWithDiscount.Foreground = new SolidColorBrush(Colors.Crimson);
			LockControlsForAutomaticProfit();

			TxtBoxMemo.IsReadOnly = true;
			TxtBoxMemo.Foreground = new SolidColorBrush(Colors.Crimson);
		}
		private void UnlockEditableControls()
		{
			TxtBlockProductTask.Foreground = new SolidColorBrush(Colors.Black);
			
			CmbBoxStatus.IsReadOnly = false;
			CmbBoxStatus.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxEnrollment.IsReadOnly = false;
			TxtBoxEnrollment.Foreground = new SolidColorBrush(Colors.Black);
			CmbBoxMountingTechnology.IsReadOnly = false;
			CmbBoxMountingTechnology.Foreground = new SolidColorBrush(Colors.Black);
			CmbBoxEncapsulationType.IsReadOnly = false;
			CmbBoxEncapsulationType.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxShortDescription.IsReadOnly = false;
			TxtBoxShortDescription.Foreground = new SolidColorBrush(Colors.Black);
			CmbBoxCategories.IsReadOnly = false;
			CmbBoxCategories.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxFullDescription.IsReadOnly = false;
			TxtBoxFullDescription.Foreground = new SolidColorBrush(Colors.Black);

			ChkBoxTheProductUsesInventory.IsEnabled = true;
			TxtBoxCurrentProductStock.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxMinProductStock.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxMaxProductStock.Foreground = new SolidColorBrush(Colors.Black);
			EnableControlsForInventory();

			TxtBoxContainer.IsReadOnly = false;
			TxtBoxContainer.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxLocation.IsReadOnly = false;
			TxtBoxLocation.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxBranchOffice.IsReadOnly = false;
			TxtBoxBranchOffice.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxShelf.IsReadOnly = false;
			TxtBoxShelf.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxLedge.IsReadOnly = false;
			TxtBoxLedge.Foreground = new SolidColorBrush(Colors.Black);
			CmbBoxUnit.IsReadOnly = false;
			CmbBoxUnit.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxPurchasePrice.IsReadOnly = false;
			TxtBoxPurchasePrice.Foreground = new SolidColorBrush(Colors.Black);
			CmbBoxProductType.IsReadOnly = false;
			CmbBoxProductType.Foreground = new SolidColorBrush(Colors.Black);
			CmbBoxManufacturer.IsReadOnly = false;
			CmbBoxManufacturer.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxManufacturerPartNumber.IsReadOnly = false;
			TxtBoxManufacturerPartNumber.Foreground = new SolidColorBrush(Colors.Black);
			
			RadioBtnAutomaticProfit.IsEnabled = true;
			RadioBtnManualProfit.IsEnabled = true;
			TxtBoxPercentageOfProfit.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxSalePrice.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxUtility.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxDiscountRate.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxPriceWithDiscount.Foreground = new SolidColorBrush(Colors.Black);
			TxtBoxProfitWithDiscount.Foreground = new SolidColorBrush(Colors.Black);
			UnlockControlsForManualProfit();

			TxtBoxMemo.IsReadOnly = false;
			TxtBoxMemo.Foreground = new SolidColorBrush(Colors.Black);
		}
		private void UnlockControlsForManualProfit()
		{
			TxtBoxPercentageOfProfit.IsReadOnly = false;
			TxtBoxSalePrice.IsReadOnly = false;
			TxtBoxUtility.IsReadOnly = false;
			TxtBoxDiscountRate.IsReadOnly = false;
			TxtBoxPriceWithDiscount.IsReadOnly = false;
			TxtBoxProfitWithDiscount.IsReadOnly = false;
		}
		private void EnableControlsForManualProfit()
		{
			RadioBtnManualProfit.IsChecked = true;
			UnlockControlsForManualProfit();
		}
		private void LockControlsForAutomaticProfit()
		{
			TxtBoxPercentageOfProfit.IsReadOnly = true;
			TxtBoxSalePrice.IsReadOnly = true;
			TxtBoxUtility.IsReadOnly = true;
			TxtBoxDiscountRate.IsReadOnly = true;
			TxtBoxPriceWithDiscount.IsReadOnly = true;
			TxtBoxProfitWithDiscount.IsReadOnly = true;
		}
		private void DisableControlsForAutomaticProfit()
		{
			RadioBtnManualProfit.IsChecked = false;
			LockControlsForAutomaticProfit();
		}
		private void EnableControlsForInventory()
		{
			TxtBoxCurrentProductStock.IsReadOnly = false;
			TxtBoxMinProductStock.IsReadOnly = false;
			TxtBoxMaxProductStock.IsReadOnly = false;
		}
		private void DisableControlsForInventory()
		{
			TxtBoxCurrentProductStock.IsReadOnly = true;
			TxtBoxMinProductStock.IsReadOnly = true;
			TxtBoxMaxProductStock.IsReadOnly = true;
		}
		private void BtnSearch_Click(object sender, RoutedEventArgs e)
		{
			AssignProductDataToControls(TxtBoxIdCode.Text);
		}
		private void BtnAddModifyAndSave_OnClick(object sender, RoutedEventArgs e)
		{
			if (CurrentTask == (int)ProductWindowTasks.Modify)
			{
				CurrentTask = (int)ProductWindowTasks.ShowDetails;
			}
			else if (CurrentTask == (int)ProductWindowTasks.ShowDetails)
			{
				CurrentTask = (int)ProductWindowTasks.Modify;
			}
			else if (CurrentTask == (int)ProductWindowTasks.AddNewProduct)
			{
				CurrentTask = (int)ProductWindowTasks.ShowDetails;
			}
			
			ConfigureControlsForTask();
		}
		private void RadioBtnAutomaticProfit_OnChecked(object sender, RoutedEventArgs e)
		{
			RadioBtnManualProfit.IsChecked = false;
			DisableControlsForAutomaticProfit();
		}
		private void RadioBtnAutomaticProfit_OnUnchecked(object sender, RoutedEventArgs e)
		{
			RadioBtnManualProfit.IsChecked = true;
			EnableControlsForManualProfit();
		}
		private void ChkBoxTheProductUsesInventory_OnChecked(object sender, RoutedEventArgs e)
		{
			EnableControlsForInventory();
		}
		private void ChkBoxTheProductUsesInventory_OnUnchecked(object sender, RoutedEventArgs e)
		{
			DisableControlsForInventory();
		}
		private void TxtBoxIdCode_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key==Key.Enter)
			{
				AssignProductDataToControls(TxtBoxIdCode.Text);
			}
		}
		private void BtnQuickLoad_OnClick(object sender, RoutedEventArgs e)
		{
			AssignProductDataToControls("1");
		}
		private void BtnExit_OnClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
