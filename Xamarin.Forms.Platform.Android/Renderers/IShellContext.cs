﻿using Android.Content;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;

namespace Xamarin.Forms.Platform.Android
{
	public interface IShellContext
	{
		Context AndroidContext { get; }
		DrawerLayout CurrentDrawerLayout { get; }
		Shell Shell { get; }

		IShellObservableFragment CreateFragmentForPage(Page page);

		IShellFlyoutContentRenderer CreateShellFlyoutContentRenderer();

		IShellItemRenderer CreateShellItemRenderer(ShellItem shellItem);

		IShellToolbarTracker CreateTrackerForToolbar(Toolbar toolbar);

		IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker();

		IShellTabLayoutAppearanceTracker CreateTabLayoutAppearanceTracker(ShellItem shellItem);

		IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem);
	}
}