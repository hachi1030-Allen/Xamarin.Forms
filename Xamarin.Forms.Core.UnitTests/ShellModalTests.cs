﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Xamarin.Forms.Core.UnitTests
{
	[TestFixture]
	public class ShellModalTests : ShellTestBase
	{
		[Test]
		public async Task BasicModalBehaviorTest()
		{
			Shell shell = new Shell();
			shell.Items.Add(CreateShellItem());

			await shell.GoToAsync("ModalTestPage");

			var navStack = shell.Items[0].Items[0].Navigation;

			Assert.AreEqual(1, navStack.ModalStack.Count);
			Assert.AreEqual(typeof(ModalTestPage), navStack.ModalStack[0].GetType());
		}


		[Test]
		public async Task ModalPopsWhenSwitchingShellItem()
		{
			Shell shell = new Shell();
			shell.Items.Add(CreateShellItem());
			shell.Items.Add(CreateShellItem(shellItemRoute: "NewRoute"));

			// pushes modal onto visible shell section
			await shell.GoToAsync("ModalTestPage");

			// Navigates to different Shell Item
			await shell.GoToAsync("///NewRoute");

			var navStack = shell.Items[0].Items[0].Navigation;
			Assert.AreEqual(0, navStack.ModalStack.Count);
		}

		[Test]
		public async Task ModalPopsWhenSwitchingShellSection()
		{
			Shell shell = new Shell();
			shell.Items.Add(CreateShellItem());
			shell.Items[0].Items.Add(CreateShellSection(shellSectionRoute: "NewRoute"));

			// pushes modal onto visible shell section
			await shell.GoToAsync("ModalTestPage");

			// Navigates to different Shell Item
			await shell.GoToAsync("///NewRoute");

			var navStack = shell.Items[0].Items[0].Navigation;
			Assert.AreEqual(0, navStack.ModalStack.Count);
		}

		[Test]
		public async Task ModalPopsWhenSwitchingShellContent()
		{
			Shell shell = new Shell();
			shell.Items.Add(CreateShellItem());
			shell.Items[0].Items[0].Items.Add(CreateShellContent(shellContentRoute: "NewRoute"));

			// pushes modal onto visible shell section
			await shell.GoToAsync("ModalTestPage");

			// Navigates to different Shell Item
			await shell.GoToAsync("///NewRoute");

			var navStack = shell.Items[0].Items[0].Navigation;
			Assert.AreEqual(0, navStack.ModalStack.Count);
		}

		[Test]
		public async Task ModalPopsWhenNavigatingWithoutModalRoute()
		{
			Shell shell = new Shell();
			shell.Items.Add(CreateShellItem(shellItemRoute: "NewRoute"));

			// pushes modal onto visible shell section
			await shell.GoToAsync("ModalTestPage");

			// Navigates to different Shell Item
			await shell.GoToAsync("///NewRoute");

			var navStack = shell.Items[0].Items[0].Navigation;
			Assert.AreEqual(0, navStack.ModalStack.Count);
		}


		[Test]
		public async Task ModalPopsWhenNavigatingToNewModalRoute()
		{
			Shell shell = new Shell();
			shell.Items.Add(CreateShellItem(shellItemRoute: "NewRoute"));

			// pushes modal onto visible shell section
			await shell.GoToAsync("ModalTestPage");

			// Navigates to different Shell Item
			await shell.GoToAsync("///NewRoute/ModalTestPage2");

			var navStack = shell.Items[0].Items[0].Navigation;
			Assert.AreEqual(1, navStack.ModalStack.Count);
			Assert.AreEqual(typeof(ModalTestPage2), navStack.ModalStack[0].GetType());
		}

		public class ModalTestPage : ContentPage
		{
			public ModalTestPage()
			{
				Shell.SetModalBehavior(this, new ModalBehavior() { Modal = true });
			}
		}

		public class ModalTestPage2 : ContentPage
		{
			public ModalTestPage2()
			{
				Shell.SetModalBehavior(this, new ModalBehavior() { Modal = true });
			}
		}

		public override void Setup()
		{
			base.Setup();
			Routing.RegisterRoute("ModalTestPage", typeof(ModalTestPage));
			Routing.RegisterRoute("ModalTestPage2", typeof(ModalTestPage));
		}
	}
}
