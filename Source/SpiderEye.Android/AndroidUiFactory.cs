using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SpiderEye.Bridge;
using Xamarin.Forms.Platform.Android;

namespace SpiderEye.Android
{
	internal class AndroidUiFactory : IUiFactory
	{
		private FormsAppCompatActivity _mainActivity;
		public AndroidUiFactory(FormsAppCompatActivity mainActivity)
		{
			this._mainActivity = mainActivity;
		}
		public IFolderSelectDialog CreateFolderSelectDialog()
		{
			throw new NotImplementedException();
		}

		public ILabelMenuItem CreateLabelMenu(string label)
		{
			throw new NotImplementedException();
		}

		public IMenu CreateMenu()
		{
			throw new NotImplementedException();
		}

		public IMenuItem CreateMenuSeparator()
		{
			throw new NotImplementedException();
		}

		public IMessageBox CreateMessageBox()
		{
			throw new NotImplementedException();
		}

		public IOpenFileDialog CreateOpenFileDialog()
		{
			throw new NotImplementedException();
		}

		public ISaveFileDialog CreateSaveFileDialog()
		{
			throw new NotImplementedException();
		}

		public IStatusIcon CreateStatusIcon(string title)
		{
			throw new NotImplementedException();
		}

		public IWindow CreateWindow(WindowConfiguration config, WebviewBridge bridge)
		{
			return new AndroidWindow(this._mainActivity,bridge);
		}
	}
}