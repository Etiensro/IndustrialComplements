﻿using System.Linq;
using System.Windows;
using Inventory.data;
using Inventory.model;

namespace Inventory.ui
{
	public partial class LoginWindow
	{
		public LoginWindow()
		{
			InitializeComponent();
			LoadUsersFromDatabaseToComboBox();
		}
		private void ChkBoxRememberData_Checked(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TxtBoxPassword.Password))
			{
				ChkBoxRememberData.IsChecked = false;
				MessageBox.Show("Introduce datos para guardar la información");
			}
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			TxtBoxPassword.Password = Properties.Settings.Default.Pass;
			ChkBoxRememberData.IsChecked = Properties.Settings.Default.SaveSession;
		}
		private void BtnConnect_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TxtBoxPassword.Password))
			{
				ChkBoxRememberData.IsChecked = false;
				MessageBox.Show("El campo de contraseña esta vacío");
				return;
			}

			if (ChkBoxRememberData.IsChecked == true)
			{
				Properties.Settings.Default.User = CmbBoxUsers.Text;
				Properties.Settings.Default.Pass = TxtBoxPassword.Password;
				Properties.Settings.Default.SaveSession = true;
				Properties.Settings.Default.Save();
			}

			if (CredentialsAreCorrect())
			{
				new MainWindow().Show();
				this.Close();
				return;
			}

			MessageBox.Show("Usuario o contraseña incorrectos.");
			Properties.Settings.Default.User = "";
			Properties.Settings.Default.Pass = "";
			Properties.Settings.Default.SaveSession = false;
			Properties.Settings.Default.Save();
		}
		private void LoadUsersFromDatabaseToComboBox()
		{
			using InventoryDbContext inventoryDb = new InventoryDbContext();
			CmbBoxUsers.ItemsSource = inventoryDb.Employees.ToList();
			CmbBoxUsers.DisplayMemberPath = "FullName";
			CmbBoxUsers.SelectedValuePath = "FullName";
			CmbBoxUsers.SelectedItem =  inventoryDb.Employees
				.FirstOrDefault(employee => employee.FullName.Equals(Properties.Settings.Default.User));
		}
		private bool CredentialsAreCorrect()
		{
			using InventoryDbContext inventoryDb = new InventoryDbContext();
			Employee employee = inventoryDb.Employees
				.FirstOrDefault(employee => employee == CmbBoxUsers.SelectedItem);

			if (employee == null || !employee.Password.Equals(TxtBoxPassword.Password)) 
				return false;

			return true;
		}
		private void BtnExit_OnClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
